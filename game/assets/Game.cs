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
using UnityEditor; // cheeky
using System.Net;
using System.IO;
using System;

public class Game : MonoBehaviour
{
    public GameObject ConcreteModel, GrassModel, DirtModel, StoneModel, K3DModel, BrickModel, LightModel, PickedBlock;
    public float xpos, ypos, zpos, movex, movey, movez;
    private readonly System.Random random = new System.Random();

    void Start()
    {
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

    void Update()
    {
        if (Input.GetKeyDown("space")) { this.movey += 0.25f; }
        if (Input.GetKeyDown("left shift")) { this.movey -= 0.25f; }

        if (Input.GetKeyDown("1"))
            PickedBlock = ConcreteModel;
        if (Input.GetKeyDown("2"))
            PickedBlock = GrassModel;
        if (Input.GetKeyDown("3"))
            PickedBlock = DirtModel;
        if (Input.GetKeyDown("4"))
            PickedBlock = StoneModel;
        if (Input.GetKeyDown("5"))
            PickedBlock = K3DModel;
        if (Input.GetKeyDown("6"))
            PickedBlock = BrickModel;
        if (Input.GetKeyDown("7"))
            PickedBlock = LightModel;

        GameObject.Find("Camera Container").transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
        GameObject.Find("Main Camera").transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y"));

        this.movex += Input.GetAxis("Horizontal") / 4;
        this.movez += Input.GetAxis("Vertical") / 4;
        this.movex = this.movex * 0.735f;
        this.movez = this.movez * 0.735f;
        this.movey = this.movey * 0.735f;
        GameObject.Find("Camera Container").transform.Translate(this.movex, this.movey, this.movez);
    }

    void OnApplicationQuit() {
        this.Save();
    }

    void Save() {
        var worldin = SceneManager.GetSceneByName("SampleScene");
        PrefabUtility.SaveAsPrefabAssetAndConnect(worldin, UnityEngine.Application.persistentDataPath + "/Saved/world.prefab", InteractionMode.AutomatedAction);
    }

    bool Load() {
        if (!File.Exists(UnityEngine.Application.persistentDataPath + "/Saved/world.json")) { return false; }
        var SavedWorld = File.ReadAllText(UnityEngine.Application.persistentDataPath + "/Saved/world.json");
        var objects = new GameObject[0];
        JsonUtility.FromJsonOverwrite(SavedWorld, objects);
        foreach (var thing in objects) {
            Instantiate(thing);
        }
        return true;
    }
}