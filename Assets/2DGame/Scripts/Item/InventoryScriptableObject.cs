using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SlotInventory
{
   public ItemScriptableObject item;
   public int amount;
}

[CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory", order = 0)]
public class InventoryScriptableObject : ScriptableObject
{
   [SerializeField] private SlotInventory[] _slotsInventory;
   [SerializeField] private int slotsAmount = 10;
   public int SlotsAmount => slotsAmount;

   private void OnEnable()
   {
      _slotsInventory = new SlotInventory[slotsAmount];
   }
#if UNITY_EDITOR
   public SlotInventory[] GetItems()
   {
      return _slotsInventory;
   }

   public int GetItemStackQuantity(ItemScriptableObject item)
   {
      // if(item.IsStackable)
      //    return Items[item];

      return 1;
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
         int index = GetSlotFromItem(itemToAdd);
         if (index != -1)
         {
            _slotsInventory[index].amount++;
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
         _slotsInventory[index].item = itemToAdd;
         _slotsInventory[index].amount++;
         return true;
      }
      return false;
   }
   int GetSlotFromItem(ItemScriptableObject item)
   {
      int length = _slotsInventory.Length;
      for (int i = 0; i < length; i++)
      {
         if (_slotsInventory[i].item == item)
            return i;
      }
      return -1;
   }
   int GetEmptySlotIndex()
   {
      int length = _slotsInventory.Length;
      for (int i = 0; i < length; i++)
      {
         if (_slotsInventory[i].item == null)
            return i;
      }
      return -1;
   }
#endif
}
