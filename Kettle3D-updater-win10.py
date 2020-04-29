from urllib.request import urlopen
from urllib.error import URLError
from os.path import normpath
from os import getenv
from os import getcwd
from os import name
import sys

osnamefile = normpath(getcwd() + "/osname.txt")
osname = open(osnamefile).read().strip()
launcher = None
o = None
print("Kettle3D Updater is launching for %s" % osname)

try:
#if True:
	launcherprogramming = urlopen("https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/kettle3D-versions.py").read().decode('utf-8')
	print("Checking for updates")

	try:
		if osname == 'windows':
			launcher = open(getenv("appdata") + "\\Kettle3D\\kettle3DLauncher.py", 'w')
			o = open("C:\\Program Files\\Kettle3D\\kettle3DLauncher.py", 'r')
			os.path[0] = getenv("appdata") + "\\Kettle3D"
		elif osname == 'os x':
			launcher = open(getenv("HOME") + "/Library/Application Support/Kettle3D/kettle3DLauncher.py", "w")
			o = open(getenv("HOME") + "/Library/Application Support/Kettle3D/kettle3DLauncher.py", 'r')
			os.path[0] = getenv("HOME") + "/Library/Application Support/Kettle3D"
		old_launcher = o.read()
		is_new = False
	except (FileNotFoundError, AttributeError):
		if osname == 'windows':
			launcher = open("C:\\Program Files\\Kettle3D\\kettle3DLauncher.py", 'x')
			os.path[0] = getenv("appdata") + "\\Kettle3D"
		if osname == 'os x':
			launcher = open(getenv("HOME") + "/Library/Application Support/Kettle3D/Kettle3D/kettle3DLauncher.py", 'x')
			os.path[0] = getenv("HOME") + "/Library/Application Support/Kettle3D"
		is_new = True
	
	try:
		launcher.write(launcherprogramming)
		launcher.close()
	except AttributeError:
		exec(launcherprogramming)
	
	if is_new:
		print("Successfully installed Kettle3D launcher")
	elif not old_launcher == launcherprogramming:
		print("Updated Kettle3D Successfully")
	else:
		print("Kettle3D is up to date")
except URLError:
	print("Kettle3D couldn't check for updates. Try checking your internet connection.")

sys.path.append(getcwd())
import kettle3DLauncher
