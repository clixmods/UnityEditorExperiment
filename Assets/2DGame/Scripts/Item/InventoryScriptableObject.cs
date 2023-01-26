using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _2DGame.Scripts.Item
{
   [Serializable]
   public class SlotInventory
   {
      #region Events

      public delegate void ItemUpdate();
      public event ItemUpdate EventItemUpdate;

      #endregion
      [SerializeField] private InventoryScriptableObject _inventoryScriptableObject;
      [SerializeField] private ItemScriptableObject _item;
      [SerializeField] private int _amount;

      #region Properties

      public InventoryScriptableObject inventoryScriptableObject
      {
         get => _inventoryScriptableObject;
         set
         {
            EventItemUpdate?.Invoke();
            _inventoryScriptableObject = value;
         }
      }
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

   [CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory", order = 0)]
   public class InventoryScriptableObject : ScriptableObject
   {
      #region Events
      public delegate void InventoryEvent();
      public delegate void InventoryEventSelect(SlotInventory slotInventory);
      public event InventoryEvent EventObjectAdd;
      public event InventoryEventSelect EventObjectSelect;
      #endregion
      [SerializeField] private SlotInventory[] slotsInventory;
      [SerializeField] private int slotsAmount = 10;
      public int SlotsAmount => slotsAmount;
     
      public SlotInventory[] GetSlotsInventory()
      {
         return slotsInventory;
      }
      /// <summary>
      /// Method to add item in inventory, if its possible, the method will return true, otherwise false
      /// </summary>
      /// <param name="itemToAdd"></param>
      /// <returns></returns>
      public bool AddItem(ItemScriptableObject itemToAdd)
      {
         if (itemToAdd.IsStackable)
         {
            int index = GetSlotIndexFromItem(itemToAdd);
            if (index != -1)
            {
               slotsInventory[index].amount++;
               EventObjectAdd?.Invoke();
               return true;
            }
            else
            {
               return AddItemToEmptySlot(itemToAdd);
            }
         }
         else
         {
            return AddItemToEmptySlot(itemToAdd);
         }
         return false;
      }
      private bool AddItemToEmptySlot(ItemScriptableObject itemToAdd)
      {
         int index = GetEmptySlotIndex();
         if (index != -1)
         {
            slotsInventory[index].inventoryScriptableObject = this;
            slotsInventory[index].item = itemToAdd;
            slotsInventory[index].amount++;
            EventObjectAdd?.Invoke();
            return true;
         }
         return false;
      }
      private int GetSlotIndexFromItem(ItemScriptableObject item)
      {
         int length = slotsInventory.Length;
         for (int i = 0; i < length; i++)
         {
            if (slotsInventory[i].item == item)
               return i;
         }
         return -1;
      }
      private int GetEmptySlotIndex()
      {
         int length = slotsInventory.Length;
         for (int i = 0; i < length; i++)
         {
            if (slotsInventory[i].item == null)
               return i;
         }
         return -1;
      }

      public bool TryGetSlotFromItem(ItemScriptableObject item, out SlotInventory slotInventory)
      {
         slotInventory = new SlotInventory();
         int index = GetSlotIndexFromItem(item);
         if (index == -1)
         {
            return false;
         }
         // Slot exist 
         slotInventory = slotsInventory[index];
         return true;
      }
      public bool HasItem(ItemScriptableObject itemToCheck)
      {
         return GetSlotIndexFromItem(itemToCheck) != -1;
      }

      public void SetSelectedSlot(SlotInventory slotInventory)
      {
         EventObjectSelect?.Invoke(slotInventory);
      }
      private void OnEnable()
      {
         slotsInventory = new SlotInventory[slotsAmount];
         for (int i = 0; i < slotsInventory.Length; i++)
         {
            slotsInventory[i] = new SlotInventory();
         }
      }
   }
}