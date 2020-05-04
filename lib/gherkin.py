# A module used to save and load classes. SPECIFIC TO CLASSES USED BY KETTLE3D. DOES NOT WORK ON OTHER OBJECTS!
# Hopefully it works.

import pickle
try:
    import block
except ModuleNotFoundError:
    import lib.block as block

class chunkdummy:
    def __init__(self, x, y, z):
        self.xpos = x
        self.ypos = y
        self.zpos = z

def dump(obj, file, type='chunk'):
    if type == 'chunk':
        mapmap = {
            'chunkmap' : [],
            'xpos' : obj.xpos,
            'ypos' : obj.ypos,
            'zpos' : obj.zpos
        }
        for blockx in range(0, 16):
            mapmap.append([])
            for blocky in range(0, 16):
                mapmap[blockx].append([])
                for blockz in range(0, 16):
                    block = obj.chunkmap[blockx][blocky][blockz]
                    if block.blocktype != 'glass_wall': # Just in case an instance of Glass Wall gets passed into this function
                        block.facing = 0
                        block.half = 0
                        block.third = 0
                    blockmap = {
                        'blocktype' : block.blocktype,
                        'states' : {
                            'facing' : block.facing,
                            'half' : block.half,
                            'third' : block.third
                        }
                    }
                    mapmap[blockx][blocky].append(blockmap)
                    pass
                pass
            pass

        pickle.dump(mapmap, file)
        pass
    pass

def load(file, renderer=None, type='chunk'):
    if type == 'chunk':
        filemap = pickle.load(file)
        mapmap = {
            'chunkmap': [],
            'xpos' : filemap['xpos'],
            'ypos' : filemap['ypos'],
            'zpos' : filemap['zpos']
        }

        cdummy = chunkdummy(mapmap['xpos'], mapmap['ypos'], mapmap['zpos'])

        for blockx in range(0, 16):
            mapmap[chunkmap].append([])
            for blocky in range(0, 16):
                mapmap['chunkmap'][blockx].append([])
                for blockz in range(0, 16):
                    blockmap = filemap['chunkmap'][blockx][blocky][blockz]
                    if blockmap['blocktype'] == 'glass_wall': # Just in case an instance of Glass Wall gets passed into this function
                        mapmap['chunkmap'][blockx][blocky].append(block.concrete(cdummy, blockx, blocky, blockz, renderer, blockmap['facing'], blockmap['half'], blockmap['third']))
                    elif blockmap['blocktype'] == 'concrete':
                        mapmap['chunkmap'][blockx][blocky].append(block.concrete(cdummy, blockx, blocky, blockz, renderer))
                    else:
                        mapmap['chunkmap'][blockx][blocky].append(block.concrete(cdummy, blockx, blocky, blockz, renderer))
                    pass
                pass
            pass

        return mapmap
    pass
