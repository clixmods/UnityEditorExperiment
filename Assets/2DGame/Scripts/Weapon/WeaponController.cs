using System;
using _2DGame.Scripts.Item;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
public class WeaponController : MonoBehaviour
{
    #region Events
    public delegate void WeaponEvent();
    public event WeaponEvent EventWeaponFire;
    #endregion
    [SerializeField] private Transform _pivotFire;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private WeaponAmmo _weaponAmmo;
    private void Start()
    {
        _weaponAmmo = GetComponent<WeaponAmmo>();
    }

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
        transform.right = directionAim;
        if (directionAim.x > 0)
        {
            _spriteRenderer.flipY = false;
        }
        else
        {
            _spriteRenderer.flipY = true;
        }
    }
    /// <summary>
    /// Set the apparence of the weapon
    /// </summary>
    /// <param name="sprite"></param>
    public void SetView(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        if (sprite != null)
        {
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _spriteRenderer.size = sprite.rect.size.normalized*2f;
        }
    }

 
    public void ShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventWeaponFire?.Invoke();
            WeaponControllerOnEventWeaponFire();
            Debug.Log("try to Shoot");
        }
    }
    // TODO : Move weapon behaviour in weapon controller
    private void WeaponControllerOnEventWeaponFire()
    {
        if (_weaponAmmo.CanUseBullet())
        {
            Debug.Log("Weapon fire");
            Rigidbody2D ammo = Instantiate(_weaponAmmo.ProjectilePrefab, _pivotFire.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            ammo.velocity = transform.TransformDirection(_pivotFire.transform.localPosition * Vector2.right);
        }
    }
    public void SetWeapon(Sprite weaponSprite, ref int ammo)
    {
        SetView(weaponSprite);
        _weaponAmmo.SetAmmoFromWeapon( ref ammo);
    }
    public void SetWeapon(Sprite weaponSprite)
    {
        SetView(weaponSprite);
        _weaponAmmo.SetAmmoFromWeapon( 0);
    }
   
}
