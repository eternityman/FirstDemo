using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildTools
{
    public static void BuildApp(){
        BuildPipeline.BuildPlayer(new BuildPlayerOptions{
            scenes = new string[]{
                "Assets/Scenes/SampleScene.unity"
            },
            locationPathName = "Builds/App.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4 | BuildOptions.StrictMode
        });
    }
}
