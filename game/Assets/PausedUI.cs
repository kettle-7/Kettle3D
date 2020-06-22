using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedUI : MonoBehaviour
{
    public Canvas PlayingCanvas, NotPlayingCanvas;
    public GameObject HowToPlaySection, PausedUISection;
    public bool IsHowToPlayVisibleOrNot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HowToPlaySection.SetActive(IsHowToPlayVisibleOrNot);
        PausedUISection.SetActive(!IsHowToPlayVisibleOrNot);
    }

    public void CloseKettle3D() {
        UnityEngine.Application.Quit();
    }

    public void HowToPlay() {
        IsHowToPlayVisibleOrNot = true;
    }
    public void Pause() {
        IsHowToPlayVisibleOrNot = false;
    }
}
