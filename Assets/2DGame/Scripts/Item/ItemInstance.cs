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
    private bool _isGrabbed;
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
            _spriteRenderer.sprite = itemGrabbable.Icon;
        }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        GetDefaultValues();
    }
#endif
    
    private void Awake()
    {
        GetDefaultValues();
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Prevent multiple OnTriggerEnter while the item is grabbed and not yet destroy
        if (_isGrabbed)
        {
            return;
        }
        if (other.TryGetComponent<ICharacter>(out var character))
        {
            OnGrab(character.Inventory);
        }
    }
    
    public void OnGrab(InventoryScriptableObject targetInventory)
    {
        bool itemIsAddedToInventory = targetInventory.AddItem(itemGrabbable);
        if (itemIsAddedToInventory)
        {
            _isGrabbed = true;
            Destroy(gameObject);
        }
    }
}
