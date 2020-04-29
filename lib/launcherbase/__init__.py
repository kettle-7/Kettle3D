from urllib.request import urlopen
from os.path import normpath
from os import getenv
from os import getcwd
import time
import sys
import pickle

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
			except:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except (FileNotFoundError, OSError):
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
			except:
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
			except:
				print("Couldn't update file. Maybe try checking your internet connection?")
		except (FileNotFoundError, OSError):
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
			except:
				print("Couldn't download file. Maybe try checking your internet connection?")
		finally:
			self.newcontent.close()

