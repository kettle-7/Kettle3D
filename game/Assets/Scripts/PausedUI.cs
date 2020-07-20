// This script is attached to every button in the pause menu.
using UnityEngine.SceneManagement; // A description of each using statement can be found in Game.cs
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using System;

// This is another object, which is known as a class. PausedUI manages the pause menu.
public class PausedUI : MonoBehaviour
{
    // These fields are described in Game.cs
    public Canvas PlayingCanvas, NotPlayingCanvas;
    // This is a reference to the 'Game' object that we make in Game.cs
    public Game game;
    // Start is called when the game loads
    void Start()
    {
        // The Game object is actually attached to the camera.
        game = GameObject.Find("Main Camera").GetComponent<Game>();
    }

    // This closes Kettle3D. It gets called by one of the buttons.
    public void CloseKettle3D() {
        UnityEngine.Application.Quit();
    }

    // This gets called when you click the 'How To Play' button.
    public void HowToPlay() {
        Process.Start("https://kettle3d.github.io/info/howtoplay");
    }

    // This saves the level.
    public void SaveLevel()
    {
        game.Save(); // What this actually does is ask the 'game' field of this class 
                     // (which is a 'Game' object, as defined in Game.cs) to go to the
                     // part of it's file that says 'void Save()' and do that. This
                     // is very common in C#.
    }

    // This gets called when you press the 'Delete Level' button.
    public void ResetLevel() {
        game.ResetLevel();
    }

    // This saves the game and loads the start screen, where you can pick a different level.
    public void LoadADifferentLevel() {
        game.Save();
        SceneManager.LoadScene("Scenes/LoadingScreen");
    }
}
