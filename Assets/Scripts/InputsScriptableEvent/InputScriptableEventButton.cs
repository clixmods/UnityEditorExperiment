using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new_InputScriptableEvent", menuName = "Events/Input Scriptable Event Button", order = 0)]
public class InputScriptableEventButton : InputScriptableEvent
{
    // public event Action Event;
    // public Sprite InputIcon;
    //
    // public void Invoke(InputAction.CallbackContext value)
    // {
    //     switch (value.phase)
    //     {
    //         case InputActionPhase.Disabled:
    //             break;
    //         case InputActionPhase.Waiting:
    //             break;
    //         case InputActionPhase.Started:
    //             break;
    //         case InputActionPhase.Performed:
    //         case InputActionPhase.Canceled:
    //             Event?.Invoke();
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }
}