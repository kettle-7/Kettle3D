versionlist = {
	"dev" : [
		[1, (20, 4, 'a')]
	],
	"stable" : [
		#not applicable
	]
}

from tkinter import *
from urllib.request import urlopen
import time
import sys

sys.path.append("C:\\Program Files\\Kettle3D")
directory = open("C:\\Program Files\\Kettle3D\\dir.txt").read()
sys.path.append(directory)

class downloadfile:
	def __init__(path, winpath, version): # file for download
		self.path = path
		self.version = version
		self.winpath = winpath # FileNotFoundError
		print("Looking for file %s..." % path)
		try:
			self.content = open(directory + winpath, 'w')
			print("File %s found successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				if self.content != self.onlinecontent:
					self.content.write(self.onlinecontent)
					print("Successfully updated file.")
				else:
					print("File matches.")
			except:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except FileNotFoundError:
			self.content = open(directory + winpath, 'x')
			print("File %s created successfully." % path)
			try:
				self.onlinecontent = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				if self.content != self.onlinecontent:
					self.content.write(self.onlinecontent)
					print("Successfully updated file.")
				else:
					print("File matches.")
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
