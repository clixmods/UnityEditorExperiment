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
    public static UIMenu ActiveMenu { get; private set; }
    [SerializeField] private GameObject _firstSelectedGameObject;
    private GameObject _previousMenu;
    public bool IsOpen { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelectedGameObject);
        ActiveMenu = this;
    }
    public void OpenMenu(GameObject previousMenu)
    {
        _previousMenu = previousMenu;
        _previousMenu.SetActive(false);
        gameObject.SetActive(true);
        IsOpen = true;
    }
    public static void CloseMenu()
    {
        if (ActiveMenu == null) 
            return;

        Debug.Log("CloseMenu");
        // The top menu
        if (ActiveMenu._previousMenu == null)
        {
            ActiveMenu.IsOpen = false;
            ActiveMenu.gameObject.SetActive(false);
            ActiveMenu = null;
        }
        else
        {
            ActiveMenu.gameObject.SetActive(false);
            ActiveMenu._previousMenu.SetActive(true);
        }
    }
}