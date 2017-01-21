// C# example.

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BatchBuilder
{

    [MenuItem("Build/Build All Platforms")]
    public static void BuildGame()
    {
        // Get filename.
        //string path = Application.persistentDataPath + "/Build";


        //C:/Users/Brian/Documents/XO/Build
        string path = "C:/Users/Brian/Documents/XO/Build"; // Build the files to this folder first

        string networkPath = EditorUtility.SaveFolderPanel("", "", ""); // Then copy to the selected folder

        
        string[] levels = {
            "Assets/Scenes/s_start_Scene.unity",
            "Assets/Scenes/preparedScene.unity",
            "Assets/Scenes/hyperspaceScene.unity",
            "Assets/Scenes/blankScene.unity"
        };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/PC/XO.exe", BuildTarget.StandaloneWindows, BuildOptions.None);        
        BuildPipeline.BuildPlayer(levels, path + "/Mac/XO.app", BuildTarget.StandaloneOSXUniversal, BuildOptions.None);
        BuildPipeline.BuildPlayer(levels, path + "/Linux/XO.x86_64", BuildTarget.StandaloneLinux64, BuildOptions.None);


        string date = DateTime.Today.ToString("yyyy-MM-dd");

        networkPath +=  "/" + date;

        if (!Directory.Exists(networkPath))
        {
            Directory.CreateDirectory(networkPath);
        }

        if (!Directory.Exists(networkPath + "/PC"))
        {
            Directory.CreateDirectory(networkPath + "/PC");
        }
        FileUtil.ReplaceDirectory(path + "/PC", networkPath + "/PC");

        if (!Directory.Exists(networkPath + "/Mac"))
        {
            Directory.CreateDirectory(networkPath + "/Mac");
        }
        FileUtil.ReplaceDirectory(path + "/Mac", networkPath + "/Mac");

        if (!Directory.Exists(networkPath + "/Linux"))
        {
            Directory.CreateDirectory(networkPath + "/Linux");
        }
        FileUtil.ReplaceDirectory(path + "/Linux", networkPath + "/Linux");

        // Run the game (Process class from System.Diagnostics).
        //        Process proc = new Process();
        //        proc.StartInfo.FileName = path + "BuiltGame.exe";
        //        proc.Start();

        Debug.Log(("Build complete.").Colored(Colors.silver));
    }
}