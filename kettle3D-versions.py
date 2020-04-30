# Kettle3D Launcher v1.0

versionlist = {
	"dev" : [
		[1, 'd20.04a']
	],
	"stable" : [
		#none yet...
	]
}

# Updates need to be posted above with syntax as such:
# Developement versions go under "dev" and releases under "stable."
# dev[0][0] is the version number; 1 is the first version, 2 the second etc.
# The string contained within the list is this:
# ['d' for dev | None for 'stable'] + <year released> + '-' + <month released> + <one-letter build name>.
# Please only add a single-letter build name corresponding to the order in which that month's versions were made.
# Feel free to make a pull request - put your version in the list. DO NOT MARK IT AS STABLE UNTIL I HAVE TESTED IT.
# Put the programming for the version in Kettle3D/versions/d2004a.py. Add a txtfile object for all the text files, along with .py etc.
# Initialise the textfile under the play function. Make sure to add all required txtfiles and binaryfiles as well.

from urllib.request import urlopen
from urllib.error import URLError
from os.path import normpath
from sys import platform
from tkinter import *
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

try:
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
except(EOFError, FileNotFoundError, OSError):
	try:
		filelistfile = open(directory + normpath("assets/files.dat"), 'wb')
		files = {# This is the filearray. It stores all the information needed to find other files, whether binary or normal text.
			"binary" : [
				{# This is a file entry as provided by the downloadfile class. This entry belongs to the file array itself.
					"path" : "assets/files.dat",
					"version" : 1
				}
			],
			"txt" : [
			],
			"image" : [
			]
		}
		pickle.dump(files, filelistfile)
		print("Successfully created a new filearray.")
		filelistfile.close()
	except(FileNotFoundError, OSError):
		filelistfile = open(directory + normpath("assets/files.dat"), 'xb')
		files = {# This is the filearray. It stores all the information needed to find other files, whether binary or normal text.
			"binary" : [
				{# This is a file entry as provided by the downloadfile class. This entry belongs to the file array itself.
					"path" : "assets/files.dat",
					"version" : 1
				}
			],
			"txt" : [
			],
			"image" : []
		}
		pickle.dump(files, filelistfile)
		print("Successfully created a new filearray.")
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

class binaryfile():
	def __init__(self, path, version): # file for download
		self.path = path
		self.version = version
		self.winpath = normpath(self.path)
		print("Looking for file %s..." % path)
		try:
			self.newcontent = open(directory + self.winpath, 'wb')
			self.oldcontent = open(directory + self.winpath, 'rb')
			print("File %s found successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read()
				if self.oldcontent != self.onlinecontent:
					self.newcontent.write(self.onlinecontent)
					print("Successfully updated file.")
				else:
					print("File matches.")
			except URLError:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except(FileNotFoundError, OSError):
			self.newcontent = open(directory + winpath, 'xb')
			print("File %s created successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read()
				print("Successfully downloaded file.")
				fae = {
					"path" : self.path,
					"version" : self.version
				}
				files["binary"].append(fae)
				self.newcontent.write(self.onlinecontent)
			except URLError:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.newcontent.close()

class imagefile:
	def __init__(self, path, version): # Image for download
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
			files["image"].append(fae)
			print("File %s downloaded successfully." % self.path)
		except URLError:
			print("Couldn't download file. Maybe try checking your internet connection?")

isdiropen = False
isplayopen = False
dir_tk = None
play_tk = None
tk = Tk()
tk.title("Kettle3D Launcher")
tk.wm_attributes("-topmost", 1)
tk.configure(bg='black')
canvas = Canvas(tk, width=500, height=500, bd=0, highlightthickness=0)
canvas.pack()
tk.update()

print("The launcher window opened successfully.")

print("Have 2 files and 1 versions to check or download...")

if not {"path" : "lib/launcherbase.py", "version" : 2} in files["txt"]:
	downloadfile = txtfile(path='lib/launcherbase.py', version=2)
if not {"path" : "assets/k3dlauncher1.png", "version" : 1} in files["image"]:
	background1 = imagefile(path='assets/k3dlauncher1.gif', version=1)
if not {"path" : "versions/d2004a.py", "version" : 2} in files["txt"]:
	downloadfile = txtfile(path='versions/d2004a.py', version=2)

print("2 files and 1 versions downloaded with no errors :)")

files = pickle.load(open(directory + normpath("assets/files.dat"), 'rb'))
filelistfile = open(directory + normpath("assets/files.dat"), 'wb')
pickle.dump(files, filelistfile)
filelistfile.close

tk.configure(bg='#47ad73')
launcherbackground = PhotoImage(file=directory + background1.winpath)

def play():
	isplayopen = True
	play_tk = Tk()
	play_tk.title("Versions - Kettle3D Launcher")
	play_tk.wm_attributes("-topmost", 1)
	play_canvas = Canvas(play_tk, width=300, height=25)
	play_canvas.pack()
	play_canvas.create_text(150, 12, text="Development Versions:", font=('Helvetica', 20))
	v1btn = Button(play_tk, text="Play 20.04 build A", command=launchv1)
	v1btn.pack()
	play_tk.update_idletasks()
	play_tk.update()
	tk.update_idletasks()
	tk.update()

def launchv1():
	try:
		v1 = __import__("versions.d2004a")
		play_tk.destroy()
		isplayopen = False
		v1.launch_k3d()
	except ModuleNotFoundError:
		err_tk = Tk()
		err_canvas = Canvas(err_tk, width=300, height=25)
		err_canvas.pack()
		err_canvas.create_text(150, 13, text="The game crashed. :(", font=('Helvetica', 20))
		err_tk.update()

playbtn = Button(tk, text="PLAY", command=play)
playbtn.pack()
closebtn = Button(tk, text="Cancel", command=tk.destroy)
closebtn.pack

backgroundImage = canvas.create_image(0, 0, image=launcherbackground, anchor=NW)

while True:
	tk.update_idletasks()
	tk.update()
	if isplayopen:
		play_tk.update_idletasks()
		play_tk.update()
	time.sleep(0.01)
