using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class BlockSide : MonoBehaviour
{
    public String Facing;
    public GameObject SelfModel;
    // Start is called before the first frame update
    void Start()
    {
        //tab
    }

    // Update is called once per frame
    void Update()
    {
        //tab
    }

    void OnMouseOver() {
        if(Input.GetMouseButtonDown(1)) {
            // Place a new block
            var dis = Instantiate (this.SelfModel);
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
