// This is a simple singleton object that holds the name of the current level.
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

public partial class LevelManagement : MonoBehaviour
{
    public static LevelManagement management; // public static means anything can access this without needing one of these objects.
    public string Level = "Level";            // this is just a simple string. It defaults to "Level", in case something happens.

    // This happens before Start, so that when other scripts need these variables they've already been defined.
    void Awake()
    {
        if (management == null) {          // What this method does is make sure that this is the ONLY LevelManagement.
            DontDestroyOnLoad(gameObject); // if (management == null) basically means "If this is the ONLY LevelManagement, and
            management = this;             // DontDestroyOnLoad means that when we load the game, this object is still there.
        }                                  // management = this sets the static man
        else if (management != this) {     // else if (management != this) basically means "If management isn't empty, and it isn't me":
            Destroy(gameObject);           // Then destroy myself because I'm not the only LevelManagent. The reason why we do this is
            return;                        // because if we have two managements then the newer one will replace the old one, and we
        }                                  // want to keep our old data because it means that we'll remember what level the player
                                           // picked.
        
        // We want to try creating the 'saves' folder where we put the levels, but if it's already there then we don't need to.
        // Note that unlike Python, C# ignores things like tabs and line breaks, so we can do it like this:
        try {Directory.CreateDirectory($"{Application.persistentDataPath}/saves");}catch{}finally{}
        // Or, if we wanted to, this:
        try // Try do this:
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/saves"); // This code will not be run because the folder
        }                                                                         // already exists, so we can't make it.
        catch // If that didn't work:
        {
            // do nothing
        }
        finally // Regardless of whether it did or didn't work, do this:
        {
            // also do nothing.
        }
        // We could also put this whole script on one line, if we removed these comments.
        /* Or did it like this: */ UnityEngine.Debug.Log(""); /* Now the next line etc. */
    }
}
