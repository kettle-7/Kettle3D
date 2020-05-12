# This library isn't done yet.

from os import system # system(String command) runs a batch command. Java function syntax :~)
from lib.launcherbase import directory
import lib.launcherbase
from lib.chunk import *
import pickle

class new_World:
	"""
class new_World(name, renderer, size, lb)

This should generate a cube of concrete with a cube of air on top of it.

Parameters:

name -- The name of the world you want to generate. This will be the name of the folder that the world goes in,
and the .world file.

renderer -- The instance of ShowBase that will be used to render the blocks. Note that you can only have one.

size -- This is a list or tuple that contains the size of the world. The default values are 8, 4 and 8. This means the
overall world size will be 8x8x8, consisting of an 8x4x8 cube of concrete with and 8x4x8 cube of air above it.

lb -- This is the world's loading bar. This is so that users can see how much of the world has loaded -- basically, how
painstakingly slow the world generation process is. Look, I tried. The default value is None; if you don't specify this
parameter the game will crash though.
	"""
	def __init__(self, name, renderer, size=(8, 4, 8), lb=None):
		print("Executed command %s" % 'cd "' + directory + "data" + '"')
		system('cd "' + directory + "data" + '"')
		print("Executed command %s" % 'md "' + name + '"')
		system('md "' + name + '"')
		self.name = name
		self.displayname = name
		self.size = size
		self.sizex = size[0]
		self.sizey = size[1] * 2
		self.sizez = size[2]
		self.playerx = self.sizex / 2
		self.playery = self.sizey
		self.playerz = self.sizez / 2
		self.worldmap = []
		self.newfile = open(directory + normpath("data/" + self.name + ".world"), 'xb') # Create world file - saves everything but chunks.
		self.newfile.close()
		self.save()
		for chunkx in range(0, size[0]):
			self.worldmap.append([])
			for chunky in range(0, size[1]):
				self.worldmap[chunkx].append([])
				for chunkz in range(0, size[2]):
					self.worldmap[chunkx][chunky].append(newchunk(self, chunkx, chunky, chunkz, True, renderer))
					self.worldmap[chunkx][chunky][chunkz].hidechunk(self, renderer)
					lb['value'] = 100 / self.sizex / self.sizey / self.sizez * chunkx * chunky * chunkz / 2
					pass
				pass
			for chunky in range(size[1], size[1] * 2):
				self.worldmap[chunkx].append([])
				for chunkz in range(0, size[2]):
					self.worldmap[chunkx][chunky].append(newchunk(self, chunkx, chunky, chunkz, False, renderer))
					pass
				pass
			pass
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

		self.file = open(directory + normpath("data/" + self.name + ".world"), 'wb')
		pickle.dump(self.mapmap, self.file)
		self.file.close()
		pass
	
	def load(self, renderer):
		print(directory + normpath("data/") + self.name + ".dat")
		self.file = open(directory + normpath("data/" + self.name + ".world"), 'rb')
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
		print(self.worldmap)
		for chunkx in range(self.playerx / 16 - 5, self.playerx / 16 + 6):  # Generate worldmap
			self.worldmap.append([])
			for chunky in range(self.playery / 16 - 5, self.playery / 16 + 6):
				self.worldmap[chunkx].append([])
				for chunkz in range(self.playerz / 16 - 5, self.playerz / 16 + 6):
					self.worldmap[chunkx][chunky].append(chunk(self, abs(chunkx), abs(chunky), abs(chunkz), renderer))
					pass
				pass
			pass
		pass

	def is_move_valid(self):
		if self.worldmap[int(self.playerx / 16)][int(self.playery / 16)][int(self.playerz / 16)].chunkmap[int(self.playerx % 16)][int(self.playery % 16)][int(self.playerz % 16)].blocktype != 'air':
			return False
		else:
			return True
		pass

	def quit(self, renderer):
		self.save()
		for chunkx in range(int(self.playerx / 16 - 5), int(self.playerx / 16 + 6)):
			for chunky in range(int(self.playery / 16 - 5), int(self.playery / 16 + 6)):
				for chunkz in range(int(self.playerz / 16 - 5), int(self.playerz / 16 + 6)):
					self.worldmap[chunkx][chunky][chunkz].hidechunk(self, renderer)
					pass
				pass
			pass
		pass
		del self
		pass
	pass

# noinspection PyAttributeOutsideInit
class World: # ** is the Python exponent operator, not ^ - Kettle
	"""
class World(name, renderer, lb)

This will load the world from the previous game session.

Parameters:

name -- This is the name of the world file -- NOT the display name -- so that the world can be loaded.

renderer -- The instance of ShowBase that will be used to render the blocks. Note that you can only have one.

lb -- This is the world's loading bar. This is so that users can see how much of the world has loaded -- basically, how
painstakingly slow the world generation process is. Look, I tried. The default value is None; if you don't specify this
parameter the game will crash though.

Other parameters like size and displayname are included in the world file.
	"""
	def __init__(self, name, renderer, lb=None): # This should load the World from where you left off.
		self.name = name
		self.worldmap = []
		self.file = open(directory + normpath("data/" + self.name + ".world"), 'rb')
		self.mapmap = pickle.load(self.file)
		self.file.close()
		for map_entry in range(0, self.mapmap['playerx']):
			self.worldmap.append([])
			for map_entry2 in range(0, self.mapmap['playery']):
				self.worldmap.append([])
				for map_entry3 in range(0, self.mapmap['playerz']):
					self.worldmap.append('')
					lb['value'] = 100 / self.sizex / self.sizey / self.sizez * map_entry * map_entry2 * map_entry3 / 2
					pass
				pass
			pass
		self.load(renderer)
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
		
		print(directory + normpath("data/") + self.name + ".world")
		self.file = open(directory + normpath("data/" + self.name + ".world"), 'wb')
		pickle.dump(self.mapmap, self.file)
		self.file.close()
		pass
	
	def load(self, renderer):
		print(directory + normpath("data/") + self.name + ".world")
		self.file = open(directory + normpath("data/" + self.name + ".world"), 'rb')
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
		print(self.worldmap)
		for chunkx in range(int(self.playerx / 16 - 5), int(self.playerx / 16 + 6)): # Load chunks
			for chunky in range(int(self.playery / 16 - 5), int(self.playery / 16 + 6)):
				for chunkz in range(int(self.playerz / 16 - 5), int(self.playerz / 16 + 6)):
					if chunkx >= 0 and chunky >= 0 and chunkz >= 0:
						self.worldmap[chunkx][chunky][chunkz] = chunk(self, chunkx, chunky, chunkz, renderer)
					pass
				pass
			pass
		pass
	
	def quit(self, renderer):
		self.save()
		for chunkx in range(self.playerx / 16 - 5, self.playerx / 16 + 6):
			for chunky in range(self.playery / 16 - 5, self.playery / 16 + 6):
				for chunkz in range(self.playerz / 16 - 5, self.playerz / 16 + 6):
					self.worldmap[chunkx][chunky][chunkz].hidechunk(self, renderer)
					pass
				pass
			pass
		del self
		pass

	def loadchunks(self, renderer): # Unloads all chunks, and then loads the ones close to the player
		for chunkx in range(0, self.sizex):
			for chunky in range(0, self.sizey):
				for chunkz in range(0, self.sizez):
					if chunkx <= self.playerx * 16 - 5 or chunkx >= self.playerx * 16 + 6:
						self.worldmap[chunkx][chunky][chunkz].hidechunk()
					elif chunky <= self.playery * 16 - 5 or chunky >= self.playery * 16 + 6:
						self.worldmap[chunkx][chunky][chunkz].hidechunk()
					elif chunkz <= self.playerz * 16 - 5 or chunkz >= self.playerz * 16 + 6:
						self.worldmap[chunkx][chunky][chunkz].hidechunk()
		for chunkx in range(self.playerx / 16 - 5, self.playerx / 16 + 6): # Load chunks
			for chunky in range(self.playery / 16 - 5, self.playery / 16 + 6):
				for chunkz in range(self.playerz / 16 - 5, self.playerz / 16 + 6):
					self.worldmap[chunkx][chunky][chunkz] = chunk(self, chunkx, chunky, chunkz, renderer)
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
	pass
