using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using System;

public class BlockSide : MonoBehaviour
{
    public String Facing;
    public GameObject SelfModel;
    public Game game;
    // Start is called before the first frame update
    void Start()
    {
        this.SelfModel = this.transform.parent.gameObject;
        game = GameObject.Find("Main Camera").GetComponent("Game") as Game;
    }

    // Update is called once per frame
    void Update()
    {
        // tab
    }

    void OnMouseOver() {
        if (!game.Playing) { return; }
        if(Input.GetMouseButtonDown(1)) {
            // Place a new block
            var GameScript = GameObject.Find("Main Camera").GetComponent("Game") as Game;
            var picked_block = GameScript.PickedBlock;
            var dis = Instantiate (picked_block, new Vector3(SelfModel.transform.position.x, SelfModel.transform.position.y, SelfModel.transform.position.z), Quaternion.identity);
            switch (Facing)
            {
                case "NORTH":
                    dis.transform.Translate(0f, 0f, 1f);
                    break;
                case "SOUTH":
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

            game.worldmap.Add(dis);
        } else if(Input.GetMouseButtonDown(0)) {
            // Destroy the block
            Destroy (this.transform.parent.gameObject);
            game.worldmap.Remove(this.transform.parent.gameObject);
        }
    }
}
