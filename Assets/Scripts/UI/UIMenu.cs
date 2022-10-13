using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIMenu : MonoBehaviour
{
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
        
    }

    public void OpenMenu(GameObject previousMenu)
    {
        _previousMenu = previousMenu;
        _previousMenu.SetActive(false);
        gameObject.SetActive(true);
        _isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.buttonEast.wasPressedThisFrame) 
        {
            // The top menu
            if (_previousMenu == null)
            {
                _isOpen = false;
                gameObject.SetActive(false);
            }
            else
            {
                _previousMenu.SetActive(true);
                gameObject.SetActive(false);
                
            }
        }
    }
}
