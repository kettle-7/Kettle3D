# The class used to save, load and query data from sections of a world.

from os import system # system(String command) runs a batch command
import lib.launcherbase as laucherbase
from os.path import normpath
import lib.block as block
from lib.block import *
import pickle

class newchunk:
	def __init__(self, world, xpos, ypos, zpos, isground, renderer):
		self.xpos = xpos
		self.ypos = ypos
		self.zpos = zpos
		self.chunkmap=[]
		for blockx in range(0, 16):
			self.chunkmap.append([])
			for blocky in range(0, 16):
				self.chunkmap[blockx].append([])
				for blockz in range(0, 16):
					if isground:
						self.chunkmap[blockx][blocky].append(block.concrete(self, blockx, blocky, blockz, renderer))
					else:
						self.chunkmap[blockx][blocky].append(block.air(self, blockx, blocky, blockz, renderer))
					pass
				pass
			pass
		
		self.mapmap = {
			'chunkmap' : self.chunkmap,
			'xpos' : self.xpos,
			'ypos' : self.ypos,
			'zpos' : self.zpos
		}
		
		self.newfile = open(directory + normpath("data/") + world.name + normpath("/chunk") + self.xpos + "_" + self.ypos + "_" + self.zpos + ".dat", 'xb')
		pickle.dump(self.mapmap, self.newfile)
		self.newfile.close()
		pass
	def save(self, world):
		self.mapmap = {
			'chunkmap' : self.chunkmap,
			'xpos' : self.xpos,
			'ypos' : self.ypos,
			'zpos' : self.zpos
		}
		
		self.file = open(directory + normpath("data/") + world.name + normpath("/chunk") + self.xpos + "_" + self.ypos + "_" + self.zpos + ".dat", 'wb')
		pickle.dump(self.mapmap, self.file)
		self.file.close()
		pass
	pass
	
	def hidechunk(self, world, renderer): # Used so that when a player gets too far away from a chunk, the chunk removes all of its nodes.
		self.save(world)    # Useful so that the user's RAM doesn't get too full. You can re-load the chunk with load(world)
		
		for blockx in self.chunkmap:
			for blocky in self.chunkmap[blockx]:
				for blockz in self.chunkmap[blockx][blocky]:
					self.chunkmap[blockx][blocky][blockz].unlender(renderer)
					pass
				pass
			pass
		self.__delete__()
		pass
	pass

class chunk:
	def load(self, world, renderer):
		self.file = open(directory + normpath("data/") + world.name + normpath("/chunk") + self.xpos + "_" + self.ypos + "_" + self.zpos + ".dat", 'rb')
		self.mapmap = pickle.load(self.file)
		self.file.close()
		self.chunkmap = self.mapmap['chunkmap']
		self.xpos = self.mapmap['xpos']
		self.ypos = self.mapmap['ypos']
		self.zpos = self.mapmap['zpos']
		for blockx in self.chunkmap:
			for blocky in self.chunkmap[blockx]:
				for blockz in self.chunkmap[blockx][blocky]:
					self.chunkmap[blockx][blocky][blockz].lender(renderer)
					pass
				pass
			pass
		pass
	
	def save(self, world):
		self.mapmap = {
			'chunkmap' : self.chunkmap,
			'xpos' : self.xpos,
			'ypos' : self.ypos,
			'zpos' : self.zpos
		}
		
		self.file = open(directory + normpath("data/") + world.name + normpath("/chunk") + self.xpos + "_" + self.ypos + "_" + self.zpos + ".dat", 'wb')
		pickle.dump(self.mapmap, self.file)
		self.file.close()
		pass
	pass
	
	def __init__(self, world, x, y, z, renderer):
		self.xpos = x
		self.ypos = y
		self.zpos = z
		self.load(world, renderer)
		pass
	
	def hidechunk(self, world, renderer): # Used so that when a player gets too far away from a chunk, the chunk removes all of its nodes.
		self.save(world)    # Useful so that the user's RAM doesn't get too full. You can re-load the chunk with load(world)
		
		for blockx in self.chunkmap:
			for blocky in self.chunkmap[blockx]:
				for blockz in self.chunkmap[blockx][blocky]:
					self.chunkmap[blockx][blocky][blockz].unlender(renderer)
					pass
				pass
			pass
		self.__delete__()
		pass
	pass

# That's it for now...
