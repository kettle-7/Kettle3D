using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO.Compression;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using System.IO;
using System;

public class Game : MonoBehaviour
{
    public GameObject ConcreteModel, GrassModel, DirtModel, StoneModel, K3DModel, BrickModel, LightModel, OvenModel, SandModel, SnowModel, StoneBricksModel, PresentModel, PickedBlock;
    public Texture Concrete, Grass, Dirt, Stone, K3D, Brick, Light, Oven, Sand, Snow, StoneBricks, Present;
    public float xpos, ypos, zpos, movex, movey, movez;
    private readonly System.Random random = new System.Random();
    public Canvas PlayingCanvas, NotPlayingCanvas;

    void Start()
    {
        PlayingCanvas.enabled = true;
        NotPlayingCanvas.enabled = false;

        //if(!this.Load()) {
        if(true) {
            for (var item = -16f; item < 16f; item++)
            {
                for (var item2 = -16f; item2 < 16f; item2++)
                {
                    // Instantiate with the given coordinates and no rotation.
                    //if (random.Next(2) == 1) {
                    //    Instantiate(StoneModel, new Vector3(item, 0f, item2), Quaternion.identity);
                    //} else {
                        Instantiate(GrassModel, new Vector3(item, 0f, item2), Quaternion.identity);
                    //}
                }
            }
        }
        PickedBlock = BrickModel;
        this.xpos = 0.0f;
        this.ypos = 1.5f;
        this.zpos = 0.0f;
        this.movex = 0.0f;
        this.movey = 0.0f;
        this.movez = 0.0f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        PickedItem = 5;
    }

    public int PickedItem = 5;
    public bool Playing = true;

    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            Playing = !Playing;
            if (Playing) {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            } else {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
            }
        }
        PlayingCanvas.enabled = Playing;
        NotPlayingCanvas.enabled = !Playing;
        if (Playing) {
            if (Input.GetKeyDown("left") || Input.GetAxis("Mouse ScrollWheel") < 0) {
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                PickedItem -= 1;
                if (PickedItem < 0) {
                    PickedItem = 11;
                }
                if (PickedItem > 11) {
                    PickedItem = 0;
                }
                if (PickedItem == 0) {
                    PickedBlock = ConcreteModel;
                    cursor_image.texture = Concrete;
                }
                if (PickedItem == 1) {
                    PickedBlock = GrassModel;
                    cursor_image.texture = Grass;
                }
                if (PickedItem == 2) {
                    PickedBlock = DirtModel;
                    cursor_image.texture = Dirt;
                }
                if (PickedItem == 3) {
                    PickedBlock = StoneModel;
                    cursor_image.texture = Stone;
                }
                if (PickedItem == 4) {
                    PickedBlock = K3DModel;
                    cursor_image.texture = K3D;
                }
                if (PickedItem == 5) {
                    PickedBlock = BrickModel;
                    cursor_image.texture = Brick;
                }
                if (PickedItem == 6) {
                    PickedBlock = LightModel;
                    cursor_image.texture = Light;
                }
                if (PickedItem == 7) {
                    PickedBlock = OvenModel;
                    cursor_image.texture = Oven;
                }
                if (PickedItem == 8) {
                    PickedBlock = SandModel;
                    cursor_image.texture = Sand;
                }
                if (PickedItem == 9) {
                    PickedBlock = SnowModel;
                    cursor_image.texture = Snow;
                }
                if (PickedItem == 10) {
                    PickedBlock = StoneBricksModel;
                    cursor_image.texture = StoneBricks;
                }
                if (PickedItem == 11) {
                    PickedBlock = PresentModel;
                    cursor_image.texture = Present;
                }
            }
            if (Input.GetKeyDown("right") || Input.GetAxis("Mouse ScrollWheel") > 0) {
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                PickedItem += 1;
                if (PickedItem < 0) {
                    PickedItem = 11;
                }
                if (PickedItem > 11) {
                    PickedItem = 0;
                }
                if (PickedItem == 0) {
                    PickedBlock = ConcreteModel;
                    cursor_image.texture = Concrete;
                }
                if (PickedItem == 1) {
                    PickedBlock = GrassModel;
                    cursor_image.texture = Grass;
                }
                if (PickedItem == 2) {
                    PickedBlock = DirtModel;
                    cursor_image.texture = Dirt;
                }
                if (PickedItem == 3) {
                    PickedBlock = StoneModel;
                    cursor_image.texture = Stone;
                }
                if (PickedItem == 4) {
                    PickedBlock = K3DModel;
                    cursor_image.texture = K3D;
                }
                if (PickedItem == 5) {
                    PickedBlock = BrickModel;
                    cursor_image.texture = Brick;
                }
                if (PickedItem == 6) {
                    PickedBlock = LightModel;
                    cursor_image.texture = Light;
                }
                if (PickedItem == 7) {
                    PickedBlock = OvenModel;
                    cursor_image.texture = Oven;
                }
                if (PickedItem == 8) {
                    PickedBlock = SandModel;
                    cursor_image.texture = Sand;
                }
                if (PickedItem == 9) {
                    PickedBlock = SnowModel;
                    cursor_image.texture = Snow;
                }
                if (PickedItem == 10) {
                    PickedBlock = StoneBricksModel;
                    cursor_image.texture = StoneBricks;
                }
                if (PickedItem == 11) {
                    PickedBlock = PresentModel;
                    cursor_image.texture = Present;
                }
            }

            GameObject.Find("Camera Container").transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
            GameObject.Find("Main Camera").transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y"));

            this.movex += Input.GetAxis("Horizontal") / 4;
            this.movey += Input.GetAxis("Up/Down") / 4;
            this.movez += Input.GetAxis("Vertical") / 4;
            this.movex = this.movex * 0.735f;
            this.movez = this.movez * 0.735f;
            this.movey = this.movey * 0.735f;
            GameObject.Find("Camera Container").transform.Translate(this.movex, this.movey, this.movez);
        }
    }
}
