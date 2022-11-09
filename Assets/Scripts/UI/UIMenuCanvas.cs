using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class UIMenuCanvas : MonoBehaviour
{
    [FormerlySerializedAs("Escape Menu")] 
    [SerializeField] private UIMenu _menuGameObject;
    void Start()
    {
        if (_menuGameObject == null)
        {
            Debug.LogError("No menuGameObject assigned, please assign it", gameObject);
            gameObject.SetActive(false);
        }
       
    }
    public void OpenMenu()
    {
        if(!_menuGameObject.IsOpen)
            _menuGameObject.gameObject.SetActive(true);
    }
}
