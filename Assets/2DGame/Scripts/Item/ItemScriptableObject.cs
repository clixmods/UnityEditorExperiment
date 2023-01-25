using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Object,
    Weapon,
    Ammo,
    Key
}

[CreateAssetMenu(fileName = "Item_", menuName = "Item/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    #region SerializedField
    [Header("Item settings")] 
    [SerializeField] private ItemType _itemType;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] protected bool _isStackable;
    [Range(1,128)]
    [SerializeField] private int _stackMaxQuantity;
    #endregion
    #region Properties
    public string name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    public bool IsStackable => _isStackable;
    public int StackMaxQuantity => _stackMaxQuantity;
    #endregion
    [Header("Weapon settings")]
    [SerializeField] private Sprite spriteWorld;
    public Sprite SpriteWorld => spriteWorld;

}
