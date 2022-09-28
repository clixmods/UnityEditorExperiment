using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CustomPropertyDrawer(typeof(SceneAttribute))]
public class SceneAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] nameScenes = new string[EditorBuildSettings.scenes.Length] ;
        for(int i = 0; i < nameScenes.Length; i++)
        {
            SceneAsset asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[i].path);
            nameScenes[i] = asset.name;
        }

        property.intValue = EditorGUILayout.Popup(property.intValue, nameScenes);
    } 
}
