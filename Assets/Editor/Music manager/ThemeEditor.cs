using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Theme))]
public class ThemeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Edit theme"))
            ThemeGeneratorWindow.CreateWindow((Theme)target);
    }
}
