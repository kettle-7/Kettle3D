# This library isn't done yet.

from os import system # system(String command) runs a batch command. Java function syntax :~)
from lib.launcherbase import directory
from lib.chunk import *
from lib.block import *
import pickle
from lib.block import air, concrete

class new_World:
	def __init__(self, name, renderer, size=[16, 128, 16]): # This should generate a cube of concrete and a cube of air on top of it.
		system('cd "' + directory + "data" + '"')
		print("Executed command %s" % 'cd "' + directory + "data" + '"')
		system('md "' + name + '"')
		print("Executed command %s" % 'md "' + name + '"')
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
			self.worldmap.append([])
			for chunky in range(0, size[1] - 1):
				self.worldmap[chunkx].append([])
				for chunkz in range(0, size[2]):
					self.worldmap[chunkx][chunky].append(newchunk(self, chunkx, chunky, chunkz, True, renderer))
					pass
				pass
			for chunky in range(size[1], size[1] * 2):
				self.worldmap[chunkx].append([])
				for chunkz in range(0, size[2]):
					self.worldmap[chunkx][chunky].append(newchunk(self, chunkx, chunky, chunkz, False, renderer))
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
					lch = chunk(self, chunkx, chunky, chunkz, renderer)
					pass
				pass
			pass
		pass

	def is_move_valid(self):
		if self.worldmap[int(self.playerx / 16)][int(self.playery / 16)][self.playerz / 16].chunkmap[int(self.playerx % 16)][int(self.playery % 16)][int(self.playerz % 16)].blocktype != 'air':
			return False
		else:
			return True
	pass

class World:
	def __init__(self, name, renderer): # This should load the World from where you left off.
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
	
	def load(self, renderer):
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
					lch = chunk(self, chunkx, chunky, chunkz, renderer)
					pass
				pass
			pass
		pass
	
	def quit(self, renderer):
		self.save()
		for chunkx in range(playerx / 16 - 5, playerx / 16 + 6): # Load chunks
			for chunky in range(playery / 16 - 5, playery / 16 + 6):
				for chunkz in range(playerz / 16 - 5, playerz / 16 + 6):
					self.worldmap[chunkx][chunky][chunkz].hidechunk(self, renderer)
					pass
				pass
			pass
		pass
		self.__delete__()
		pass
	
	def is_move_valid(self):
		if self.worldmap[int(self.playerx / 16)][int(self.playery / 16)][self.playerz / 16].chunkmap[int(self.playerx % 16)][int(self.playery % 16)][int(self.playerz % 16)].blocktype != 'air':
			return False
		else:
			return True
	pass
	pass
