using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CustomPropertyDrawer(typeof(SceneAttribute))]

public class SceneAttributeDrawer : PropertyDrawer
{
    private string[] _nameScenes;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _nameScenes = new string[EditorBuildSettings.scenes.Length] ;
        for(int i = 0; i < _nameScenes.Length; i++)
        {
            SceneAsset asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[i].path);
            _nameScenes[i] = asset.name;
        }
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = EditorGUILayout.Popup(property.displayName ,property.intValue, _nameScenes);
                break;
            case SerializedPropertyType.String:
                int i = EditorGUILayout.Popup(property.displayName ,GetIndexFromName(property.stringValue), _nameScenes);
                property.stringValue = _nameScenes[i];
                break;
            default :
                EditorGUILayout.HelpBox($"Type of {property.displayName} is incompatible for Scene attribute, use Int or String type",MessageType.Error);
                break;
        }
    } 
    int GetIndexFromName(string strToSearch)
    {
        int length = _nameScenes.Length;
        for (int i = 0; i < length; i++)
        {
            if (_nameScenes[i] == strToSearch)
                return i;
        }
        return 0;
    }
}
