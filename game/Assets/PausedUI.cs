using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

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
}
