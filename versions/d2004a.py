from urllib.request import urlopen
from urllib.error import URLError
from os.path import normpath
from sys import platform
from os import getenv
from os import getcwd
import pickle
import time
import sys
 
osnamefile = normpath(getcwd() + "/osname.txt")

osname = open(osnamefile).read()

directory = None

#if True:
if platform.startswith('win32') or platform.startswith('cygwin'): # do windows-specific things
	directory = getenv("appdata") + "\\Kettle3D\\"
	sys.path[0] = getenv("appdata") + "\\Kettle3D"
if platform.startswith('darwin'): #do apple-specific things
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

filelistfile = open(directory + normpath("assets/files.dat"), 'rb')
tempfiles = pickle.load(filelistfile)
if "image" in tempfiles:
	files = tempfiles
else:
	files = {
		"txt" : tempfiles["txt"],
		"binary" : tempfiles["binary"],
		"image" : []
	}
print("Successfully retrieved file array.")
	filelistfile.close()

class txtfile():
	def __init__(self, path, version, newcontent=None): # file for download
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
				files["txt"].append(fae)
				self.newcontent.write(self.onlinecontent)
			except URLError:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.newcontent.close()

if not {"path" : "lib/launcherbase.py", "version" : 2} in files["txt"]:
  downloadfile = txtfile(path='lib/launcherbase.py', version=2)
if not {"path" : "lib/world.py", "version" : 1} in files["txt"]:
  downloadfile = txtfile(path='lib/world.py', version=1)

import lib.launcherbase as launcherbase


# All versions need the above code.

def launch(self=None):
  # The code below executes when you open the version with the launcher.
  
  print("Kettle3D development version d20-04 build A launched.")