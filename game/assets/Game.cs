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
    public GameObject ConcreteModel, GrassModel, DirtModel, StoneModel, K3DModel, BrickModel, LightModel, OvenModel, SandModel, SnowModel, StoneBricksModel, PickedBlock;
    public Texture Concrete, Grass, Dirt, Stone, K3D, Brick, Light, Oven, Sand, Snow, StoneBricks;
    public float xpos, ypos, zpos, movex, movey, movez;
    private readonly System.Random random = new System.Random();
    public Canvas PlayingCanvas, NotPlayingCanvas;

    void Start()
    {
        PlayingCanvas.enabled = true;
        NotPlayingCanvas.enabled = false;
        #region Updater
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            if (!File.Exists(Environment.GetEnvironmentVariable("appdata") + @"\Microsoft\Windows\Start Menu\Programs\Kettle3D.lnk")) {
                MessageBox.Show("Kettle3D is updating...");
                var client = new WebClient();
                client.DownloadFile("https://github.com/Kettle3D/Kettle3D/releases/download/v1.1/Kettle3D_Windows.zip", Environment.GetEnvironmentVariable("temp") + @"\kettle3d_updater.zip");
                if (Directory.Exists(Environment.GetEnvironmentVariable("temp") + @"\kettle3d_updater")) {
                    foreach (var file in Directory.GetFiles(Environment.GetEnvironmentVariable("temp") + @"\kettle3d_updater")) {
                        File.Delete(file);
                    }
                    Directory.Delete(Environment.GetEnvironmentVariable("temp") + @"\kettle3d_updater");
                }
                ZipFile.ExtractToDirectory(Environment.GetEnvironmentVariable("temp") + @"\kettle3d_updater.zip", Environment.GetEnvironmentVariable("temp") + @"\kettle3d_updater");
                Process.Start(Environment.GetEnvironmentVariable("temp") + @"\kettle3d_updater\Kettle3D.exe");
                MessageBox.Show("Kettle3D has finished updating. Please restart the app and if this happens again, report it on the Kettle3D bug tracker.");
            }
        }
        #endregion

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
            if (Input.GetKeyDown("left")) {
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                PickedItem -= 1;
                if (PickedItem < 0) {
                    PickedItem = 10;
                }
                if (PickedItem > 10) {
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
            }
            if (Input.GetKeyDown("right")) {
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                PickedItem += 1;
                if (PickedItem < 0) {
                    PickedItem = 10;
                }
                if (PickedItem > 10) {
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
