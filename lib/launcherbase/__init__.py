osnamefile = normpath(getcwd() + "/osname.txt")

osname = open(osnamefile).read()

directory = None

#if True:
if osname == 'windows': # do windows-specific things
	directory = getenv("USERPROFILE") + "\\AppData\\Roaming\\Kettle3D\\"
if osname == 'os x': #do apple-specific things
	directory = getenv("HOME") + "/Library/Application Support/Kettle3D/"

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
files = pickle.load(filelistfile)
print("Successfully retrieved file array.")
filelistfile.close()
