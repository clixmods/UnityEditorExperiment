using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CustomEditor(typeof(StringComponent))]
public class StringComponentEditor : Editor
{
    private SerializedProperty PSceneInt { get; set; }
    private SerializedProperty PSceneString { get; set; }
    private SerializedProperty PMyBeautifulString { get; set; }
    private SerializedProperty PStats { get; set; }
    public void OnEnable()
    {
        PSceneInt = serializedObject.FindProperty("sceneInt");
        PSceneString = serializedObject.FindProperty("sceneString");
        PMyBeautifulString = serializedObject.FindProperty("_myBeautifulString");
        PStats = serializedObject.FindProperty("_stats");
    }

    public override void OnInspectorGUI()
    {
        using (new GUILayout.VerticalScope())
        {
            EditorGUILayout.PropertyField(PMyBeautifulString, true);
            EditorGUILayout.LabelField($"Oh shit, {PMyBeautifulString.stringValue}");
            EditorGUILayout.PropertyField(PSceneInt, true);
            DrawAddMinButtonForProperty(PSceneInt);
            EditorGUILayout.PropertyField(PSceneString, true);
            EditorGUILayout.PropertyField(PStats, true);
            EditorGUILayout.LabelField($"Youpi");
        }
        serializedObject.ApplyModifiedProperties();
    }

    void DrawAddMinButtonForProperty(SerializedProperty property)
    {
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("--"))
            {
                property.intValue--;
            }
            if (GUILayout.Button("++"))
            {
                property.intValue++;
            }
        }
        var scenesLength = EditorBuildSettings.scenes.Length-1;
        property.intValue = Mathf.Clamp(property.intValue, 0, scenesLength);
    }
}
