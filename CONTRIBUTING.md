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
- If you would like to add a block, you have two options:
  1. Make a mod for the game that adds your block. For instructions on how to do this, see [Kettle3D.github.io/mods](https://Kettle3D.github.io/mods)
  2. Add your block into the base game. To do this, edit the files for the mod called `default`.
- Any block textures you have go in the **`textures`** folder of your mod.

Thank you in advance for contributing to our game.
