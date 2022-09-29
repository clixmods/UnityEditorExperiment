using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
[CustomEditor(typeof(CameraZoneTrigger))]

public class CameraZoneTriggerEditor : Editor
{
    private BoxCollider _boxCollider;
    private Camera _camera;
    private BoxBoundsHandle _box;
    private CameraZoneTrigger myObject;
    private void Awake()
    {
        myObject = (CameraZoneTrigger)target;
        _camera = myObject.GetComponentInChildren<Camera>();
        _boxCollider = myObject.GetComponentInChildren<BoxCollider>();
        // Create a new box 
        _box = new BoxBoundsHandle();
        _box.center = _boxCollider.center;
        _box.size = _boxCollider.size;
    }

    private void OnSceneGUI()
    {
        //https://docs.unity3d.com/ScriptReference/EditorGUI.ChangeCheckScope.html
        Transform boxColliderTransform = _boxCollider.transform;
        Transform cameraTransform = _camera.transform;
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            Vector3 posCam = Handles.PositionHandle(cameraTransform.position, Quaternion.identity);
            Vector3 posComponentBox = Handles.PositionHandle(boxColliderTransform.position , Quaternion.identity);
            if (check.changed)
            {
                boxColliderTransform.position = posComponentBox;
                cameraTransform.position = posCam;
            }
        }
        Matrix4x4 matrix = Matrix4x4.TRS(boxColliderTransform.position,boxColliderTransform.rotation, boxColliderTransform.lossyScale);
        using (new Handles.DrawingScope(Color.green,matrix))
        {
            _boxCollider.center = _box.center;
            _boxCollider.size = _box.size;
             _box.DrawHandle();
        }
    }
}

