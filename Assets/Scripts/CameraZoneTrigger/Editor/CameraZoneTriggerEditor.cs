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
        CameraEditorUtils.HandleFrustum(_camera,0);
        Transform boxColliderTransform = _boxCollider.transform;
        Transform cameraTransform = _camera.transform;
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            CreateHandlesForTransform(cameraTransform, out var positionHandleCam, out var rotationHandleCam);
            CreateHandlesForTransform(boxColliderTransform, out var positionHandleBox, out var rotationHandleBox);
            if (check.changed)
            {
                boxColliderTransform.position = positionHandleBox;
                boxColliderTransform.rotation = rotationHandleBox;
                cameraTransform.position = positionHandleCam;
                cameraTransform.rotation = rotationHandleCam;
            }
        }
        // method localToWorldMatrix correspond to Matrix4x4.TRS(boxColliderTransform.position,boxColliderTransform.rotation, boxColliderTransform.lossyScale);
        using (new Handles.DrawingScope(Color.green,boxColliderTransform.localToWorldMatrix))
        {
            _boxCollider.center = _box.center;
            _boxCollider.size = _box.size;
            _box.DrawHandle();
        }
    }

    private void CreateHandlesForTransform(Transform targetTransform, out Vector3 positionHandle,
        out Quaternion rotationHandle)
    {
        Vector3 transformPosition = targetTransform.position;
        positionHandle = Handles.PositionHandle(transformPosition, Quaternion.identity);
        rotationHandle = Handles.RotationHandle(targetTransform.rotation, transformPosition );
    }
}

