using System.Collections.Generic;
using System.Collections;
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
            if (Facing == "NORTH")
                dis.transform.Translate(0f, 0f, 1f);
            else if (Facing == "SOUTH")
                dis.transform.Translate(0f, 0f, -1f);
            else if (Facing == "EAST")
                dis.transform.Translate(1f, 0f, 0f);
            else if (Facing == "WEST")
                dis.transform.Translate(-1f, 0f, 0f);
            else if (Facing == "TOP")
                dis.transform.Translate(0f, 1f, 0f);
            else if (Facing == "BOTTOM")
                dis.transform.Translate(0f, -1f, 0f);
        } else if(Input.GetMouseButtonDown(0)) {
            // Destroy the block
            Destroy (this.transform.parent.gameObject);
        }
    }
}
