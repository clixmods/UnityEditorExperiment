using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CustomPropertyDrawer(typeof(LimitVisionAttribute))]

public class LimitVisionAttributeDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
       var value = property.serializedObject.FindProperty("typeCalcul").intValue;
       switch (value)
       {
           case 0:
               EditorGUI.Slider(position, property, 0, 1);
               break;
           case 1:
               EditorGUI.Slider(position, property, 0, 180);

               break;
       }
    } 
  
}
