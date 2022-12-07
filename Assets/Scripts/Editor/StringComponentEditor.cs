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
        // var propertyStats = serializedObject.FindProperty("_stats");
        // int healthValue = propertyStats.FindPropertyRelative("Health").intValue;
        // int maxhealthValue = propertyStats.FindPropertyRelative("maxhealth").intValue;
        // int manaValue = propertyStats.FindPropertyRelative("Mana").intValue;
        // int maxmanaValue = propertyStats.FindPropertyRelative("maxmana").intValue;
        // Rect rHealth = EditorGUILayout.BeginVertical();
        // EditorGUI.ProgressBar(rHealth, (float)healthValue/(float)maxhealthValue , $"{healthValue}/{maxhealthValue}" );
        // GUILayout.Space(16);
        // EditorGUILayout.EndVertical();
        // Rect rMana = EditorGUILayout.BeginVertical();
        // EditorGUI.ProgressBar(rMana, (float)manaValue/(float)maxmanaValue , $"{manaValue}/{maxmanaValue}" );
        // GUILayout.Space(16);
        // EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
