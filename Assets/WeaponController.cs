using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _gunParent;
    public void AimWeaponMouse(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        _gunParent.transform.right = new Vector3(mousePos.x,mousePos.y,0) - transform.position;
    }
    //
    public void AimWeaponGamePad(InputAction.CallbackContext context)
    {
        _gunParent.transform.right = (Vector3)context.ReadValue<Vector2>();
    }

    public void ShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Shoot");
        }
    }
}
