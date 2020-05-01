# This file stores the Block class and all of its children.

from direct.showbase.ShowBase import ShowBase

class Block(ShowBase):
	def __init__(xpos, ypos, zpos, blockpath):
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
		pass
	
	def destroy(self, chunk):
		pass

class concrete(Block):
	def __init__(self, chunk, xpos, ypos, zpos): # blockpath is expected to be provided within this class. Extra parameters are allowed such as blockstates.
		Block.__init__(xpos, ypos, zpos, 'models/concrete')
		self.blocktype = 'concrete'
		self.xpos = xpos
		self.ypos = ypos
		self.zpos = zpos
		self.blockpath = 'models/concrete'
		self.collision_box = CollisionBox((Point3(self.posx, self.posz, self.posy), Point3(self.posx * 64 + 64, self.posz * 64 + 64, self.posy * 64 + 64))
		pass
	
	def destroy(self, chunk):
		self.removenode()
		xpos = self.xpos
		ypos = self.ypos
		zpos = self.zpos
		chunk.chunkmap[xpos][ypos][zpos] = air(chunk, xpos, ypos, zpos)
