using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LogConsole : EditorWindow
{
    private string _message;
    [MenuItem("Tools/Log Console")]
    static void Open()
    {
        GetWindow<LogConsole>();
    }

    private void OnGUI()
    {
       _message = EditorGUILayout.TextField(_message);
       if (GUILayout.Button("Send to console"))
       {
           Debug.Log(_message);
       }
       
       GUI.enabled = UnityEditor.Selection.activeObject != null;
       if (GUILayout.Button("Send selected object name"))
       {
           Debug.Log(UnityEditor.Selection.activeObject.name);
       }
       GUI.enabled = true;
       
    }
}
