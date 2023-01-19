using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ItemInstance : MonoBehaviour, IGrabbable
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private ItemScriptableObject itemGrabbable;
    private BoxCollider2D _boxCollider2D;
    /// <summary>
    /// Method used to get default values for somes parameters to guarantee a correct execution
    /// </summary>
    private void GetDefaultValues()
    {
        if (_boxCollider2D == null || !_boxCollider2D.isTrigger )
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
        }
        if (_spriteRenderer == null && itemGrabbable != null)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = itemGrabbable.icon;
        }
    }
    private void OnValidate()
    {
        GetDefaultValues();
    }
    private void Awake()
    {
        GetDefaultValues();
    }
    
    public void OnGrab(InventoryScriptableObject targetInventory)
    {
        targetInventory.AddItem(itemGrabbable);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggered");
        if (other.TryGetComponent<ICharacter>(out var character))
        {
            OnGrab(character.Inventory);
            Destroy(gameObject);
        }
    }
}
