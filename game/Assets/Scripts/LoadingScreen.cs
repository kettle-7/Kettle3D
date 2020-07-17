using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public enum Mode {
    ShowLevels,
    CreateLevel
}

public class LoadingScreen : MonoBehaviour
{
    public Mode mode;
    public Canvas mainCanvas, newLevelCanvas;
    public InputField worldname;
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    void OnGUI() {
        int height = 60;
        foreach (string save in Directory.GetFiles($"{Application.persistentDataPath}/saves")) {
            Debug.Log(save);
            if (GUI.Button(new Rect(10, height, 834, 30), Path.GetFileNameWithoutExtension(save))) {
                LevelManagement.management.Level = Path.GetFileNameWithoutExtension(save);
                StartGame();
            }
            height += 30;
        }
        if (Directory.GetFiles($"{Application.persistentDataPath}/saves").Length == 0) {
            GUI.Label(new Rect(352, 50, 150, 50), "You have not made any levels. You can make a new level by clicking below.");
        }
    }

    void Start() {
        mode = Mode.ShowLevels;
    }

    void Update() {
        if (mode == Mode.CreateLevel) {
            mainCanvas.enabled = (false);
            newLevelCanvas.enabled = (true);
        }
        else
        {
            mainCanvas.enabled = (true);
            newLevelCanvas.enabled = (false);
        }
    }

    public void CreateNewLevel() {
        mode = Mode.CreateLevel;
    }

    public void Go() {
        bool possiblePath = (worldname.text.IndexOfAny(Path.GetInvalidPathChars()) == -1);
        if (possiblePath && !(worldname.text == "")) {
            LevelManagement.management.Level = worldname.text;
        }
        else if (worldname.text == "") {
            LevelManagement.management.Level = "Level";
        }
        else {
            return;
        }
        StartGame();
    }
}
