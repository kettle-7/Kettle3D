﻿using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System;

public class LoadingScreen : MonoBehaviour
{
    // Update is called once per frame
    void Update ()
    {

    }

    // Start is called before the first frame loads
    void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}