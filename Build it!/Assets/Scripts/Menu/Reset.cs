using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeletePlayerPrefs))]
public class Reset : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DeletePlayerPrefs myscript = (DeletePlayerPrefs)target;

        if(GUILayout.Button("Reset Level Progression"))
        {
            myscript.Reset();
        }
    }
}
