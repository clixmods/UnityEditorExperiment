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
   #region Events
   public delegate void InventoryEvent();
   public event InventoryEvent EventObjectAdd;
   #endregion
   [SerializeField] private SlotInventory[] slotsInventory;
   [SerializeField] private int slotsAmount = 10;
   public int SlotsAmount => slotsAmount;
   private void OnEnable()
   {
      slotsInventory = new SlotInventory[slotsAmount];
   }
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
         int index = GetSlotFromItem(itemToAdd);
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
         slotsInventory[index].item = itemToAdd;
         slotsInventory[index].amount++;
         EventObjectAdd?.Invoke();
         return true;
      }
      return false;
   }
   private int GetSlotFromItem(ItemScriptableObject item)
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

}
