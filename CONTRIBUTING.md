Contributing to Kettle3D
===

Thank you for helping make Kettle3D happen. It really means a lot to us. But there are a few things I'll need to go over with you before you submit that pull request.

- If this code is readable, please keep it that way. If it isn't, please don't make it worse.
- The large number of comments that are in our scripts are there for people who are new to using C#, and we're trying to help them understand what each line does. You, by all means, do not have to continue this standard if you don't want to. About a third(ish) of Game.cs is comments.
- When you submit a pull request (or issue), there are a few labels we've added. We did this for a reason. Our issues and pull requests are mainly sorted into these categories:
  * New blocks (we've had fifteen of these, and there's only seventeen blocks). These should have the **`block`** label.
  * Typos, bugs and suggestions for the website (we get a lot of these too; most of them submitted by me). These should have the **`website`** label.
  * An improvement to the game that is **not** a block should have the **`enhancement`** label.

Some things that'll make this a bit easier for you
---
Here is where we keep everything:
- The source code for the game is in the **`game`** folder on the C# branch. These paths are relative to this folder.
- Any .NET assemblies that you need to reference for any reason should go in [Assets/Assemblies](https://github.com/Kettle3D/Kettle3D/tree/C%23/game/Assets/Assemblies). So far this folder only contains Windows Forms stuff.
- The blocks are Unity prefabs. When you make a new block, please open the prefab assets for an existing block and copy the GameObject inside. Then follow these steps:
  1) Go **GameObject > Create Empty** in the Unity editor version 2019.3.1f1.
  2) Give that GameObject the name of your block.
  3) Drag it to the [blocks folder](https://github.com/Kettle3D/Kettle3D/tree/C%23/game/Assets/blocks). This makes the Prefab asset.
  4) Open the asset and paste the GameObject you copied.
  5) The positions will be offset. To fix this, make sure the block is centered around 0,0,0. This means that the maximum a coordinate for one of the meshes will be is 0.5, and the minimum -0.5.
  6) Drag your texture (which should be in the textures folder)  onto each side of the block in the Scene view. You might have to find it first.
  7) Add your block into the script. To do this you'll need to modify Game.cs a bit:
  ```csharp
  // I'm not showing the whole file.
  ----------------------------------------------------------------------------------------------------------------------------------------------------------------
  // line 40:
  public GameObject ConcreteModel, ···, <YourBlock>Model, PickedBlock; // Replace <YourBlock> with the name for your block.
  // There's a comment here
  public Texture Concrete, ···, <YourBlock>;
  ----------------------------------------------------------------------------------------------------------------------------------------------------------------
  // about line 190, in the part where it loads the levels.
      break;
  case /*make an id for your block, ideally one more than the one before.*/ 19: // If the block ID of this block matches the one you made
      i = Instantiate(<YourBlock>Model, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity); // Copy an instance of that block.
      worldmap.Add(i); // And add it to the map.
      break; // Preventing CS8070, control cannot fall out of switch statement
  ----------------------------------------------------------------------------------------------------------------------------------------------------------------
  // About line 370:
  if (PickedItem < 0) {
      PickedItem = 18; // You need to add one to this number.
  }
  // there's a comment here
  if (PickedItem > 18) { // And this one.
      PickedItem = 0;
  }
  ----------------------------------------------------------------------------------------------------------------------------------------------------------------
  // About line 460:
      break;
  case 19: // again, just make this one more than the previous number.
      PickedBlock = <YourBlock>Model;
      cursor_image.texture = <YourBlock>;
      break;
  ----------------------------------------------------------------------------------------------------------------------------------------------------------------
  // You'll need to repeat this at about lines 470 and 550.
  ----------------------------------------------------------------------------------------------------------------------------------------------------------------
  // About line 670, in the Save() function, an if-then-else statement checks what block each GameObject is before saving it.
  // In the if statement, it checks the start of each block's name. Make sure this is the name of the Unity model, NOT the block name. It is best if these are both the same though.
  // For example:
  else if (block.name.StartsWith("Tiles"))
      blockblock.blocktype = 18;
  else if (block.name.StartsWith("<The name of your UNITY MODEL, this is very important>"))
      blockblock.blocktype = 19;
  blocks.Add(blockblock);
  // The above is purely an example.
  ```
  The above is everything you should need to change on the scripting side of things, you may see other stuff but that's probably either deprecated or a bug.
  8) Propose the changes in the pull request. I am working on a way of making third-party mods for the game, so that you can make your own changes without 
- The 
