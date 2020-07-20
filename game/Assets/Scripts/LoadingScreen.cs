// This script controls the Start Screen, where you choose a level.
using UnityEngine.SceneManagement; // A description of each using statement can be found in Game.cs
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public enum Mode { // For a description of what an enum does, see Game.cs
    ShowLevels,    // This particular one is used to keep track of whether the player is on the Choose a Level screen or
    CreateLevel    // the Make a Level screen.
}

public class LoadingScreen : MonoBehaviour // This class is a child of MonoBehaviour.
{
    public Mode mode;                         // This is an instance of the Mode enum.
    public Canvas mainCanvas, newLevelCanvas; // These are references to the canvases.
    public InputField worldname;              // This is the text box on the Make a Level screen, where the player enters the name
                                              // of the world they are creating.

    public void StartGame()                   // This gets called by this object.
    {
        SceneManager.LoadScene("Scenes/Game");// This loads the game.
    }

    void OnGUI() { // This is used for making buttons and stuff like that.
        int height = 60; // int means make a number.
        foreach (string save in Directory.GetFiles($"{Application.persistentDataPath}/saves")) {     // For each file (called save) in the folder where we keep our levels:
            if (GUI.Button(new Rect(10, height, 834, 30), Path.GetFileNameWithoutExtension(save))) { // If the user is clicking a button with the name of that level:
                LevelManagement.management.Level = Path.GetFileNameWithoutExtension(save);           // Set LevelManagement's Level field to the name of the level, but without the extension.
                StartGame();                                                                         // And start the game.
            }
            height += 30;                                                                            // Then add 30 to height so that the buttons stack on top of each other.
        }
        if (Directory.GetFiles($"{Application.persistentDataPath}/saves").Length == 0) {             // If there are no levels, tell the user that they can make one.
            GUI.Label(new Rect(352, 50, 150, 50), "You have not made any levels. You can make a new level by clicking below.");
        }
    }

    void Start() { // When Kettle3D loads, make it show the levels, not the screen to create a new one.
        mode = Mode.ShowLevels;
    }

    void Update() { // This gets called every frame.
        if (mode == Mode.CreateLevel) {     // If the user is making a new level:
            mainCanvas.enabled = (false);   // then hide the Show Levels canvas...
            newLevelCanvas.enabled = (true);// ...and show the Make a Level canvas
        }
        else
        {
            mainCanvas.enabled = (true); // Otherwise do the opposite.
            newLevelCanvas.enabled = (false);
        }
    }

    public void CreateNewLevel() { // Show the Create a New Level screen.
        mode = Mode.CreateLevel;
    }

    public void Go() { // When the user has entered the name of the level and clicked the button:
        bool possiblePath = (worldname.text.IndexOfAny(Path.GetInvalidPathChars()) == -1); // Check if the file name is valid (they can't have any slashes, colons etc.)
        if (possiblePath && !(worldname.text == "")) {         // && means and
            LevelManagement.management.Level = worldname.text; // If the name is valid and not nothing, make the level.
        }
        else if (worldname.text == "") {                       // Otherwise just call it Level.
            LevelManagement.management.Level = "Level";
        }
        else {
            return;
        }
        StartGame(); // And call the StartGame function we made above.
    }
}
