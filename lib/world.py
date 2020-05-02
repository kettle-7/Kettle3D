# This library isn't done yet.

from os import system # system(String command) runs a batch command. Java function syntax :~)
from lib.launcherbase import directory
from lib.chunk import *
from lib.block import *
import pickle

class new_World:
	def __init__(self, name, size=[16, 128, 16]): # This should generate a cube of concrete and a cube of air on top of it.
		system('cd "' + directory + "data" + '"')
		system('md "' + name + '"')
		self.name = name
		self.displayname = name
		self.size = size
		self.sizex = size[0]
		self.sizey = size[1]
		self.sizez = size[2]
		self.playerx = 0
		self.playery = 0
		self.playerz = 0
		self.worldmap = []
		for chunkx in range(0, size[0]):
			self.worldmap[chunkx] = []
			for chunky in range(0, size[1] - 1):
				self.worldmap[chunkx][chunky] = []
				for chunkz in range(0, size[2]):
					self.worldmap[chunkx][chunky][chunkz] = (newchunk(self, chunkx, chunky, chunkz, True))
					pass
				pass
			for chunky in range(size[1], size[1] * 2):
				self.worldmap[chunkx].append([])
				for chunkz in range(0, size[2]):
					self.worldmap[chunkx][chunky][chunkz] = (newchunk(self, chunkx, chunky, chunkz, False))
					pass
				pass
			pass
		
		print(directory + normpath("data/") + self.name + ".dat")
		self.newfile = open(directory + normpath("data/" + self.name + ".dat"), 'xb') # Create world file - saves everything but chunks.
		self.newfile.close()
		self.save()
		pass
	
	def save(self):
		self.mapmap = {
			"name" : self.name,
			"displayname" : self.displayname,
			"sizex" : self.sizex,
			"sizey" : self.sizey,
			"sizez" : self.sizez,
			"playerx" : self.playerx,
			"playery" : self.playery,
			"playerz" : self.playerz
		}
		
		print(directory + normpath("data/") + self.name + ".dat")
		self.file = open(directory + normpath("data/" + self.name + ".dat"), 'xb')
		pickle.dump(self.mapmap, self.file)
		self.file.close()
		pass
	
	def load(self):
		print(directory + normpath("data/") + self.name + ".dat")
		self.file = open(directory + normpath("data/" + self.name + ".dat"), 'rb')
		self.mapmap = pickle.load(self.file)
		self.file.close()
		
		self.name = self.mapmap['name']
		self.displayname = self.mapmap['displayname']
		self.sizex = self.mapmap['sizex']
		self.sizey = self.mapmap['sizey']
		self.sizez = self.mapmap['sizez']
		self.playerx = self.mapmap['playerx']
		self.playery = self.mapmap['playery']
		self.playerz = self.mapmap['playerz']
		for chunkx in range(playerx / 16 - 5, playerx / 16 + 6): # Load chunks
			for chunky in range(playery / 16 - 5, playery / 16 + 6):
				for chunkz in range(playerz / 16 - 5, playerz / 16 + 6):
					lch = chunk(self, chunkx, chunky, chunkz)
					pass
				pass
			pass
		pass
	pass

class World:
	def __init__(self, name): # This should load the World from where you left off.
		self.name = name
		self.load()
		pass
	
	def save(self):
		self.mapmap = {
			"name" : self.name,
			"displayname" : self.displayname,
			"sizex" : self.sizex,
			"sizey" : self.sizey,
			"sizez" : self.sizez,
			"playerx" : self.playerx,
			"playery" : self.playery,
			"playerz" : self.playerz
		}
		
		print(directory + normpath("data/") + self.name + ".dat")
		self.file = open(directory + normpath("data/" + self.name + ".dat"), 'wb')
		pickle.dump(self.mapmap, self.file)
		self.file.close()
		pass
	
	def load(self):
		print(directory + normpath("data/") + self.name + ".dat")
		self.file = open(directory + normpath("data/" + self.name + ".dat"), 'rb')
		self.mapmap = pickle.load(self.file)
		self.file.close()
		
		self.name = self.mapmap['name']
		self.displayname = self.mapmap['displayname']
		self.sizex = self.mapmap['sizex']
		self.sizey = self.mapmap['sizey']
		self.sizez = self.mapmap['sizez']
		self.playerx = self.mapmap['playerx']
		self.playery = self.mapmap['playery']
		self.playerz = self.mapmap['playerz']
		for chunkx in range(playerx / 16 - 5, playerx / 16 + 6): # Load chunks
			for chunky in range(playery / 16 - 5, playery / 16 + 6):
				for chunkz in range(playerz / 16 - 5, playerz / 16 + 6):
					lch = chunk(self, chunkx, chunky, chunkz)
					pass
				pass
			pass
		pass
	
	def quit(self):
		self.save()
		self.__delete__()
		pass
	pass
