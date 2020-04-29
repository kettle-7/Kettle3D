versionlist = {
	"dev" : [
		[1, (20, 4, 'a')]
	],
	"stable" : [
		#none yet...
	]
}

# Updates need to be posted above with syntax as such:
# Developement versions go under "dev" and releases under "stable."
# dev[0][0] is the version number; 1 is the first version, 2 the second etc.
# The tuple contained within the array for the version is this:
# (<year released>, <month released>, <build name>). Please only add a single-letter build name corresponding to the order in which
# that month's versions were made.
# Feel free to make a pull request - put your version in the list. DO NOT MARK IT AS STABLE UNTIL I HAVE TESTED IT.
# Put the programming for the version in Kettle3D/versions/d20-04a.py. Add a txtfile object for all the text files, along with .py etc.
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
	directory = getenv("USERPROFILE") + "\\AppData\\Roaming\\Kettle3D\\"
if platform.startswith('darwin'): #do apple-specific things
	directory = getenv("HOME") + "/Library/Application Support/Kettle3D/"
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
			"txt" : tempfiles["txt"]
			"binary" : tempfiles["binary"]
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
			]
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
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path, 'rb').read()
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
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path, 'rb').read()
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
	def __init__(path, version):
		self.path = path
		self.version = version
		winpath = normpath(path)
		self.winpath = winpath
		print("Looking for file %s" % path)
		img_data = urlopen("https://github.com/Kettle3D/Kettle3D/raw/master/" + path, 'rb').content
		with open(directory + normpath(path), 'wb') as handler:
			handler.write(img_data)
			handler.close()
		fae = {
			"path" : self.path,
			"version" : self.version
		}
		files["image"].append(fae)
		print("File %s downloaded successfully.")

isdiropen = False
isplayopen = False
dir_tk = None
play_tk = None
tk = Tk()
tk.title("Kettle3D Launcher")
tk.wm_attributes("-topmost", 1)
canvas = Canvas(tk, width=500, height=500, bd=0, highlightthickness=0)
canvas.pack()
tk.update()

print("The launcher window opened successfully.")

print("Have 2 files to check or download...")

if not {"path" : "lib/launcherbase.py", "version" : 1} in files["txt"]:
	downloadfile = txtfile(path='lib/launcherbase.py', version=1)
if not {"path" : "assets/k3dlauncher1.png", "version" : 1} in files["image"]:
	downloadfile = imagefile(path='assets/k3dlauncher1.png', version=1)

files = pickle.load(open(directory + normpath("assets/files.dat"), 'rb'))
filelistfile = open(directory + normpath("assets/files.dat"), 'wb')
pickle.dump(files, filelistfile)
filelistfile.close

def play():
	isplayopen = True
	play_tk = Tk()
	play_tk.title("Versions - Kettle3D Launcher")
	play_tk.wm_attributes("-topmost", 1)
	play_canvas = Canvas(play_tk, width=250, height=250)
	play_canvas.pack()
	play_tk.update()
	tk.update()
	
	
def closedirwin():
	if isdiropen:
		dir_tk.destroy()
		dir_tk = None
		isdiropen = False

def launch():
	play_tk.destroy()
	isplayopen = False
	
def dir():
	# Change directory
	
	isdiropen = True
	
	dir_tk = Tk()
	dir_tk.title("Change Directory - Kettle3D Launcher")
	dir_tk.wm_attributes("-topmost", 1)
	dir_canvas = Canvas(dir_tk, width=500, height=20)
	dir_canvas.pack()
	dir_tk.update()
	tk.update()
	dirtxt = dir_canvas.create_text(250, 11, text="The directory is set to %s." % directory, font=('Helvetica', 15))
	
choosedir = Button(tk, text="Change Directory", command=dir)
playbtn = Button(tk, text="PLAY", command=play)
choosedir.pack()
playbtn.pack()
closebtn = Button(tk, text="Cancel", command=closedirwin)

while True:
	tk.update_idletasks()
	tk.update()
	if isdiropen:
		dir_canvas.itemconfig(dirtxt, x=dir_tk.winfo_width(), text="The directory is set to %s." % directory)
		dir_tk.update_idletasks()
		dir_tk.update()
	if isplayopen:
		play_tk.update_idletasks()
		play_tk.update()
	time.sleep(0.01)
