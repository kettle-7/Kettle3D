using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using System;

public class PausedUI : MonoBehaviour
{
    public Canvas PlayingCanvas, NotPlayingCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseKettle3D() {
        UnityEngine.Application.Quit();
    }

    public void HowToPlay() {
        Process.Start("https://kettle3d.github.io/info/howtoplay");
    }

    public void SaveLevel()
    {
        Game game = GameObject.Find("Main Camera").GetComponent<Game>();
        game.Save();
    }
}
