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
    //[SerializeField] private HashSet<GameObject> _previousMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (_menuGameObject == null)
        {
            Debug.LogError("No menuGameObject assigned, please assign it", gameObject);
            gameObject.SetActive(false);
        }
       
    }

    // public void SetPreviousMenu(GameObject previousMenu)
    // {
    //     _previousMenu.Add(previousMenu); 
    // }

    public void OpenMenu()
    {
        if(!_menuGameObject.IsOpen)
            _menuGameObject.gameObject.SetActive(true);
    }
}
