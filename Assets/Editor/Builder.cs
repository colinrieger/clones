using System.Linq;
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [MenuItem("Build/Build Win32")]
    public static void BuildWin32()
    {
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();

        BuildPipeline.BuildPlayer(scenes, "Build/Win32/clones.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }
}