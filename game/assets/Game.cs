using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameObject ConcreteModel;
    public GameObject GrassModel;
    public float xpos, ypos, zpos, movex, movey, movez;
    public TextMesh xXxXxX, yYyYyY, zZzZzZ;

    void Start()
    {
        var array = new int[16];
        List<GameObject> worldmap = new List<GameObject>();
        // Instantiate with the given coordinates and no rotation.
        foreach (var item in array)
        {
            foreach (var item2 in array)
            {
                worldmap.Add(Instantiate(ConcreteModel, new Vector3(item, -1.5f, item2), Quaternion.identity));
            }
        }
        
        this.xpos = 0.0f;
        this.ypos = 1.0f;
        this.zpos = 0.0f;
        this.movex = 0.0f;
        this.movey = 0.0f;
        this.movez = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(Input.mousePosition.y,Input.mousePosition.x,0);

        if (Input.GetKeyDown(KeyCode.W)) {
            this.movex = this.movex / 0.73f;
            this.movez = this.movez / 0.73f;
            this.movez += 0.25f;
            this.xpos += this.movex;
            this.zpos += this.movez;
            this.move();
        } else if (Input.GetKeyDown(KeyCode.A)) {
            this.movex = this.movex / 0.73f;
            this.movez = this.movez / 0.73f;
            this.movex -= 0.25f;
            this.xpos += this.movex;
            this.zpos += this.movez;
            this.move();
        } else if (Input.GetKeyDown(KeyCode.S)) {
            this.movex = this.movex / 0.73f;
            this.movez = this.movez / 0.73f;
            this.movez -= 0.25f;
            this.xpos += this.movex;
            this.zpos += this.movez;
            this.move();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            this.movex = this.movex / 0.73f;
            this.movez = this.movez / 0.73f;
            this.movex += 0.25f;
            this.xpos += this.movex;
            this.zpos += this.movez;
            this.move();
        } else {
            this.movex = this.movex / 0.73f;
            this.movez = this.movez / 0.73f;
            this.xpos += this.movex;
            this.zpos += this.movez;
            this.move();
        }

        this.xXxXxX.text = "X: pos: " + this.xpos.ToString() + "velocity: " + this.movex.ToString();
        this.yYyYyY.text = "Y: pos: " + this.ypos.ToString() + "velocity: " + this.movey.ToString();
        this.zZzZzZ.text = "Z: pos: " + this.zpos.ToString() + "velocity: " + this.movez.ToString();
    }

    void move()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(this.xpos, this.ypos, this.zpos);
    }
}