using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Makescriptable : MonoBehaviour {
    [MenuItem("Assets/Create/My Scriptable Object")]
    public static void CreateMyAsset()
    {
        GUNs asset = ScriptableObject.CreateInstance<GUNs>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
