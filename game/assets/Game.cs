using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameObject ConcreteModel;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(ConcreteModel, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.rotation = Quaternion.Euler(Input.mousePosition.x,Input.mousePosition.y,0);
        Console.WriteLine(Input.mousePosition.x);
        Console.WriteLine(Input.mousePosition.y);
    }
}
