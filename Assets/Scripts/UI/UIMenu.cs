using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UIMenu : MonoBehaviour
{
    static UIMenu _activeMenu;
    public static UIMenu ActiveMenu
    {
        get { return _activeMenu; }
    }
    
    [SerializeField] private GameObject _firstSelectedGameObject;
    private GameObject _previousMenu;

    private bool _isOpen = false;
    public bool IsOpen {
        get { return _isOpen; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
            
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelectedGameObject);
        _activeMenu = this;
    }

   
    public void OpenMenu(GameObject previousMenu)
    {
        _previousMenu = previousMenu;
        _previousMenu.SetActive(false);
        gameObject.SetActive(true);
        _isOpen = true;
    }
    
    public static void CloseMenu()
    {
        if (_activeMenu == null) return;
        
        Debug.Log("CloseMenu");
        // The top menu
        if (_activeMenu._previousMenu == null)
        {
            _activeMenu._isOpen = false;
            _activeMenu.gameObject.SetActive(false);
            _activeMenu = null;
            
        }
        else
        {
            _activeMenu.gameObject.SetActive(false);
            _activeMenu._previousMenu.SetActive(true);
        }
    }
    
}
