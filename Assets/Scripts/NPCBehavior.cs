using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCBehavior : MonoBehaviour
{   
    [SerializeField]
    private InputAction _inputAction;
    // Start is called before the first frame update
    void OnEnable()
    {
        _inputAction.Enable();
    }
    void OnDisable()
    {
        _inputAction.Disable();
    }

    private void Start()
    {
        _inputAction.performed += InputActionOnperformed;
    }

    private void InputActionOnperformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Interaction callback ");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
