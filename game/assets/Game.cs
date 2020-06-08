using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameObject ConcreteModel;
    public GameObject GrassModel;
    public GameObject DirtModel;
    public GameObject StoneModel;
    public GameObject K3DModel;
    public float xpos, ypos, zpos, movex, movey, movez;

    void Start()
    {
        var array = new int[16];
        List<GameObject> worldmap = new List<GameObject>();
        foreach (var item in array)
        {
            foreach (var item2 in array)
            {
                // Instantiate with the given coordinates and no rotation.
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
        this.movex = Input.GetAxis("Horizontal");
        this.movez = Input.GetAxis("Vertical");
        GameObject.Find("Main Camera").transform.Rotate(Input.mousePosition.y,Input.mousePosition.x,0);
        GameObject.Find("Main Camera").transform.Translate(this.movex, this.movey, this.movez);
    }

    void move()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(this.xpos, this.ypos, this.zpos);
    }
}