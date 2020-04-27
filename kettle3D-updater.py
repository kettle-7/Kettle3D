from urllib.request import urlopen
import sys

try:
	launcherprogramming = urlopen("https://Kettle3D.github.io/Kettle3D/kettle3D-versions.py").read().decode('utf-8')
	print("Checking for updates")
	
	try:
		launcher = open("C:\\Program Files\\Kettle3D\\kettle3DLauncher.py", '+')
		old_launcher = launcher.read()
		is_new = False
	except:
		launcher = open("C:\\Program Files\\Kettle3D\\kettle3DLauncher.py", 'x')
		is_new = True

	launcher.write(launcherprogramming)
	launcher.close()
	if is_new:
		print("Successfully installed Kettle3D launcher")
	elif old_launcher != launcherprogramming:
		print("Updated Kettle3D Successfully")
	else:
		print("Kettle3D is up to date")
#except:
#	print("Kettle3D couldn't check for updates. Try checking your internet connection.")

sys.path.append("C:\\Program Files\\Kettle3D")

import kettle3DLauncher
