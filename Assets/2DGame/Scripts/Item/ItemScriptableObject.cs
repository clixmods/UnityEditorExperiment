using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _2DGame.Scripts.Item
{
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
        #region SerializedField Item
        [Header("Item settings")] 
        [SerializeField] private ItemType _itemType;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] protected bool _isStackable;
        [Range(1,128)]
        [SerializeField] private int _stackMaxQuantity;
        #endregion
        #region Properties Item
        public ItemType Type => _itemType;
        public string name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public bool IsStackable => _isStackable;
        public int StackMaxQuantity => _stackMaxQuantity;
        #endregion
        [Header("Weapon settings")]
        [SerializeField] private Sprite spriteWorld;
        [SerializeField] private ItemScriptableObject ammoItem;
        public Sprite SpriteWorld => spriteWorld;
        public ItemScriptableObject AmmoItem => ammoItem;

    }
}