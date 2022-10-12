using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        switch(context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("Started");
                break;
            case InputActionPhase.Performed:
                Debug.Log("Performed");
                break;
            case InputActionPhase.Canceled:
                Debug.Log("Canceled");
                break;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
