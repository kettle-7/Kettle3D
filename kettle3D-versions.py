# Kettle3D Launcher v1.0, Mr. T pities the fool :~)

versionlist = {
	"dev": [
		[0, 'd2005a', 'alpha-dev 20.05 build A']
	],
	"stable": [
		# none yet...
	]
}

displayVersionList = []

for v in versionlist["dev"]:
	displayVersionList.append(v[2])

'''
Updates need to be posted above with syntax as such:

dev:

[version_number, version_file]

version_number is an integer that tells Kettle3D what order the versions come in -- 1 is the first version, 2 is the 
second etc.

version_file is the name of the file within ./versions that the version is in. Put 'deprecated' if the version is not
for use. The name for the version should be something like 'd2005b' -- d for development, 20 for the year, 05 for the
month (May), and 'a' because it is build A. The build name should be a single lowercase letter from a to z corresponding
to the order in which that month's versions were made.

stable:

[version_number, version_file]

version_number is just as above. Note that if version 1 and 2 are dev versions, then if I make a stable release, then
it'll be version 3. This is so that the launcher can list the versions in the order that they were made. This feature is
planned.

version_file is as above, except the name should follow semantic versioning, starting with 1.0, then 1.1, and so on. If
a release is not big enough to require a new version number, name it 1.3a etc. Unlike with dev versions, the version
number has nothing to do with the month it was released, just the order.
'''

from panda3d.core import ConfigVariableString, ConfigVariableInt
from urllib.request import urlopen
from urllib.error import URLError
from tkinter.ttk import Combobox
from os import getenv, getcwd
from os.path import normpath
from sys import platform
from lib.tools import *
from tkinter import *
import pickle
import time
import sys

directory = None

# if True:
if platform.startswith('win32') or platform.startswith('cygwin'):  # do windows-specific things
	directory = getenv("appdata") + "\\Kettle3D\\"
	sys.path[0] = getcwd()
if platform.startswith('darwin'):  # do apple-specific things
	directory = getenv("HOME") + "/Library/Application Support/Kettle3D/"
	sys.path[0] = getcwd()
	sys.path.append(getenv("HOME") + "/Library/Developer/Panda3D")


class file_dummy:
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
			"txt": tempfiles["txt"],
			"binary": tempfiles["binary"],
			"image": []
		}
	print("Successfully retrieved file array.")
	filelistfile.close()
except(EOFError, FileNotFoundError, OSError):
	try:
		filelistfile = open(directory + normpath("assets/files.dat"), 'wb')
		files = {  # This is the filearray. It stores all the information needed to find other files, whether
			"binary": [  # binary or normal text.
				{
					# This is a file entry as provided by the downloadfile class. This entry belongs to the file array.
					"path": "assets/files.dat",
					"version": 1
				}
			],
			"txt": [
			],
			"image": [
			]
		}
		pickle.dump(files, filelistfile)
		print("Successfully created a new filearray.")
		filelistfile.close()
	except(FileNotFoundError, OSError):
		filelistfile = open(directory + normpath("assets/files.dat"), 'xb')
		files = {  # This is the filearray. It stores all the information needed to find other files, whether
			"binary": [  # binary or normal text.
				{
					# This is a file entry as provided by the downloadfile class. This entry belongs to the file array.
					"path": "assets/files.dat",
					"version": 1
				}
			],
			"txt": [
			],
			"image": []
		}
		pickle.dump(files, filelistfile)
		print("Successfully created a new filearray.")
		filelistfile.close()

try:
	settingsfile = open(directory + normpath("assets/settings.dat"), 'rb')
	settings = pickle.load(settingsfile)
	print("Successfully retrieved preferences file.")
	settingsfile.close()
except(EOFError, FileNotFoundError, OSError):
	try:
		settings = {  # This is the preferences file. It stores all of the user's settings.
			"config": {
				"index": ["fullscreen", "log_level", "c++_log_level", "gl_log_level", "world", "username"],
				"fullscreen": 1,
				"log_level": "warning",
				"c++_log_level": "warning",
				"gl_log_level": "warning",
				"world": 'world',
				"username": "player",
				"version": 'alpha-dev 20.05 build A'
			}
		}
		settingsfile = open(directory + normpath("assets/settings.dat"), 'wb')
		pickle.dump(settings, settingsfile)
		print("Successfully created a new preferences file.")
		settingsfile.close()
	except(FileNotFoundError, OSError):
		settings = {  # This is the preferences file. It stores all of the user's settings.
			"config": {
				"index": ["fullscreen", "log_level", "c++_log_level", "gl_log_level", "world", "username"],
				"fullscreen": 1,
				"log_level": "warning",
				"c++_log_level": "warning",
				"gl_log_level": "warning",
				"world": 'world',
				"username": "player",
				"version": 'alpha-dev 20.05 build A'
			}
		}
		settingsfile = open(directory + normpath("assets/settings.dat"), 'xb')
		pickle.dump(settings, settingsfile)
		print("Successfully created a new preferences file.")
		settingsfile.close()

if settings['config']['fullscreen'] == '#t' or settings['config']['fullscreen'] == '#f':
	print('User setting \'fullscreen\' was invalid :~(')
	settings['config']['fullscreen'] = 1


class txtfile:
	def __init__(self, path, version, newcontent=None):  # file for download
		self.path = path
		self.version = version
		self.winpath = normpath(self.path)
		self.newcontent = newcontent
		print("Looking for file %s..." % path)
		try:
			self.newcontent = open(directory + self.winpath, 'w')
			self.oldcontent = open(directory + self.winpath)
			print("File %s found successfully." % path)
			try:
				self.onlinecontent = urlopen(
					"https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				if self.oldcontent != self.onlinecontent and self.onlinecontent is not None and self.onlinecontent.strip() != '':
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
				self.onlinecontent = urlopen(
					"https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
				print("Successfully downloaded file.")
				fae = {
					"path": self.path,
					"version": self.version
				}
				files["txt"].append(fae)
				self.newcontent.write(self.onlinecontent)
			except URLError:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.newcontent.close()


class binaryfile:
	def __init__(self, path, version):  # file for download
		self.path = path
		self.version = version
		self.winpath = normpath(self.path)
		print("Looking for file %s..." % path)
		try:
			self.newcontent = open(directory + self.winpath, 'wb')
			self.oldcontent = open(directory + self.winpath, 'rb')
			print("File %s found successfully." % path)
			try:
				self.onlinecontent = urlopen(
					"https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read()
				if self.oldcontent != self.onlinecontent:
					self.newcontent.write(self.onlinecontent)
					print("Successfully updated file.")
				else:
					print("File matches.")
			except URLError:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except(FileNotFoundError, OSError):
			self.newcontent = open(directory + self.winpath, 'xb')
			print("File %s created successfully." % path)
			try:
				self.onlinecontent = urlopen(
					"https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read()
				print("Successfully downloaded file.")
				fae = {
					"path": self.path,
					"version": self.version
				}
				files["binary"].append(fae)
				self.newcontent.write(self.onlinecontent)
			except URLError:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.newcontent.close()


# noinspection PyPep8Naming
class imagefile:
	"""Downloads an image"""

	def __init__(self, path, version):  # Image for download
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
				"path": self.path,
				"version": self.version
			}
			files["image"].append(fae)
			print("File %s downloaded successfully." % self.path)
		except URLError:
			print("Couldn't download file. Maybe try checking your internet connection?")


class tkdummy:
	"""A class that does nothing"""

	def __init__(self):
		pass

	def destroy(self):
		"""x"""
		pass

	def update(self):
		"""y"""
		pass

	def update_idletasks(self):
		"""z"""
		pass

	pass


isdiropen = False
isplayopen = False
ioo = False
fullscreenBtn = None
dir_tk = None
play_tk = tkdummy()
tk = Tk()
tk.title("Kettle3D Launcher")
tk.wm_attributes("-topmost", 1)
tk.configure(bg='#47ad73')
canvas = Canvas(tk, width=500, height=500, bd=0, highlightthickness=0)
canvas.pack()
tk.update()

# username_entry = None
# world_entry = None
llBtn = None
cllBtn = None
gllBtn = None

title = ConfigVariableString('window-title', 'Kettle3D')
fullscreen = ConfigVariableInt('fullscreen', settings["config"]["fullscreen"])
notify_level = ConfigVariableString('default-directnotify-level', settings['config']["log_level"])
c_notify_level = ConfigVariableString('notify-level', settings['config']["c++_log_level"])
glgsg_notify_level = ConfigVariableString('notify-level-glgsg', settings['config']['gl_log_level'])

files = pickle.load(open(directory + normpath("assets/files.dat"), 'rb'))

print("The launcher window opened successfully.")

print("Have 2 files and 2 versions to check or download...")

downloadfile = txtfile(path='lib/launcherbase.py', version=4)
background1 = imagefile(path='assets/k3dlauncher1.gif', version=1)
downloadfile = txtfile(path='versions/d2004a.py', version=6)
downloadfile = txtfile(path='versions/d2005a.py', version=10)

print('')
print("2 files and 2 versions downloaded with no errors :)")
print('')

filelistfile = open(directory + normpath("assets/files.dat"), 'wb')
pickle.dump(files, filelistfile)
filelistfile.close()

launcherbackground = PhotoImage(file=directory + background1.winpath)


def play():
	van = settings['config']['version'].strip()
	vsn = parseRawInt(van)
	if van.startswith('alpha-dev'):
		vsn = 'd' + vsn
	if van.endswith('A') or van.endswith('a'):
		vsn += 'a'
	launch(vsn)


def launch(vsn='d2005a'):
	play_tk.destroy()
	tk.destroy()
	versionstr = "versions." + vsn
	print("Attempting to launch version %s at %s." % (versionstr, time.asctime()))
	version = __import__("versions", fromlist=[vsn])
	eval('version.%s.launch_k3d(worldname=settings["config"]["world"])' % vsn)


def unoptions1():
	global tk
	tk.destroy()
	del tk


def oo(aye):
	if aye == "#t" or aye is True or aye == 1:
		return "ON"
	elif aye == "#f" or aye is False or aye == 0:
		return "OFF"


def toggle_log_level(log_type):
	if log_type == 'gl':
		l = settings['config']['gl_log_level']
		if l == 'warning':
			l = 'error'
		elif l == 'error':
			l = 'fatal'
		elif l == 'fatal':
			l = 'spam'
		elif l == 'spam':
			l = 'debug'
		elif l == 'debug':
			l = 'info'
		elif l == 'info':
			l = 'warning'
		settings['config']['gl_log_level'] = l
		gllBtn.config(text='OpenGL Log Output Level: %s' % l)
		glgsg_notify_level.setValue(l)
		pass
	elif log_type == 'c++':
		l = settings['config']['c++_log_level']
		if l == 'warning':
			l = 'error'
		elif l == 'error':
			l = 'fatal'
		elif l == 'fatal':
			l = 'spam'
		elif l == 'spam':
			l = 'debug'
		elif l == 'debug':
			l = 'info'
		elif l == 'info':
			l = 'warning'
		settings['config']['c++_log_level'] = l
		cllBtn.config(text='C++ Log Output Level: %s' % l)
		c_notify_level.setValue(l)
		pass
	elif log_type == 'python':
		l = settings['config']['log_level']
		if l == 'warning':
			l = 'error'
		elif l == 'error':
			l = 'debug'
		elif l == 'debug':
			l = 'info'
		elif l == 'info':
			l = 'warning'
		settings['config']['log_level'] = l
		llBtn.config(text='Game Log Output Level: %s' % l)
		notify_level.setValue(l)
		pass
	pass


def toggle_fullscreen():
	if settings["config"]['fullscreen'] == 1:
		settings['config']["fullscreen"] = 0
		fullscreenBtn.config(text="Fullscreen: OFF")
		fullscreen.setValue(0)
	else:
		settings['config']["fullscreen"] = 1
		fullscreenBtn.config(text="Fullscreen: ON")
		fullscreen.setValue(1)
	pass


def unoptions2():
	global ioo
	ioo = False
	global tk
	tk = Tk()
	tk.title("Kettle3D Launcher")
	tk.wm_attributes("-topmost", 1)
	tk.configure(bg='#47ad73')
	global canvas
	canvas = Canvas(tk, width=500, height=500, bd=0, highlightthickness=0)
	canvas.pack()
	tk.update()

	playbtn = Button(tk, text="PLAY (%s)" % settings['config']['version'], command=play)
	playbtn.pack()
	optionsbtn = Button(tk, text="Options", command=options)
	optionsbtn.pack()

	fullscreen.setValue(settings["config"]["fullscreen"])
	notify_level.setValue(settings['config']["log_level"])
	c_notify_level.setValue(settings['config']["c++_log_level"])
	glgsg_notify_level.setValue(settings['config']['gl_log_level'])

	if worldVar.get() is not None and worldVar.get() != '':
		settings['config']['world'] = worldVar.get()
	if userVar.get() is not None and userVar.get() != '':
		settings['config']['username'] = userVar.get()
	settings['config']['version'] = versionVar.get()

	settingsfile = open(directory + normpath("assets/settings.dat"), 'wb')
	pickle.dump(settings, settingsfile)
	print("Updated %s's preferences." % settings['config']['username'])
	settingsfile.close()

	global launcherbackground
	global backgroundImage
	launcherbackground = PhotoImage(file=directory + background1.winpath)
	backgroundImage = canvas.create_image(0, 0, image=launcherbackground, anchor=NW)
	tk.update_idletasks()
	tk.update()


def unoptions():
	unoptions1()
	unoptions2()


def options1():
	global tk
	tk.destroy()
	del tk


def options2():
	global ioo, tk, fullscreenBtn
	ioo = True
	tk = Tk()
	tk.configure(bg='#47ad73')
	tk.title("Kettle3D Launcher")
	fullscreenBtn = Button(tk, text="Fullscreen: %s" % oo(settings['config']['fullscreen']), command=toggle_fullscreen)
	fullscreenBtn.pack()
	global llBtn, cllBtn, gllBtn, worldVar, userVar, versionVar, world_label, username_label
	llBtn = Button(tk, text='Game Log Output Level: %s' % settings['config']['log_level'],
				   command=lambda: toggle_log_level('python'))
	llBtn.pack()
	cllBtn = Button(tk, text='C++ Log Output Level: %s' % settings['config']['log_level'],
					command=lambda: toggle_log_level('c++'))
	cllBtn.pack()
	gllBtn = Button(tk, text='OpenGL Log Output Level: %s' % settings['config']['log_level'],
					command=lambda: toggle_log_level('gl'))
	gllBtn.pack()

	world_label = Label(tk, text="World name:")
	username_label = Label(tk, text="Username:")

	userVar = StringVar()
	userVar.set(settings['config']['username'])
	worldVar = StringVar()
	worldVar.set(settings['config']['world'])
	versionVar = StringVar()
	versionVar.set(settings['config']['version'])
	try:
		versionVar.set(settings['config']['version'])
	except KeyError:
		versionVar.set('alpha-dev 20.05 build A')
		settings['config']['version'] = 'alpha-dev 20.05 build A'

	global world_entry
	world_entry = Entry(tk, textvariable=worldVar)
	global username_entry
	username_entry = Entry(tk, textvariable=userVar)
	world_label.pack()
	world_entry.pack()
	username_label.pack()
	username_entry.pack()

	versionPicker = Combobox(tk, textvariable=versionVar, values=displayVersionList)

	backBtn = Button(tk, text="Done", command=unoptions)
	backBtn.pack()


def options():
	options1()
	options2()


#	except ModuleNotFoundError:
#		err_tk = Tk()
#		err_canvas = Canvas(err_tk, width=300, height=25)
#		err_canvas.pack()
#		err_canvas.create_text(150, 13, text="The game crashed. :(", font=('Helvetica', 20))
#		err_tk.update()

playbtn = Button(tk, text="PLAY (%s)" % settings['config']['version'], command=play)
playbtn.pack()
optionsbtn = Button(tk, text="Options", command=options)
optionsbtn.pack()

backgroundImage = canvas.create_image(0, 0, image=launcherbackground, anchor=NW)

while True:
	tk.update_idletasks()
	tk.update()
	try:
		play_tk.update_idletasks()
		play_tk.update()
	except TclError:
		pass
	time.sleep(0.01)
