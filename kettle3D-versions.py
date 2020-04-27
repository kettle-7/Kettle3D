versionlist = {
	"dev" : [
		[1, (20, 4, 'a')]
	],
	"stable" : [
		#not applicable
	]
}

# Updates need to be posted above with syntax as such:
# Developement versions go under "dev" and releases under "stable."
# dev[0][0] is the version number; 1 is the first version, 2 the second etc.
# The tuple contained within the array for the version is this:
# (<year released>, <month released>, <build name>). Please only add a single-letter build name corresponding to the order in which
# that month's versions were made.
# Feel free to make a pull request - put your version in the list. DO NOT MARK IT AS STABLE UNTIL I HAVE TESTED IT.

from tkinter import *
from urllib.request import urlopen
import time
import sys
import pickle

sys.path.append("C:\\Program Files\\Kettle3D")
directory = open("C:\\Program Files\\Kettle3D\\dir.txt").read()
sys.path.append(directory)

try:
	filelistfile = open(directory + "assets\\files.dat", 'rb')
	files = pickle.load(filelistfile)
	print("Successfully retrieved file array.")
	filelistfile.close()
except FileNotFoundError:
	filelistfile = open(directory + "assets\\files.dat", 'xb')
	files = {# This is the filearray. It stores all the information needed to find other files, whether binary or normal text.
		"binary" : [
			{# This is a file entry as provided by the downloadfile class. This entry belongs to the file array itself.
				"path" : "assets/files.dat",
				"winpath" : "assets\\files.dat",
				"version" : 1
			}
		],
		"txt" : [
		]
	}
	pickle.dump(files, filelistfile)
	print("Successfully created a new filearray.")
	filelistfile.close()

class txtfile:
	def __init__(self, path, winpath, version): # file for download
		self.path = path
		self.version = version
		self.winpath = winpath
		print("Looking for file %s..." % path)
		try:
			self.newcontent = open(directory + winpath, 'w')
			self.oldcontent = open(directory + winpath, 'r')
			print("File %s found successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				if self.oldcontent != self.onlinecontent:
					self.newcontent.write(self.onlinecontent)
					print("Successfully updated file.")
				else:
					print("File matches.")
			except:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except (FileNotFoundError, OSError):
			self.newcontent = open(directory + winpath, 'x')
			print("File %s created successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				print("Successfully downloaded file.")
				fae = {
					"path" : self.path,
					"winpath" : self.winpath,
					"version" : self.version
				}
				files[txt].append(fae)
			except:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.newcontent.close()

class binaryfile:
	def __init__(self, path, winpath, version): # file for download
		self.path = path
		self.version = version
		self.winpath = winpath
		print("Looking for file %s..." % path)
		try:
			self.newcontent = open(directory + winpath, 'wb')
			self.oldcontent = open(directory + winpath, 'rb')
			print("File %s found successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path, 'rb').read().decode('utf-8')
				if self.oldcontent != self.onlinecontent:
					self.newcontent.write(self.onlinecontent)
					print("Successfully updated file.")
				else:
					print("File matches.")
			except:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except (FileNotFoundError, OSError):
			self.content = open(directory + winpath, 'xb')
			print("File %s created successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path, 'rb').read().decode('utf-8')
				print("Successfully downloaded file.")
				fae = {
					"path" : self.path,
					"winpath" : self.winpath,
					"version" : self.version
				}
				files[txt].append(fae)
			except:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.content.close()

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

print("Have 1 files to update or download.")

assets_index = txtfile("assets/assets_index.py", "assets\index\\assets_index.py", 1)

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
	
	directory = open("C:\\Program Files\\Kettle3D\\dir.txt").read()
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
