using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public static LevelManagement management;
    public string Level = "Level";

    void Awake()
    {
        if (management == null) {
            DontDestroyOnLoad(gameObject);
            management = this;
        }
        else if (management != this) {
            Destroy(gameObject);
            return;
        }

        try {Directory.CreateDirectory($"{Application.persistentDataPath}/saves");}catch{}
    }
}
