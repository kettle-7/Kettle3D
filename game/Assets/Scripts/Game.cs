using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
//using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;
using System.Collections;
using System.Xml.Linq;
using System.Windows;
using UnityEngine.UI;
using UnityEngine;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System;

using Debug = UnityEngine.Debug;

public partial class Game : MonoBehaviour
{
    public GameObject ConcreteModel, GrassModel, DirtModel, StoneModel, K3DModel, BrickModel, LightModel, OvenModel, SandModel, SnowModel, StoneBricksModel, PresentModel, GlassModel, HayModel, PickedBlock;
    public Texture Concrete, Grass, Dirt, Stone, K3D, Brick, Light, Oven, Sand, Snow, StoneBricks, Present, Glass, Hay;
    public float xpos, ypos, zpos, movex, movey, movez;
    public List<GameObject> worldmap = new List<GameObject>();
    private readonly System.Random random = new System.Random();
    public Canvas PlayingCanvas, NotPlayingCanvas;
    private string savefile;
    public static Game game;

    void Awake() {
        game = this;
    }

    void Load() {
        if (File.Exists($"{Application.persistentDataPath}/saves/{savefile}.dat")) {
            FileStream fs = new FileStream($"{Application.persistentDataPath}/saves/{savefile}.dat", FileMode.Open);
            List<Block> blocks = new List<Block>();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and
                // assign the reference to the local variable.
                blocks = (List<Block>) formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            foreach (Block block in blocks) {
                GameObject i;
                switch (block.blocktype) {
                    case BlockType.Concrete:
                        i = Instantiate(ConcreteModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Bricks:
                        i = Instantiate(BrickModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Dirt:
                        i = Instantiate(DirtModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Glass:
                        i = Instantiate(GlassModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Grass:
                        i = Instantiate(GrassModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Hay:
                        i = Instantiate(HayModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.K3D:
                        i = Instantiate(K3DModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Light:
                        i = Instantiate(LightModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Oven:
                        i = Instantiate(OvenModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Prensent:
                        i = Instantiate(PresentModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Sand:
                        i = Instantiate(SandModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Snow:
                        i = Instantiate(SnowModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.Stone:
                        i = Instantiate(StoneModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case BlockType.StoneBricks:
                        i = Instantiate(StoneBricksModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                }
            }
        }

        else {
        //if(true) {
            for (var item = -16f; item < 16f; item++)
            {
                for (var item2 = -16f; item2 < 16f; item2++)
                {
                    // Instantiate with the given coordinates and no rotation.
                    //if (random.Next(2) == 1) {
                    //    Instantiate(StoneModel, new Vector3(item, 0f, item2), Quaternion.identity);
                    //} else {
                        worldmap.Add(Instantiate(GrassModel, new Vector3(item, 0f, item2), Quaternion.identity));
                    //}
                }
            }
        }
    }

    void Start()
    {
        savefile = LevelManagement.management.Level;
        Debug.Log(Application.installerName);
        Debug.Log(Application.installMode);
        PlayingCanvas.enabled = true;
        NotPlayingCanvas.enabled = false;
        Load();
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

    void OnApplicationQuit() {
        Save();
    }

    public void ResetLevel() {
        foreach (var o in worldmap.ToArray()) {
            Destroy(o);
            worldmap.Remove(o);
        }
        File.Delete($"{Application.persistentDataPath}/saves/{savefile}.dat");
        SceneManager.LoadScene("Scenes/LoadingScreen");
    }

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
                    PickedItem = 13;
                }
                if (PickedItem > 13) {
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
                if (PickedItem == 12) {
                    PickedBlock = GlassModel;
                    cursor_image.texture = Glass;
                }
                if (PickedItem == 13) {
                    PickedBlock = HayModel;
                    cursor_image.texture = Hay;
                }
            }
            if (Input.GetKeyDown("right") || Input.GetAxis("Mouse ScrollWheel") > 0) {
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                PickedItem += 1;
                if (PickedItem < 0) {
                    PickedItem = 13;
                }
                if (PickedItem > 13) {
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
                if (PickedItem == 12) {
                    PickedBlock = GlassModel;
                    cursor_image.texture = Glass;
                }
                if (PickedItem == 13) {
                    PickedBlock = HayModel;
                    cursor_image.texture = Hay;
                }
            }

            GameObject.Find("Camera Container").transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
            GameObject.Find("Main Camera").transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y"));

            movex += Input.GetAxis("Horizontal") / 4;
            movey += Input.GetAxis("Up/Down") / 4;
            movez += Input.GetAxis("Vertical") / 4;
            movex = movex * 0.735f;
            movez = movez * 0.735f;
            movey = movey * 0.735f;
            GameObject.Find("Camera Container").transform.Translate(movex, movey, movez);
        }
    }

    public void Save()
    {
        FileStream fs = new FileStream($"{Application.persistentDataPath}/saves/{savefile}.dat", FileMode.Create);
        List<Block> blocks = new List<Block>();
        foreach (var block in worldmap)
        {
            var blockblock = new Block
            {
                posx = block.transform.position.x,
                posy = block.transform.position.y,
                posz = block.transform.position.z
            };

            if (block.name.StartsWith("brick"))
                blockblock.blocktype = BlockType.Bricks;
            else if (block.name.StartsWith("concrete"))
                blockblock.blocktype = BlockType.Concrete;
            else if (block.name.StartsWith("dirt"))
                blockblock.blocktype = BlockType.Dirt;
            else if (block.name.StartsWith("GlassBlock"))
                blockblock.blocktype = BlockType.Glass;
            else if (block.name.StartsWith("grass"))
                blockblock.blocktype = BlockType.Grass;
            else if (block.name.StartsWith("Hay"))
                blockblock.blocktype = BlockType.Hay;
            else if (block.name.StartsWith("k3d"))
                blockblock.blocktype = BlockType.K3D;
            else if (block.name.StartsWith("light"))
                blockblock.blocktype = BlockType.Light;
            else if (block.name.StartsWith("Oven"))
                blockblock.blocktype = BlockType.Oven;
            else if (block.name.StartsWith("Present"))
                blockblock.blocktype = BlockType.Prensent;
            else if (block.name.StartsWith("Sand"))
                blockblock.blocktype = BlockType.Sand;
            else if (block.name.StartsWith("Snow"))
                blockblock.blocktype = BlockType.Snow;
            else if (block.name.StartsWith("stone"))
                blockblock.blocktype = BlockType.Stone;
            else if (block.name.StartsWith("StoneBricks"))
                blockblock.blocktype = BlockType.StoneBricks;
            blocks.Add(blockblock);
        }
        // Next
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, blocks);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }
}

public enum BlockType
{
    Concrete,
    Grass,
    Dirt,
    Stone,
    K3D,
    Bricks,
    Light,
    Oven,
    Sand,
    Snow,
    StoneBricks,
    Prensent,
    Glass,
    Hay
}

[Serializable]
public class Block
{
    public float posx { get; set; }
    public float posy { get; set; }
    public float posz { get; set; }
    public BlockType blocktype { get; set; }
}
