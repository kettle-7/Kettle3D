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
public partial class Game : MonoBehaviour
{
    /*
    The lines below are 'fields', which are other objects that are part of this object.
        GameObject is a block or model.
               |   Unity handles these fields. They are the models for the blocks. PickedBlock, if you scroll right to the end, is the block that the user is about to place.
              \_/          |
                          \_/
    */
    public GameObject ConcreteModel, GrassModel, DirtModel, StoneModel, K3DModel, BrickModel, LightModel, OvenModel, SandModel, SnowModel, StoneBricksModel, PresentModel, GlassModel, HayModel, GlowingModel, LeavesModel, LogModel, DoNotTouchModel, PickedBlock;
    // These are the textures for the blocks that I use for the cursor.
    public Texture Concrete, Grass, Dirt, Stone, K3D, Brick, Light, Oven, Sand, Snow, StoneBricks, Present, Glass, Hay, Leaves, Glowing, Log, DoNotTouch;
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

    // 'void' means that this is something that the 'Game' object can do. 'Awake' gets called when the game loads.
    void Awake() {
        // The 'game' field above gets set to 'this', which is this object.
        game = this;
    }

    // This makes the game load the level.
    void Load() {
        // Check if the level exists.
        if (File.Exists($"{Application.persistentDataPath}/saves/{savefile}.dat")) {
            // This is used to load the level file.
            FileStream fs = new FileStream($"{Application.persistentDataPath}/saves/{savefile}.dat", FileMode.Open);
            // These are lists of blocks.
            List<Block> legacyBlocks = new List<Block>();
            List<BlockRewrite> blocks = new List<BlockRewrite>();
            // try do this
            try
            {
                // BinaryFormatter is used to convert the level into something that the game can understand.
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize means convert from binary jibberish.
                var level = (LevelFile) formatter.Deserialize(fs);
                // Look for the player and move it to where it was before the level last saved. 'level.playerx' means the 'playerx' field of the 'level' object.
                GameObject.Find("Camera Container").transform.Translate(level.playerx, level.playery, level.playerz);
                // Sets the variable 'blocks' to the 'worldmap' field of the Deserialised level.
                blocks = (List<BlockRewrite>) level.worldmap;
            }
            //catch (SerializationException e)
            //{
            //    Debug.LogError("Failed to deserialize. Reason: " + e.Message);
            //    throw;
            //}
            catch // We used to use a different method for saving the level. If what we 'tried' didn't work, assume we are using the old format.
            {
                // I described this above
                BinaryFormatter formatter = new BinaryFormatter();

                // We used to just save the blocks; now we save the player position too.
                legacyBlocks = (List<Block>) formatter.Deserialize(fs);
            }
            finally // Regardless of whether what we tried worked or not, do this.
            {
                fs.Close();
            }

            // Do this for each item in the 'blocks' list. This makes a variable called 'block' for that specific item in the list.
            // The 'BlockRewrite' bit tells C# what type of object it is. We can just use 'var' if we wanted to.
            foreach (BlockRewrite block in blocks) {
                // Make a GameObject called 'i'. I could name this anything but I just needed a nice short name. Remember that GameObject is a block.
                GameObject i;
                // We now need to turn our data that the game can understand into something that Unity can understand. For some strange reason you can't just serialise the block, so we have to do it like this.
                switch (block.blocktype) { // Check what the 'blocktype' field of this particular block is. This should be a number.
                    // If this number is zero:
                    case 0:
                        // Make a block based on ConcreteModel. Vector3 is a position. Quaternion.identity means no rotation. We need to set the position to the numbers in block.
                        i = Instantiate(ConcreteModel, new Vector3(block.posx, block.posy, block.posz), Quaternion.identity);
                        // Add i to worldmap. Worldmap is the list of blocks in the scene.
                        worldmap.Add(i);
                        // 'break' tells C# we're done now, and can exit the 'switch' statement.
                        break;
                    // If this number is 1, and so on.
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
                    // If the number is not one of the ones above, then it must have been saved in a newer version.
                    default:
                        // Write some text to the log file. If you're curious, press Windows+R, type in "%userprofile%\AppData\LocalLow\Kettle3D\Kettle3D" and press enter. This is a hidden folder, so you'll need to put it in the path. It has the output log and all of your levels in it.
                        Debug.LogWarning("This level has been saved in a newer version. Any blocks that do not exist in this version will not render.");
                        break;
                }
            }
            // Note there's two lists here. This one does the same as above but for the old format.
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

        // If the level does not exist, generate one.
        else {
        //if(true) {
            // This sets item to -16, runs the code, adds 1 to item, runs the code, adds 1 to item again, and keeps doing this while item is less than 16. Basically, it does this 32 times.
            for (var item = -16f; item < 16f; item++)
            {
                // Again, but this time with item2.
                for (var item2 = -16f; item2 < 16f; item2++)
                {
                    //if (random.Next(2) == 1) {
                    //    Instantiate(StoneModel, new Vector3(item, 0f, item2), Quaternion.identity);
                    //} else {
                        // Make some grass at this position.
                        worldmap.Add(Instantiate(GrassModel, new Vector3(item, 0f, item2), Quaternion.identity));
                    //}
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
        // You start the game with a brick in your hand.
        PickedBlock = BrickModel;
        // This sets the player position to the middle of the scene.
        this.xpos = 0.0f;
        this.ypos = 1.5f;
        this.zpos = 0.0f;
        this.movex = 0.0f;
        this.movey = 0.0f;
        this.movez = 0.0f;

        // This keeps the mouse in the middle of the screen.
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        // This is also needed to start the game with a brick in the player's hand.
        PickedItem = 5;
    }

    // More fields. PickedItem is a number that says which item the player is holding. 5 is bricks.
    public int PickedItem = 5;
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
                var cursor_image = GameObject.Find("Cursor").GetComponent<RawImage>();
                // Subtract one from the 'PickedItem' variable
                PickedItem -= 1;
                // If PickedItem is less that zero, make it 17. This means that if you scroll down below concrete, make it Uranium Sign.
                if (PickedItem < 0) {
                    PickedItem = 17;
                }
                // If PickedItem is more than 17, make it zero. This means that if you scroll up past Uranium Sign, make it concrete.
                if (PickedItem > 17) {
                    PickedItem = 0;
                }
                // If PickedItem is zero:
                if (PickedItem == 0) {
                    // Set the 'PickedBlock' field to the model of some concrete. This gets used by BlockSide.cs when you place a block, so that it knows which block to place.
                    PickedBlock = ConcreteModel;
                    // Set the cursor to a picture of concrete.
                    cursor_image.texture = Concrete;
                }
                // If PickedItem is one...
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
            // If the user pressed the right arrow key or is scrolling (up, I think?), then do what we would if the user pressed the left arrow, except we add 1 to PickedItem instead of subracting 1.
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
            movex += Input.GetAxis("Horizontal") / 4;
            // Up/Down is the same as above, but instead of responding to the A and D keys, it listens to LEFT SHIFT and SPACE.
            movey += Input.GetAxis("Up/Down") / 4;
            // And Vertical listens to S and W.
            movez += Input.GetAxis("Vertical") / 4;
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

        I have Linux, so on my computer and other Linux and ChromeOS computers it's at ~/.config/unity3d/Kettle3D/Kettle3D.

        On macOS, it's in ~/Library/Application Support/Kettle3D/Kettle3D.

        ~ on Linux, macOS and ChromeOS is an environment variable called $HOME that points to /home/<your name>.

        FileStream puts binary data in a file. FileMode.Create tells System.IO (see my usings at the top of this file) that we want
        to create a file if one doesn't exist, and delete it and create a new one if one is there.

        savefile is a field of Game.
        */
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

            // If the name of block starts with "brick":
            if (block.name.StartsWith("brick"))
                blockblock.blocktype = 1; // Set the blocktype field of this blockblock to 1.
            // If the name of block starts with "concrete":
            else if (block.name.StartsWith("concrete"))
                blockblock.blocktype = 0; // Set it to 0.
            // and so on...
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
        }
    }
}

public enum BlockType // Deprecated way of keeping track of what type of block a block is.
{                     // An enum is sort of a list of things something can be.
    Concrete,         // BlockType can be Concrete, Grass etc.
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
} // The issue of doing it this way is if we change the enum, the BinaryFormatter won't be able to read the file any more.
  // And every time we make a new block, we have to change the enum.

[Serializable] // You can serialise this.
public class Block    // Also Deprecated
{
    // This object is a bunch of fields that we use to save info.
    public float posx { get; set; }          // You can get and set the X position,
    public float posy { get; set; }          // the Y postion,
    public float posz { get; set; }          // and the Z positon.
    public BlockType blocktype { get; set; } // You can get and set the BlockType of this block. Because we used the BlockType
}                                            // enum, we can't use this object any more. That's why we have BlockRewrite.
                                             // The only reason this object still exists is in case we need to load a level saved
                                             // when we did use Block. Even then, you should still make backups of your old levels.
                                             // You can download a ZIP of the old version by clicking Code and then Releases,
                                             // and picking the oldest one.

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
