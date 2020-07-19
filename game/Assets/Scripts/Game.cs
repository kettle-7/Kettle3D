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
    public GameObject ConcreteModel, GrassModel, DirtModel, StoneModel, K3DModel, BrickModel, LightModel, OvenModel, SandModel, SnowModel, StoneBricksModel, PresentModel, GlassModel, HayModel, GlowingModel, LeavesModel, LogModel, DoNotTouchModel, PickedBlock;
    public Texture Concrete, Grass, Dirt, Stone, K3D, Brick, Light, Oven, Sand, Snow, StoneBricks, Present, Glass, Hay, Leaves, Glowing, Log, DoNotTouch;
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
            List<Block> legacyBlocks = new List<Block>();
            List<BlockRewrite> blocks = new List<BlockRewrite>();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the level
                var level = (LevelFile) formatter.Deserialize(fs);
                GameObject.Find("Camera Container").transform.Translate(level.playerx, level.playery, level.playerz);
                blocks = (List<BlockRewrite>) level.worldmap;
            }
            //catch (SerializationException e)
            //{
            //    Debug.LogError("Failed to deserialize. Reason: " + e.Message);
            //    throw;
            //}
            catch // Old format
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the level
                legacyBlocks = (List<Block>) formatter.Deserialize(fs);
            }
            finally
            {
                fs.Close();
            }

            foreach (BlockRewrite block in blocks) {
                GameObject i;
                switch (block.blocktype) { // We don't know if the level uses the BlockType enum or a Byte ID, so we test both.
                    case 0:
                        i = Instantiate(ConcreteModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 1:
                        i = Instantiate(BrickModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 2:
                        i = Instantiate(DirtModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 3:
                        i = Instantiate(GlassModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 4:
                        i = Instantiate(GrassModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 5:
                        i = Instantiate(HayModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 6:
                        i = Instantiate(K3DModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 7:
                        i = Instantiate(LightModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 8:
                        i = Instantiate(OvenModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 9:
                        i = Instantiate(PresentModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 10:
                        i = Instantiate(SandModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 11:
                        i = Instantiate(SnowModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 12:
                        i = Instantiate(StoneModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 13:
                        i = Instantiate(StoneBricksModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 14:
                        i = Instantiate(GlowingModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 15:
                        i = Instantiate(LeavesModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 16:
                        i = Instantiate(LogModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    case 17:
                        i = Instantiate(DoNotTouchModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        worldmap.Add(i);
                        break;
                    default:
                        Debug.LogWarning("This level has been saved in a newer version. Any blocks that do not exist in this version will not render.");
                        break;
                }
            }
            foreach (Block block in legacyBlocks) {
                GameObject i;
                switch (block.blocktype) { // We don't know if the level uses the BlockType enum or a Byte ID, so we test both.
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
                    PickedItem = 17;
                }
                if (PickedItem > 17) {
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
                if (PickedItem == 14) {
                    PickedBlock = GlowingModel;
                    cursor_image.texture = Glowing;
                }
                if (PickedItem == 15) {
                    PickedBlock = LeavesModel;
                    cursor_image.texture = Leaves;
                }
                if (PickedItem == 16) {
                    PickedBlock = LeavesModel;
                    cursor_image.texture = Leaves;
                }
                if (PickedItem == 17) {
                    PickedBlock = DoNotTouchModel;
                    cursor_image.texture = DoNotTouch;
                }
            }
            if (Input.GetKeyDown("right") || Input.GetAxis("Mouse ScrollWheel") > 0) {
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                PickedItem += 1;
                if (PickedItem < 0) {
                    PickedItem = 17;
                }
                if (PickedItem > 17) {
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
                if (PickedItem == 14) {
                    PickedBlock = GlowingModel;
                    cursor_image.texture = Glowing;
                }
                if (PickedItem == 15) {
                    PickedBlock = LeavesModel;
                    cursor_image.texture = Leaves;
                }
                if (PickedItem == 16) {
                    PickedBlock = LeavesModel;
                    cursor_image.texture = Leaves;
                }
                if (PickedItem == 17) {
                    PickedBlock = DoNotTouchModel;
                    cursor_image.texture = DoNotTouch;
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
        List<BlockRewrite> blocks = new List<BlockRewrite>();
        foreach (var block in worldmap)
        {
            var blockblock = new BlockRewrite
            {
                posx = block.transform.position.x,
                posy = block.transform.position.y,
                posz = block.transform.position.z
            };

            if (block.name.StartsWith("brick"))
                blockblock.blocktype = 1;
            else if (block.name.StartsWith("concrete"))
                blockblock.blocktype = 0;
            else if (block.name.StartsWith("dirt"))
                blockblock.blocktype = 2;
            else if (block.name.StartsWith("GlassBlock"))
                blockblock.blocktype = 3;
            else if (block.name.StartsWith("grass"))
                blockblock.blocktype = 4;
            else if (block.name.StartsWith("Hay"))
                blockblock.blocktype = 5;
            else if (block.name.StartsWith("k3d"))
                blockblock.blocktype = 6;
            else if (block.name.StartsWith("light"))
                blockblock.blocktype = 7;
            else if (block.name.StartsWith("Oven"))
                blockblock.blocktype = 8;
            else if (block.name.StartsWith("Present"))
                blockblock.blocktype = 9;
            else if (block.name.StartsWith("Sand"))
                blockblock.blocktype = 10;
            else if (block.name.StartsWith("Snow"))
                blockblock.blocktype = 11;
            else if (block.name.StartsWith("stone"))
                blockblock.blocktype = 12;
            else if (block.name.StartsWith("StoneBricks"))
                blockblock.blocktype = 13;
            else if (block.name.StartsWith("Glowing"))
                blockblock.blocktype = 14;
            else if (block.name.StartsWith("Leaves"))
                blockblock.blocktype = 15;
            else if (block.name.StartsWith("Log"))
                blockblock.blocktype = 16;
            else if (block.name.StartsWith("DoNotTouch"))
                blockblock.blocktype = 17;
            blocks.Add(blockblock);
        }
        // Next
        BinaryFormatter formatter = new BinaryFormatter();
        var p = GameObject.Find("Camera Container").transform;
        try
        {
            formatter.Serialize(fs, new LevelFile{
                worldmap = blocks,
                playerx = p.position.x,
                playery = p.position.y,
                playerz = p.position.z
            });
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

public enum BlockType // Deprecated
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
public class Block    // Also Deprecated
{
    public float posx { get; set; }
    public float posy { get; set; }
    public float posz { get; set; }
    public BlockType blocktype { get; set; }
}

[Serializable]
public class BlockRewrite
{
    public float posx { get; set; }
    public float posy { get; set; }
    public float posz { get; set; }
    public Byte blocktype { get; set; }
}

[Serializable]
public class LevelFile
{
    public List<BlockRewrite> worldmap { get; set; }
    public float playerx { get; set; }
    public float playery { get; set; }
    public float playerz { get; set; }
    /* Just in case we need more fields in the future, this class needs to be backwards-compatible. */
    public Dictionary<string, object> otherthings { get; set; }
}
