// These 'using' messages tell Unity what other pieces of code we're using.
using System.Runtime.Serialization.Formatters.Binary; // I use this to save and load the levels
using System.Runtime.InteropServices;                 // And this
using System.Runtime.Serialization;                   // And this too
using UnityEngine.SceneManagement;                    // This lets me change between the screen at the start and the actual game.
using System.Collections.Generic;                     // This adds better lists.
//using System.Windows.Forms;                            This only works on Windows, so I'm ignoring it.
using System.IO.Compression;                          // This helps me deal with zip files.
using System.Diagnostics;                             // This opens a web page or an app.
using System.Collections;                             // This adds better lists.
using System.Windows;                                 // This takes care of some of the graphics stuff that Unity doesn't.
using UnityEngine.UI;                                 // This does all the buttons, labels, images blah blah blah.
using UnityEngine;                                    // This is Unity.
using System.Text;                                    // This has some useful pieces of code for dealing with text.
using System.Net;                                     // This handles all the online stuff, I don't think I'm using it at the moment.
using System.Xml;                                     // This is useful for storing data.
using System.IO;                                      // This is important for dealing with files.
using System;                                         // This is the system.

using Debug = UnityEngine.Debug;                      // This tells C# that whenever I say 'Debug' I mean the Unity debug, not the system one.
/* Here are some descriptions of the keywords below:
Anyone can use this. Important for Unity.
    |  People can expand on this. Useful for making mods etc.
   \_/    |   It's a thing.
         \_/     |   This is the name of the thing. I'm calling it 'Game', because that's a good name for it.
                \_/    |    This thing is a child of MonoBehaviour, which is important for Unity.
                      \_/        |
                                \_/
*/
public class K3DModule {
    public string description;
}

public partial class Game : MonoBehaviour
{
    /*
    The lines below are 'fields', which are other objects that are part of this object.
        GameObject is a block or model.
               |   Unity handles these fields. They are the models for the blocks. PickedBlock is the block that the user is about to place.
              \_/          |
                          \_/
    */
    public GameObject BlockTemplate, PickedBlock;
    // This is the position of the player.
    public float xpos, ypos, zpos, movex, movey, movez;
    // This is a list of blocks that are in the scene.
    public List<GameObject> worldmap = new List<GameObject>();
    // This is a random. I used to use it for world generation.
    private readonly System.Random random = new System.Random();
    // These are Canvases, they are like models that hold all the buttons and text. PlayingCanvas holds the cursor, and NotPlaying canvas holds the pause menu, which is dealt with in PausedUI.cs.
    public Canvas PlayingCanvas, NotPlayingCanvas;
    // The name of the level we are saving to.
    private string savefile;
    // This 'Game' object. We could probably do without it.
    public static Game game;

    public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
    public Dictionary<int, Block> blocks = new Dictionary<int, Block>();
    public List<K3DModule> modules = new List<K3DModule>();

    public string mod = "default";

    // 'void' means that this is something that the 'Game' object can do. 'Awake' gets called when the game loads.
    void Awake() {
        // The 'game' field above gets set to 'this', which is this object.
        game = this;

        try {
            Directory.CreateDirectory($"{Application.persistentDataPath}/modules");
        } catch { }

        // We need to load all the blocks.
        string searchpath = $"{Application.persistentDataPath}/modules";
        foreach (string mo_d in Directory.GetDirectories(searchpath)) {
            mod = mo_d;
            foreach (string line in File.ReadAllLines($"{mod}/manifest")) {
                parseLine(line);
            }
        }
    }

    int current_block_def = -1;

    void parseLine(string line) {
        if (line.StartsWith("#")) {
            return;
        }
        if (line.StartsWith("BLOCK_DEF")) {
            if (line.Split(' ').Length != 2) { return; }
            var cbd = line.Split(' ')[1];
            int id = -1;
            if (int.TryParse(cbd, out id)) {
                try
                {
                    blocks.Add(id, new Block { id = id, model = Instantiate(BlockTemplate), name = "MyBlock"});
                } catch {
                    blocks[id] = (new Block { id = id, model = Instantiate(BlockTemplate), name = "MyBlock"});
                }
                blocks[id].model.active = false;
                blocks[id].model.transform.Find("Point Light").gameObject.SetActive(false);
                current_block_def = id;
            }
        }
        else {
            var ln = line.Split(':', ' ');
            if (ln.Length == 0) {
                return;
            }
            var com = ln[0];
            string sl = "";
            for (int count = 1; count < ln.Length; count++) {
                if (sl == "") {
                    sl = ln[count];
                }
                else {
                    sl += " " + ln[count];
                }
            }

            byte[] bytes;
            Texture2D img;
            switch (com) {
                case "Description":
                case "description":
                    modules.Add(new K3DModule { description = sl });
                    return;
                case "Name":
                case "name":
                    return;
                case "Id":
                case "id":
                    return;
                case "All":
                case "all":
                    img = new Texture2D(64, 64);
                    bytes = File.ReadAllBytes($"{mod}/textures/{sl}");
                    ImageConversion.LoadImage(img, bytes, false);
                    blocks[current_block_def].model.transform.Find("top").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    blocks[current_block_def].model.transform.Find("bottom").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    blocks[current_block_def].model.transform.Find("north").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    blocks[current_block_def].model.transform.Find("south").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    blocks[current_block_def].frontTexture = new Texture2D(64, 64);
                    blocks[current_block_def].frontTexture?.LoadImage(File.ReadAllBytes($"{mod}/textures/{sl}"));
                    blocks[current_block_def].model.transform.Find("east").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    blocks[current_block_def].model.transform.Find("west").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    return;
                case "top":
                case "Top":
                    img = new Texture2D(64, 64);
                    bytes = File.ReadAllBytes($"{mod}/textures/{sl}");
                    ImageConversion.LoadImage(img, bytes, false);
                    blocks[current_block_def].model.transform.Find("top").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    return;
                case "bottom":
                case "Bottom":
                    img = new Texture2D(64, 64);
                    bytes = File.ReadAllBytes($"{mod}/textures/{sl}");
                    ImageConversion.LoadImage(img, bytes, false);
                    blocks[current_block_def].model.transform.Find("bottom").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    return;
                case "north":
                case "North":
                    img = new Texture2D(64, 64);
                    bytes = File.ReadAllBytes($"{mod}/textures/{sl}");
                    ImageConversion.LoadImage(img, bytes, false);
                    blocks[current_block_def].model.transform.Find("north").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    return;
                case "south":
                case "South":
                    img = new Texture2D(64, 64);
                    bytes = File.ReadAllBytes($"{mod}/textures/{sl}");
                    ImageConversion.LoadImage(img, bytes, false);
                    blocks[current_block_def].frontTexture = new Texture2D(64, 64);
                    blocks[current_block_def].frontTexture?.LoadImage(File.ReadAllBytes($"{mod}/textures/{sl}"));
                    blocks[current_block_def].model.transform.Find("south").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    return;
                case "east":
                case "East":
                    img = new Texture2D(64, 64);
                    bytes = File.ReadAllBytes($"{mod}/textures/{sl}");
                    ImageConversion.LoadImage(img, bytes, false);
                    blocks[current_block_def].model.transform.Find("east").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    return;
                case "west":
                case "West":
                    img = new Texture2D(64, 64);
                    bytes = File.ReadAllBytes($"{mod}/textures/{sl}");
                    ImageConversion.LoadImage(img, bytes, false);
                    blocks[current_block_def].model.transform.Find("west").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", img);
                    return;
                case "Special":
                case "special":
                    if (sl.Contains("EmitsLight") || sl.Contains("emits_light"))
                        blocks[current_block_def].model.transform.Find("Point Light").gameObject.SetActive(true);
                    return;
            }
        }
    }

    // This makes the game load the level.
    void Load() {
        // Check if the level exists.
        if (File.Exists($"{Application.persistentDataPath}/saves/{savefile}.dat")) {
            // This is used to load the level file.
            FileStream fs = new FileStream($"{Application.persistentDataPath}/saves/{savefile}.dat", FileMode.Open);
            // These are lists of blocks.
            List<BlockRewrite> blks = new List<BlockRewrite>();
            // BinaryFormatter is used to convert the level into something that the game can understand.
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize means convert from binary jibberish.
            var level = (LevelFile) formatter.Deserialize(fs);
            // Look for the player and move it to where it was before the level last saved. 'level.playerx' means the 'playerx' field of the 'level' object.
            GameObject.Find("Camera Container").transform.Translate(level.playerx, level.playery, level.playerz);
            // Sets the variable 'blocks' to the 'worldmap' field of the Deserialised level.
            blks = (List<BlockRewrite>) level.worldmap;
            fs.Close();

            bool isValid = true;
            byte errorId = 0;

            // Do this for each item in the 'blocks' list. This makes a variable called 'block' for that specific item in the list.
            // The 'BlockRewrite' bit tells C# what type of object it is. We can just use 'var' if we wanted to.
            foreach (BlockRewrite block in blks) {
                // Make a GameObject called 'i'. I could name this anything but I just needed a nice short name. Remember that GameObject is a block.
                GameObject i;
                // We now need to turn our data that the game can understand into something that Unity can understand. For some strange reason you can't just serialise the block, so we have to do it like this.
                if (blocks.ContainsKey(block.blocktype)) { // Check what the 'blocktype' field of this particular block is. This should be a number.
                    // Make a block based on the block of the current ID. Vector3 is a position. Quaternion.identity means no rotation. We need to set the position to the numbers in block.
                    i = Instantiate(blocks[block.blocktype].model, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                    i.name = blocks[block.blocktype].id.ToString();
                    // Add i to worldmap. Worldmap is the list of blocks in the scene.
                    i.active = true;
                    worldmap.Add(i);
                }
                else {
                    errorId = block.blocktype;
                    isValid = false;
                }
            }

            if (!isValid) {
                Debug.LogWarning($"This level was saved in a newer version and/or with mods, so some blocks may not render correctly. Id for non-existent block: {errorId}");
            }
        }

        // If the level does not exist, generate one.
        else {
        //if(true) {
            // This sets item to -16, runs the code, adds 1 to item, runs the code, adds 1 to item again, and keeps doing this while item is less than 16. Basically, it does this 32 times.
            for (var item = -8f; item < 8f; item++)
            {
                // Again, but this time with item2.
                for (var item2 = -8f; item2 < 8f; item2++)
                {
                    if (random.Next(2) == 1) {
                        var i = Instantiate(blocks[2].model, new Vector3(item, 0f, item2), Quaternion.identity);
                        i.name = "2";
                        i.active = true;
                        worldmap.Add(i);

                        var j = Instantiate(blocks[4].model, new Vector3(item, 1f, item2), Quaternion.identity);
                        j.name = "4";
                        j.active = true;
                        worldmap.Add(j);
                    } else {
                        // Make some grass at this position.
                        var i = Instantiate(blocks[4].model, new Vector3(item, 0f, item2), Quaternion.identity);
                        i.name = "4";
                        i.active = true;
                        worldmap.Add(i);
                    }
                }
            }
        }
    }

    // This gets called when the game starts.
    void Start()
    {
        // Check the LevelManagement class, which is in LevelManagement.cs to see what the name of the level is.
        savefile = LevelManagement.management.Level;
        // This is just something I was experimenting with.
        Debug.Log(Application.installerName);
        Debug.Log(Application.installMode);
        // This shows the cursor and hides the pause menu.
        PlayingCanvas.enabled = true;
        NotPlayingCanvas.enabled = false;
        // This does 'Load', which is defined above.
        Load();
        // You start the game with some concrete in your hand.
        PickedBlock = blocks[0].model;
        // This sets the player position to the middle of the scene.
        this.xpos = 0.0f;
        this.ypos = 1.5f;
        this.zpos = 0.0f;
        this.movex = 0.0f;
        this.movey = 0.0f;
        this.movez = 0.0f;

        // This keeps the mouse in the middle of the screen.
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        // This is also needed to start the game with a concrete block in the player's hand.
        PickedItem = 0;
        cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
        if (cursor_image == null) {
            Debug.LogError("CURSOR_IMAGE IS NULL. PANIC AND CRASH THE GAME");
        }
    }

    // More fields. PickedItem is a number that says which item the player is holding. 0 is concrete.
    public int PickedItem = 0;
    // This says whether the pause menu is showing or not. bool is sort of a yes/no situation, where true is yes and false is no. Because it is true, the player is playing and so the pause menu should not be showing.
    public bool Playing = true;

    // Save the game when the user closes it. Save is another thing that Game can do.
    void OnApplicationQuit() {
        Save();
    }

    // This deletes the level.
    public void ResetLevel() {
        // Destroy every block in the scene.
        foreach (var o in worldmap.ToArray()) {
            Destroy(o);
            worldmap.Remove(o);
        }
        // Delete the level file.
        File.Delete($"{Application.persistentDataPath}/saves/{savefile}.dat");
        // Load the starting screen.
        SceneManager.LoadScene("Scenes/LoadingScreen");
    }
    
    public RawImage cursor_image;

    // This gets called every frame
    void Update()
    {
        // Check if the user has just pressed the escape key.
        if (Input.GetKeyDown("escape")) {
            // Whatever the 'Playing' field was, make it the opposite. '!' means opposite.
            Playing = !Playing;
            // If Playing is true, so the user is not paused:
            if (Playing) {
                // Keep the mouse in the middle of the screen
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            } else {
                // Let the mouse run free so that the user can click buttons.
                UnityEngine.Cursor.lockState = CursorLockMode.None;
            }
        }

        // If Playing is true, show the cursor.
        PlayingCanvas.enabled = Playing;
        // If Playing is the opposite of true, which is false, show the pause menu.
        NotPlayingCanvas.enabled = !Playing;
        // If the user is not paused:
        if (Playing) {
            // If the user pressed the left arrow key or is scrolling (down, I think?)
            if (Input.GetKeyDown("left") || Input.GetAxis("Mouse ScrollWheel") < 0) {
                /* Set the variable cursor_image to whatever this is:
                Look for a GameObject called 'Cursor'.
                If you find it, which it will, find an image inside it.
                RawImage is an image that is raw.
                */
                // Subtract one from the 'PickedItem' variable
                PickedItem --;
                
                if (PickedItem < 0) {
                    PickedItem = blocks.Keys.Count;
                }

                if (PickedItem > blocks.Keys.Count) {
                    PickedItem = 0;
                }

                if (blocks.ContainsKey(PickedItem)) {
                    PickedBlock = blocks[PickedItem].model;
                    cursor_image.texture = blocks[PickedItem].frontTexture;
                }
            }
            // If the user pressed the right arrow key or is scrolling (up, I think?), then do what we would if the user pressed the left arrow, except we add 1 to PickedItem instead of subracting 1.
            if (Input.GetKeyDown("right") || Input.GetAxis("Mouse ScrollWheel") > 0) {
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                PickedItem ++;
                
                if (PickedItem < 0) {
                    PickedItem = blocks.Keys.Count;
                }
                
                if (PickedItem > blocks.Keys.Count) {
                    PickedItem = 0;
                }

                if (blocks.ContainsKey(PickedItem)) {
                    PickedBlock = blocks[PickedItem].model;
                    cursor_image.texture = blocks[PickedItem].frontTexture;
                }
            }

            /* Find the player and turn their head in the direction that the user moved theit mouse. I think this one needs more explaining, so here goes:
            GameObject.Find looks for a GameObject with that name in the scene. transform is it's position.
            Rotate makes it turn based on a Vector3 object.
            Vector3.up is a Vector3 that point up, and the * symbol tells it to multiply it by whatever comes next.
            Input.GetAxis("Mouse X") checks how much the X coordinate of the mouse has changed.
            This one here here tuns the user side to side. */
            GameObject.Find("Camera Container").transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
            // And this one does the up and down movement.
            GameObject.Find("Main Camera").transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y"));

            /*
            Move the user.
            movex is a field that says how much the player is moving. This gives for that nice smooth movement.
            Input.GetAxis("Horizontal") tells Game a decimal number between -1 and 1.
            The harder the user is pressing 'A', the lower the number is. The harder the user is pressing 'D', the higher it is.
            This line multiplies that number by 4, then adds it to movex. */
            movex += Input.GetAxis("Horizontal") / 6;
            // Up/Down is the same as above, but instead of responding to the A and D keys, it listens to LEFT SHIFT and SPACE.
            movey += Input.GetAxis("Up/Down") / 6;
            // And Vertical listens to S and W.
            movez += Input.GetAxis("Vertical") / 6;
            // Make movex less than it is now. If we didn't do this, then the user would keep on moving forever.
            movex = movex * 0.735f;
            // Same as above, but for the Z movement
            movez = movez * 0.735f;
            // And for the Y movement
            movey = movey * 0.735f;
            /*
            Take those numbers and do something with them.
            Camera Container is an invisible container that the player is in.
            transform is the position of the GameObject.
            Translate moves the GameObject by the specified X, Y and Z. */
            GameObject.Find("Camera Container").transform.Translate(movex, movey, movez);
        }
    }

    // This saves the level.
    public void Save()
    {
        /* This makes a file. I probably should have said this before, but
        Application.persistentDataPath is a hidden folder where any Unity app can keep it's files.

        On your computer, along with other Windows computers, it's in C:\Users\<your name>\AppData\LocalLow\Kettle3D\Kettle3D.
        I told you how to get to this file earlier, but I didn't quite explain it properly.
        %userprofile% is something called an Environment Variable, which has a piece of imformation about your computer.
        userprofile is your home directory, which is C:\Users\<your name>.
        It's a hidden folder, so you'll need to use Windows+R and type in the location to get there.

        I have Linux, so on my computer and other Linux and ChromeOS computers it's at ~/.config/unity3d/Kettle3D/Kettle3D.

        On macOS, it's in ~/Library/Application Support/Kettle3D/Kettle3D.

        ~ on Linux, macOS and ChromeOS is an environment variable called $HOME that points to /home/<your name>.

        FileStream puts binary data in a file. FileMode.Create tells System.IO (see my usings at the top of this file) that we want
        to create a file if one doesn't exist, and delete it and create a new one if one is there.

        savefile is a field of Game.
        */
        byte[] backup = null;
        if (File.Exists($"{Application.persistentDataPath}/saves/{savefile}.dat")) {
            backup = File.ReadAllBytes($"{Application.persistentDataPath}/saves/{savefile}.dat");
        }
        FileStream fs = new FileStream($"{Application.persistentDataPath}/saves/{savefile}.dat", FileMode.Create);
        // Make a new List of BlockRewrite objects, see the bottom of the file.
        List<BlockRewrite> blocks = new List<BlockRewrite>();
        // Do this for each block in the worldmap field, which is a list of all the blocks:
        foreach (var block in worldmap)
        {
            // Make a new BlockRewrite object called blockblock.
            var blockblock = new BlockRewrite
            {
                // Set the posx field of blockblock to the X position of the block's GameObject
                posx = block.transform.position.x,
                // Do the same for the Y position...
                posy = block.transform.position.y,
                // ...and the Z position.
                posz = block.transform.position.z
            };
            if (!byte.TryParse(block.name, out byte id)) {
                Debug.LogError($"A block in the level has an invalid name: {block.name}. Assuming Concrete.");
                id = 0;
                /*fs.Close();
                if (backup != null)
                    File.WriteAllBytes($"{Application.persistentDataPath}/saves/{savefile}.dat", backup);
                return;*/
            }
            blockblock.blocktype = id;
            blocks.Add(blockblock);
        }
        // Make another BinaryFormatter
        BinaryFormatter formatter = new BinaryFormatter();
        // Get the position of the player
        var p = GameObject.Find("Camera Container").transform;
        // try to serialise the Level by doing the following:
        try
        {
            /*
            1) Make a new LevelFile object.
            2) Set it's worldmap field to our list of blocks.
            3) Set it's playerx field to the X position of the player.
            4) Do the same for the Y postion...
            5) ...and the Z position.
            */
            formatter.Serialize(fs, new LevelFile{
                worldmap = blocks,
                playerx = p.position.x,
                playery = p.position.y,
                playerz = p.position.z
            });
        }
        // If what we tried didn't work because of an issue with the serialization:
        catch (SerializationException e)
        {
            // Write the error to the output log.
            Debug.LogError("Failed to serialize. Reason: " + e.Message);
            // throw means crash the game, I think.
            throw;
        }
        finally // Regardless of whether or not what we tried worked, close the file so that it doesn't get corrupted.
        {
            fs.Close();
        }//*/
    }
}

[Serializable] // You can serialise this. Not neccessary at the moment
public class Block    // This class is for block definitions, as opposed to save data for a block.
{
    public string name { get; set; }
    public GameObject model { get; set; }
    public int id { get; set; }
    public Texture2D frontTexture { get; set; }
}

[Serializable] // You can serialise this.
public class BlockRewrite // A rewrite of the Block object, with one difference:
{
    public float posx { get; set; }
    public float posy { get; set; }
    public float posz { get; set; }
    public Byte blocktype { get; set; } // Byte is a number between 0 and 255.
}

[Serializable] // You can also serialise this.
public class LevelFile    // The object that gets saved to a level file.
{
    public List<BlockRewrite> worldmap { get; set; } // A list of BlockRewrite objects that are in the level
    public float playerx { get; set; }               // The player X,
    public float playery { get; set; }               // Y,
    public float playerz { get; set; }               // and Z position.
    /* Just in case we need more fields in the future, this object needs to be backwards-compatible. */
    public Dictionary<string, object> otherthings { get; set; }
}
// I hope you can understand this.
