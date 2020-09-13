// This script is attached to all the sides of all the blocks
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using System;

public partial class BlockSide : MonoBehaviour
{
    public String Facing;        // A string that says which side of the block this is
    public GameObject SelfModel; // The model of the block that this script is part of
    public Game game;            // The 'Game' object
    // Start is called when the game loads
    void Start()
    {
        this.SelfModel = this.transform.parent.gameObject; // Set the SelfModel field to the GameObject of the Parent of the 
        // Transform (the block that this script is attached to. The reason we use the parent of the transform is because this
        // script is attached to a Mesh, which is part of the block. The parent is a GameObject that contains these Meshes.)
        game = GameObject.Find("Main Camera").GetComponent("Game") as Game; // This looks for the Game object, which is attached to our camera.
    }

    void OnMouseOver() { // This function gets called when the mouse is on top of this side of the block.
        if (!game.Playing) { return; } // If we're paused, we don't need to do this, so exit the function.
        if(Input.GetMouseButtonDown(1)) { // If the player is right-clicking:
            // Place a new block
            var GameScript = game; // We don't need this, we could just say Game.
            var picked_block = GameScript.PickedBlock; // Set picked_block to the block the player is holding
            // Set the variable 'dis' to a copy of picked_block, and set it's position to where this block is.
            var dis = Instantiate (picked_block, new Vector3(SelfModel.transform.position.x, SelfModel.transform.position.y, SelfModel.transform.position.z), Quaternion.identity);
            switch (Facing)
            { // Check what side of the block we are, so that we know where to put the new block we just made.
                case "NORTH": // If this is the NORTH side of the block:
                    dis.transform.Translate(0f, 0f, 1f); // Move the new block so that it's one block north of this one.
                    break;
                case "SOUTH": // And so on...
                    dis.transform.Translate(0f, 0f, -1f);
                    break;
                case "EAST":
                    dis.transform.Translate(1f, 0f, 0f);
                    break;
                case "WEST":
                    dis.transform.Translate(-1f, 0f, 0f);
                    break;
                case "TOP":
                    dis.transform.Translate(0f, 1f, 0f);
                    break;
                case "BOTTOM":
                    dis.transform.Translate(0f, -1f, 0f);
                    break;
            }
            dis.active = true;
            game.worldmap.Add(dis); // Add dis to the worldmap field of game. This is so that this block also gets saved when we quit the game.
        }
        if(Input.GetMouseButtonDown(0)) { // If the user is left-clicking, Destroy the block.
            Destroy (this.transform.parent.gameObject); // We want to destroy the whole block, not just this side of it. transform is the position of this object in the block, parent is the transform of the whole block and gameObject is the GameObject that a tranform (or script) is attached to.
            game.worldmap.Remove(this.transform.parent.gameObject); // And then we want to remove it from worldmap, so that we don't get issues later on.
        }
    }
}
