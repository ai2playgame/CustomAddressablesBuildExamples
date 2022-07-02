using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEditor;

namespace CustomAASBuild.Editor
{
    public static class BuildPathChecker
    {
        [MenuItem("AAS/Advanced/Get Paths for Addressable")]
        private static void GetAddressablePaths()
        {
            var buildTarget = EditorUserBuildSettings.activeBuildTarget.ToString();
            var buildPath = Addressables.BuildPath;
            var runtimePath = Addressables.RuntimePath;

            Debug.Log($"LocalBuildPath: {buildPath}/{buildTarget}");
            Debug.Log($"LocalLoadedPath: {runtimePath}/{buildTarget}");
        }
    }
}