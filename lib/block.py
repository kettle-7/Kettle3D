# This file stores the Block class and all of its children.
# Please note that xpos, ypos and zpos are positions within the chunk, if you want the position within the world, use absx, absy and
# absz.

from direct.showbase.ShowBase import ShowBase
import lib.launcherbase as launcherbase
from panda3d.core import Filename

print('')

class Block():
	def __init__(self):
		pass
	
	def destroy(self, chunk):
		self.model.removenode()
		xpos = self.xpos
		ypos = self.ypos
		zpos = self.zpos
		chunk.chunkmap[xpos][ypos][zpos] = air(chunk, xpos, ypos, zpos)
		pass
	def lender(self, renderer):
		self.model = renderer.loader.loadModel(self.blockpath) # Make sure to add these variables to any children
		self.model.reparentTo(renderer.render)
		self.model.setPos(self.absx * 64, self.absz * 64, self.absy * 64) # Y and Z are reversed. This is intentional.
		pass
	def unlender(self, chunk):
		self.model.removeNode()
		chunk.chunkmap[self.xpos][self.ypos][self.zpos] = self
		del self
		pass
	pass

class air():
	def __init__(self, chunk, xpos, ypos, zpos, renderer): # air is a ghost block; it doesn't have a model.
		self.blocktype = 'air'
		self.xpos = xpos
		self.ypos = ypos
		self.zpos = zpos
		self.absx = chunk.xpos * 16 + xpos
		self.absy = chunk.ypos * 16 + ypos
		self.absz = chunk.zpos * 16 + zpos
		pass
	
	def destroy(self, chunk):
		pass
	
	def lender(self):
		pass

	def unlender(self):
		pass

class concrete(Block):
	def __init__(self, chunk, xpos, ypos, zpos, renderer): # blockpath is expected to be provided within this class. Extra parameters are allowed such as blockstates.
		self.absx = chunk.xpos * 16 + xpos
		self.absy = chunk.ypos * 16 + ypos
		self.absz = chunk.zpos * 16 + zpos
		self.chunk = chunk
		self.blocktype = 'concrete'
		self.xpos = xpos
		self.ypos = ypos
		self.zpos = zpos
		self.blockpath = 'assets/concrete'
		self.lender(renderer)

class glass_wall(Block):
	def __init__(self, chunk, xpos, ypos, zpos, renderer, facing, half, third): # blockpath is expected to be provided within this class. Extra parameters are allowed such as blockstates.
		self.absx = chunk.xpos * 16 + xpos
		self.absy = chunk.ypos * 16 + ypos
		self.absz = chunk.zpos * 16 + zpos
		self.chunk = chunk
		self.blocktype = 'glass_wall'
		self.xpos = xpos
		self.ypos = ypos
		self.zpos = zpos
		self.facing = facing
		self.blockpath = Filename.fromOsSpecific(launcherbase.directory).getFullpath() + "/assets/glass_wall_" + half + third + ".egg"
		pass
	
	def lender(self, renderer):
		self.model = renderer.loader.loadModel(self.blockpath) # Make sure to add these variables to any children
		self.model.reparentTo(self.render)
		self.model.setPos(self.absx * 64, self.absz * 64, self.absy * 64) # Y and Z are reversed. This is intentional.
		self.model.setHpr(self.facing * 90, 0, 0)
		pass
	pass
