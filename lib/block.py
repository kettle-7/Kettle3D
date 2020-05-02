# This file stores the Block class and all of its children.
# Please note that xpos, ypos and zpos are positions within the chunk, if you want the position within the world, use absx, absy and
# absz.

from direct.showbase.ShowBase import ShowBase
import lib.launcherbase as launcherbase
from panda3d.core import Filename

class Block(ShowBase):
	def __init__(self, xpos, ypos, zpos, blockpath):
		self.model = self.loader.loadModel(blockpath) # Make sure to add these variables to any children
		self.scene.reparentTo(self.render)
		self.scene.setPos(xpos * 64, zpos * 64, ypos * 64) # Y and Z are reversed. This is intentional.
		pass
	pass

class air():
	def __init__(self, chunk, xpos, ypos, zpos): # air is a ghost block; it doesn't have a model.
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

class concrete(Block):
	def __init__(self, chunk, xpos, ypos, zpos): # blockpath is expected to be provided within this class. Extra parameters are allowed such as blockstates.
		self.absx = chunk.xpos * 16 + xpos
		self.absy = chunk.ypos * 16 + ypos
		self.absz = chunk.zpos * 16 + zpos
		super().__init__(self.absx, self.absy, self.absz, blockpath=Filename.fromOsSpecific(launcherbase.directory).getFullpath() + "/assets/concrete.egg")
		self.blocktype = 'concrete'
		self.xpos = xpos
		self.ypos = ypos
		self.zpos = zpos
		self.blockpath = 'assets/concrete'
		self.reparentTo(render)
		self.collision_box = CollisionBox((Point3(self.posx, self.posz, self.posy), Point3(self.posx * 64 + 64, self.posz * 64 + 64, self.posy * 64 + 64)))
	def destroy(self, chunk):
		self.removenode()
		xpos = self.xpos
		ypos = self.ypos
		zpos = self.zpos
		chunk.chunkmap[xpos][ypos][zpos] = air(chunk, xpos, ypos, zpos)
		pass
	def lender(self):
		self.reparentTo(self.render)
		self.scene.setPos(xpos, zpos, ypos)
	def unlender(self, chunk):
		self.removenode()
		chunk.chunkmap[xpos][ypos][zpos] = self
		self.__delete__()

class glass_wall(concrete):
	def __init__(self, chunk, xpos, ypos, zpos, facing, half, third): # blockpath is expected to be provided within this class. Extra parameters are allowed such as blockstates.
		self.absx = chunk.xpos * 16 + xpos
		self.absy = chunk.ypos * 16 + ypos
		self.absz = chunk.zpos * 16 + zpos
		Block.__init__(self.absx, self.absy, self.absz, Filename.fromOsSpecific(launcherbase.directory).getFullpath() + "/assets/glass_" + half + third + ".egg")
		self.blocktype = 'glass_wall'
		self.xpos = xpos
		self.ypos = ypos
		self.zpos = zpos
		self.facing = facing
		self.blockpath = 'assets/glass'
		self.setHpr(facing * 90, 0, 0)
		self.reparentTo(render)
		self.collision_box = CollisionBox((Point3(self.posx, self.posz, self.posy), Point3(self.posx * 64 + 64, self.posz * 64 + 64, self.posy * 64 + 64)))
		pass
	
	def lender(self):
		self.reparentTo(self.render)
		self.scene.setPos(xpos, zpos, ypos)
	def unlender(self, chunk):
		self.removenode()
		chunk.chunkmap[xpos][ypos][zpos] = self
		self.__delete__()

# That's it for now...
