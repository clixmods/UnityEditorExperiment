using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneController : MonoBehaviour
{
    [SerializeField] InputActionAsset _actionAsset;
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions = _actionAsset;
        //_playerInput.SwitchCurrentActionMap("UI");
    }

    // Update is called once per frame
    void Update()
    {
        //_playerInput.SwitchCurrentControlScheme("Gamepad");
    }
}
