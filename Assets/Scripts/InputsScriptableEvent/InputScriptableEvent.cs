using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new_InputScriptableEvent" , menuName ="Events/Input Scriptable Event")]
public abstract class InputScriptableEvent : ScriptableObject
{
    public event Action Event;
    [SerializeField] private Sprite inputIcon;
    [SerializeField] private string helpCommand = "Push me";
    
    public void Invoke(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
            case InputActionPhase.Canceled:
                Event?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
    }
}

public abstract class InputScriptableEvent<T> : ScriptableObject where T : struct
{
    public event Action<T> Event;
    [SerializeField] private Sprite inputIconPlateformPC;
    [SerializeField] private string helpCommand = "Push me";

    private InputActionMap _inputActionMap;
  
    
    
    public void Invoke(InputAction.CallbackContext value)
    {
        //_inputIcon = value.action.actionMap; 
        switch (value.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
            case InputActionPhase.Canceled:
                Event?.Invoke(value.ReadValue<T>() );
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
    }
}