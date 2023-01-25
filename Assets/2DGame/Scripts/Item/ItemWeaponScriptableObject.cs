using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ON A PAS LE DROIT

[CreateAssetMenu(fileName = "Item_", menuName = "Item/Item", order = 1)]
public class ItemWeaponScriptableObject : ItemScriptableObject
{
    [Header("Weapon settings")]
    [SerializeField] private Sprite spriteWorld;
    public Sprite SpriteWorld => spriteWorld;

    private void OnValidate()
    {
        
    }
}
