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
    /// <summary>
    /// Indicate the current active menu
    /// </summary>
    public static UIMenu ActiveMenu { get; private set; }
    [Tooltip("Assign the button to select when the menu is opened")]
    [SerializeField] private GameObject _firstSelectedGameObject;
    [Header("Settings")]
    [SerializeField] private bool closeMenuOnReleaseButton;
    [Tooltip("The menu can be opened when an another menu is open ?")]
    [SerializeField] private bool canBeOpenedAnywhere;
    [Tooltip("Reopen the parent menu when the menu is closed ?")]
    [SerializeField] private bool reopenPreviousMenuOnClose;
    [Tooltip("The menu can erase the active menu and close it ?")]
    [SerializeField] private bool EraseActiveMenu;
    [Tooltip("The timescale when the menu is opened")]
    [SerializeField] [Range(0,10)] private float timeScale = 1;
    private float _previousTimeScale;
    private GameObject _previousMenu;
    private double TOLERANCE = 0.05f;

    public bool IsOpen { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(_firstSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(_firstSelectedGameObject);
        else
        {
            Debug.LogError("UIMenu : variable _firstSelectedGameObject is not assigned, please assign it");
        }
    }

    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Started:
                if (ActiveMenu == this && !closeMenuOnReleaseButton)
                {
                    this.CloseMenu(false);
                }
                else
                {
                    this.OpenMenu(null);
                }
                break;
            case InputActionPhase.Canceled:
                if(closeMenuOnReleaseButton)
                    this.CloseMenu(false);
                break;
        }
    }
    public void OpenMenu(GameObject parentMenu = null)
    {
        if (!canBeOpenedAnywhere && ActiveMenu != null && parentMenu != ActiveMenu.gameObject)
        {
            return;
        }
        // Erase the current active menu to replace it
        if (EraseActiveMenu && ActiveMenu != null && ActiveMenu != this)
        {
            ActiveMenu.CloseMenu();
        }
        ActiveMenu = this;
        // Menu opened by a parent ? Go save it in cache
        if (parentMenu != null)
        {
            _previousMenu = parentMenu;
            _previousMenu.SetActive(false);
        }
        // Menu affect Timescale 
        _previousTimeScale = Time.timeScale;
        if (Math.Abs(Time.timeScale - 1) < TOLERANCE)
        {
            _previousTimeScale = Time.timeScale;
            Time.timeScale = timeScale;
        }
        // Open the menu
        gameObject.SetActive(true);
        IsOpen = true;
    }
    public void CloseMenu(bool openPreviousMenu = false)
    {
        if (ActiveMenu == null || !IsOpen) 
            return;
        
        Time.timeScale = ActiveMenu._previousTimeScale;
        // Parent menu
        if(_previousMenu != null)
        {
            if (openPreviousMenu)
            {
                _previousMenu.SetActive(true);
            }
            else
            {
                _previousMenu = null;
            }
        }
        // Close menu
        IsOpen = false;
        gameObject.SetActive(false);
        ActiveMenu = null;
        
    }
}