using System.Collections.Generic;
using System.Collections;
using System.Windows;
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
    private readonly System.Random random = new System.Random();

    void Start()
    {
        List<GameObject> worldmap = new List<GameObject>();
        for (var item = -8f; item < 8f; item++)
        {
            for (var item2 = -8f; item2 < 8f; item2++)
            {
                // Instantiate with the given coordinates and no rotation.
                GameObject dis;
                if (random.Next(2) == 1) {
                    dis = Instantiate(StoneModel, new Vector3(item, 0f, item2), Quaternion.identity);
                } else {
                    dis = Instantiate(GrassModel, new Vector3(item, 0f, item2), Quaternion.identity);
                }
                // dis.transform.Translate(item, 0f, item2);
                worldmap.Add(dis);
            }
        }
        
        this.xpos = 0.0f;
        this.ypos = 1.0f;
        this.zpos = 0.0f;
        this.movex = 0.0f;
        this.movey = 0.0f;
        this.movez = 0.0f;
    }

    void Update()
    {
        
        GameObject.Find("Main Camera").transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));

        this.movex += Input.GetAxis("Horizontal") / 4;
        this.movez += Input.GetAxis("Vertical") / 4;
        this.movex = this.movex * 0.735f;
        this.movez = this.movez * 0.735f; //                                                                    \/ 0f - (Input.mousePosition.y - 380) / 2
        //GameObject.Find("Main Camera").transform.rotation = new Quaternion(0,(Input.mousePosition.x - 683f) / 2,0,180);
        GameObject.Find("Main Camera").transform.Translate(this.movex, this.movey, this.movez);
        GameObject.Find("Main Camera").transform.Translate(0, 1.5f - GameObject.Find("Main Camera").transform.position.y, 0);
        this.movey = 0f;
    }
}