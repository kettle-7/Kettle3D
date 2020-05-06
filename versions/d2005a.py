from direct.gui.OnscreenImage import OnscreenImage
from direct.task.TaskManagerGlobal import taskMgr
from direct.showbase.ShowBase import ShowBase
from panda3d.bullet import BulletWorld
from direct.gui.DirectGui import *
from urllib.request import urlopen
from panda3d.core import Filename
from urllib.error import URLError
from os.path import normpath
from direct.task import Task
from sys import platform
from os import getenv
from os import getcwd
import pickle
import time
import sys
import os

directory = None

#if True:
if platform.startswith('win32') or platform.startswith('cygwin'): # do windows-specific things
	directory = getenv("appdata") + "\\Kettle3D\\"
	sys.path[0] = getenv("appdata") + "\\Kettle3D"
if platform.startswith('darwin'): # do apple-specific things
	directory = getenv("HOME") + "/Library/Application Support/Kettle3D/"
	sys.path[0] = getenv("HOME") + "/Library/Application Support/Kettle3D"
	sys.path.append(getenv("HOME") + "/Library/Developer/Panda3D")

class file_dummy():
	def open(self, a=None, b=None, c=None):
		pass
	def close(self):
		pass
	def read(self):
		pass
	def write(self):
		pass

temp_files = {
	'txt' : [],
	'img' : [],
	'image' : []
}

class txtfile():
	def __init__(self, path, version=1, newcontent=None): # file for download
		self.path = path
		self.version = version
		self.winpath = normpath(self.path)
		self.newcontent = newcontent
		print("Looking for file %s..." % path)
		try:
			self.newcontent = open(directory + self.winpath, 'w')
			self.oldcontent = open(directory + self.winpath, 'r')
			print("File %s found successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				if self.oldcontent != self.onlinecontent:
					self.newcontent.write(self.onlinecontent)
					print("Successfully updated file.")
				else:
					print("File matches.")
			except URLError:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except(FileNotFoundError, OSError):
			self.newcontent = open(directory + self.winpath, 'x')
			print("File %s created successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				print("Successfully downloaded file.")
				fae = {
					"path" : self.path,
					"version" : self.version
				}
				temp_files["txt"].append(fae)
				self.newcontent.write(self.onlinecontent)
			except URLError:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.newcontent.close()

class imagefile:
	def __init__(self, path, version=1): # Image for download
		self.path = path
		self.version = version
		winpath = normpath(path)
		self.winpath = winpath
		print("Looking for file %s" % path)
		try:
			img_data = urlopen("https://github.com/Kettle3D/Kettle3D/raw/master/" + path).read()
			with open(directory + winpath, 'wb') as handler:
				handler.write(img_data)
				handler.close()
			fae = {
				"path" : self.path,
				"version" : self.version
			}
			temp_files["image"].append(fae)
			print("File %s downloaded successfully." % self.path)
		except URLError:
			print("Couldn't download file. Maybe try checking your internet connection?")

print("Have 8 files to check or download.")

download_file = txtfile(path='lib/launcherbase.py', version=3)
download_file = txtfile(path='lib/world.py', version=1)
download_file = txtfile(path='assets/concrete.egg', version=1)
download_file = imagefile(path='assets/concrete.png', version=1)
download_file = imagefile(path='assets/generating_level.png', version=1)
download_file = txtfile(path='lib/chunk.py', version=1)
download_file = txtfile(path='lib/block.py', version=2)
gherkin_module = txtfile(path='lib/gherkin.py', version=1)
import lib.world as world

print("8 files were found or downloaded successfully.")

# All versions need the above code.

global worldin
worldin = None

class App(ShowBase):
	def __init__(self):
		ShowBase.__init__(self)
		base.disableMouse()
		pass
	
	def mousewatchtask(self, worldin):
		if base.mouseWatcherNode.hasMouse():
			x = base.mouseWatcherNode.getMouseX()
			y = base.mouseWatcherNode.getMouseY()
			self.camera.setHpr(x, y, 0)
		self.camera.setPos(worldin.playerx, worldin.playerz, worldin.playery + 96)
		
	pass

def launch_k3d(self=None, worldname='world', lanhost=False): # worlds etc. need to be passed in as parameters here.
	# The code below executes when you open the version with the launcher.
	print('')
	print("Kettle3D development version d20-05 build A launched.")
	print('')
	k3d_window = App()

	coverImage = OnscreenImage(image=Filename.fromOsSpecific(directory).getFullpath() + 'assets/generating_level.png', pos=(0, 0, 0))
	loading_bar = DirectWaitBar(text="", value=0, pos=(0, 0, 0))

	if os.path.exists(directory + normpath("data/" + worldname + ".dat")):
		worldin = world.World(worldname, k3d_window, lb=loading_bar)
	else:
		worldin = world.new_World(worldname, k3d_window, lb=loading_bar)

	coverImage.destroy()

	while True:
		taskMgr.step()
		time.sleep(1 / 24)
		k3d_window.mousewatchtask(worldin)
