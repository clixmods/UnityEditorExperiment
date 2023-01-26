using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using _2DGame.Scripts.Item;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class WeaponController : MonoBehaviour
{
    #region Events
    public delegate void WeaponEvent();
    public event WeaponEvent EventWeaponFire;
    #endregion
    [SerializeField] private GameObject _gunParent;
    public int currentAmmo;
    public void AimWeaponMouse(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        SetAimDirection( new Vector3(mousePos.x,mousePos.y,0) - transform.position);
    }
    public void AimWeaponGamePad(InputAction.CallbackContext context)
    {
        SetAimDirection((Vector3)context.ReadValue<Vector2>());
    }

    public void SetAimDirection(Vector2 directionAim)
    {
        _gunParent.transform.right = directionAim;
    }

    public void ShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventWeaponFire?.Invoke();
            Debug.Log("Shoot");
        }
    }
}
