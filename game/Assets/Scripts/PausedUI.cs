using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using System;

public class PausedUI : MonoBehaviour
{
    public Canvas PlayingCanvas, NotPlayingCanvas;
    public Game game;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Main Camera").GetComponent<Game>();
    }

    public void CloseKettle3D() {
        UnityEngine.Application.Quit();
    }

    public void HowToPlay() {
        Process.Start("https://kettle3d.github.io/info/howtoplay");
    }

    public void SaveLevel()
    {
        game.Save();
    }

    public void ResetLevel() {
        game.ResetLevel();
    }

    public void LoadADifferentLevel() {
        SceneManager.LoadScene("Scenes/LoadingScreen");
    }
}
