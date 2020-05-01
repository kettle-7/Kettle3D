# The class used to save, load and query data from sections of a world.

from os import system # system(String command) runs a batch command
import pickle
import lib.block as block
import lib.launcherbase as laucherbase

class newchunk:
	def __init__(self, world, xpos, ypos, zpos, isground):
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
						self.chunkmap[blockx][blocky].append(concrete(self, blockx, blocky, blockz))
					else:
						self.chunkmap[blockx][blocky].append(concrete(self, blockx, blocky, blockz))
					pass
				pass
			pass
		
		self.mapmap = {
			'chunkmap' : self.chunkmap,
			'xpos' : self.xpos,
			'ypos' : self.ypos,
			'zpos' : self.zpos
		}
		
		system("cd " + launcherbase.directory + "data")
		system("mkdir " + world.name)
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

class chunk:
	def load(self, world):
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
					self.chunkmap[blockx][blocky][blockz].lender()
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
	
	def __init__(self, world):
		self.load(world)
		pass
	pass

# That's it for now...
