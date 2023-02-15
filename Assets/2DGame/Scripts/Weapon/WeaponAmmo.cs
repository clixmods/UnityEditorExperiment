using System.Collections;
using System.Collections.Generic;
using _2DGame.Scripts.Item;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [SerializeField] private bool UnlimitedAmmo;
    [SerializeField] private GameObject projectilePrefab;
    public int currentAmmo;
    public GameObject ProjectilePrefab => projectilePrefab;
    public void SetAmmoFromWeapon(ref int amount)
    {
        currentAmmo = amount;
    }

  
    public void SetAmmoFromWeapon(int amount)
    {
        currentAmmo = amount;
    }

    public bool CanUseBullet()
    {
        if (UnlimitedAmmo)
        {
            return true;
        }
        bool canFire = currentAmmo > 0;
        if (canFire)
        {
            currentAmmo--;
            return true;
        }

        return false;
    }
}
