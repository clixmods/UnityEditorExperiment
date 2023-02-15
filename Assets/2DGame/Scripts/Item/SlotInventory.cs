using System;
using UnityEngine;
namespace _2DGame.Scripts.Item
{
    
    public class SlotInventory
    {
        #region Events

        public delegate void ItemUpdate();
        public event ItemUpdate EventItemUpdate;

        #endregion
        //[SerializeField] private InventoryScriptableObject _inventoryScriptableObject;
        [SerializeField] private ItemScriptableObject _item;
        public int _amount;

        public ref int GetRefAmount()
        {
            return ref _amount;
        }
        #region Properties

        // public InventoryScriptableObject inventoryScriptableObject
        // {
        //    get => _inventoryScriptableObject;
        //    set
        //    {
        //       EventItemUpdate?.Invoke();
        //       _inventoryScriptableObject = value;
        //    }
        // }
        public ItemScriptableObject item 
        {
            get => _item;
            set
            {
                EventItemUpdate?.Invoke();
                _item = value;
            }
        }
        public int amount 
        {
            get => _amount;
            set
            {
                EventItemUpdate?.Invoke();
                _amount = value;
                if (_amount == 0 && item != null && item.IsStackable)
                {
                    ClearSlot();
                }
            }
        }

        #endregion
        private void ClearSlot()
        {
            item = null;
            amount = 0;
        }
    }
}