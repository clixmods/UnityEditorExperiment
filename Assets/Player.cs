using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private float _xMove;
    private float _yMove;
    private Vector3 _fireDirection;
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject projectile;
    private PlayerInput _playerInput;


    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(_xMove, 0,_yMove ) * speed;
        Vector3 translation = move * Time.deltaTime;
        transform.Translate(translation);
    }

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


    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove");
        _xMove = context.ReadValue<Vector2>().x;
        _yMove = context.ReadValue<Vector2>().y;
     
    }
    

    
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("OnFire started");
            GameObject newNoPooledProjectile = Instantiate(projectile);
            newNoPooledProjectile.transform.position = transform.position;
            Rigidbody rb = newNoPooledProjectile.GetComponent<Rigidbody>();
            rb.transform.LookAt(Vector3.forward);
            rb.Sleep();
            rb.WakeUp();
            rb.AddForce(Vector3.forward * 1000);
        }
           
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) UIMenu.CloseMenu();
        if(UIMenu.ActiveMenu == null) _playerInput.SwitchCurrentActionMap("Game");
    }
}
