using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CustomEditor(typeof(StringComponent))]
public class StringComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        EditorGUILayout.LabelField($"Oh shit, {serializedObject.FindProperty ("_myBeautifulString").stringValue}");

        base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();

        var property = serializedObject.FindProperty("test");
        if(GUILayout.Button("--"))
        {
            property.intValue--;
        }

        if (GUILayout.Button("++"))
        {
            property.intValue++;
        }

        var scenesLength = EditorBuildSettings.scenes.Length-1;
        property.intValue = Mathf.Clamp(property.intValue, 0, scenesLength);
        
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }
}
