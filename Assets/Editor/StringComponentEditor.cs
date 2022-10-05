using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(StringComponent))]
public class StringComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUI.enabled = false;
        EditorGUILayout.TextField($"Oh shit, {serializedObject.FindProperty ("_myBeautifulString").stringValue}");
        GUI.enabled = true;
        base.OnInspectorGUI();
    }
}
