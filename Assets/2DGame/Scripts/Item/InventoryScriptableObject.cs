using System;
using _2DGame.Scripts.Save;
using UnityEngine;
namespace _2DGame.Scripts.Item
{
   [CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory", order = 0)]
   public class InventoryScriptableObject : ScriptableObjectSaveable 
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
           // slotsInventory[index].inventoryScriptableObject = this;
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
      
      #region Save and Load
      class SlotsInventorySaveData : SaveData
      {
         [Serializable]
         public struct slot
         {
            public string resourcesFileName;
            public int value;
         }
         public slot[] slots;
      }
      public override void OnLoad(string data)
      {
         SlotsInventorySaveData slotsInventoryLoaded = JsonUtility.FromJson<SlotsInventorySaveData>(data);
         slotsInventory = new SlotInventory[slotsInventoryLoaded.slots.Length];
         for (int i = 0; i < slotsInventoryLoaded.slots.Length; i++)
         {
            slotsInventory[i] = new SlotInventory();
            slotsInventory[i].amount = slotsInventoryLoaded.slots[i].value;
            // Get File name of the scriptableObject used in item to get it from Resources Folder
            string resourcesFileName = slotsInventoryLoaded.slots[i].resourcesFileName;
            slotsInventory[i].item = DataPersistentUtility.GetAssetFromResources<ItemScriptableObject>(resourcesFileName);
         }
         EventObjectAdd?.Invoke();
      }
      public override void OnSave(out SaveData saveData)
      {
         SlotsInventorySaveData slotsToSave = new SlotsInventorySaveData();
         slotsToSave.slots = new SlotsInventorySaveData.slot[slotsInventory.Length] ;
         for (int i = 0; i < slotsInventory.Length; i++)
         {
            var item = slotsInventory[i].item;
            if(item != null)
               slotsToSave.slots[i].resourcesFileName = DataPersistentUtility.ExtractAssetName(item);
            
            slotsToSave.slots[i].value = slotsInventory[i].amount;
         }
         saveData = slotsToSave;
      }
      #endregion
     
   }
}