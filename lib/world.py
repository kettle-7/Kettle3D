# This library isn't done yet.

from os import system # system(String command) runs a batch command. Java function syntax :~)
from lib.launcherbase import directory
import lib.launcherbase
from lib.chunk import *
import pickle

class new_World:
	def __init__(self, name, renderer, size=[8, 4, 8], lb=None): # This should generate a cube of concrete and a cube of air on top of it.
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
					lb['value'] = 100 / self.sizex / self.sizey / self.sizez * chunkx * chunky * chunkz
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
		for chunkx in range(self.playerx / 16 - 5, self.playerx / 16 + 6):  # Generate worldmap
			self.worldmap.append([])
			for chunky in range(self.playery / 16 - 5, self.playery / 16 + 6):
				self.worldmap[chunkx].append([])
				for chunkz in range(self.playerz / 16 - 5, self.playerz / 16 + 6):
					self.worldmap[chunkx][chunky].append(None)
					self.worldmap[chunkx][chunky][chunkz] = chunk(self, chunkx, chunky, chunkz, renderer)
					self.worldmap[chunkx][chunky][chunkz].hidechunk(self, renderer)
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
		for chunkx in range(int(self.playerx / 16 - 5), int(self.playerx / 16 + 6)): # Load chunks
			for chunky in range(int(self.playery / 16 - 5), int(self.playery / 16 + 6)):
				for chunkz in range(int(self.playerz / 16 - 5), int(self.playerz / 16 + 6)):
					if chunkx >= 0 and chunky >= 0 and chunkz >= 0:
<<<<<<< HEAD
						try:
							self.worldmap[chunkx][chunky][chunkz] = chunk(self, chunkx, chunky, chunkz, renderer)
						except IndexError:
							print("There was an error at the following indexes:")
							print("%s, %s, %s" % (chunkx, chunky, chunkz))
=======
						self.worldmap[chunkx][chunky][chunkz] = chunk(self, chunkx, chunky, chunkz, renderer)
>>>>>>> parent of 0146837... Added debug
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
