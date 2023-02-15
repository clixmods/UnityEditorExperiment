using UnityEngine;
namespace _2DGame.Scripts.Item
{
   [CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory", order = 0)]
   public class InventoryScriptableObject : ScriptableObject
   {
      #region Events
      public delegate void InventoryEvent();
      public delegate void InventoryEventSelect(SlotInventory slotInventory);
      public event InventoryEvent EventObjectAdd;
      public event InventoryEventSelect EventObjectSelect;
      #endregion
      private SlotInventory[] slotsInventory;
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
      public  bool TryGetSlotFromItem(ItemScriptableObject item, ref SlotInventory slotInventory)
      {
         int index = GetSlotIndexFromItem(item);
         if (index == -1)
         {
            return false;
         }
         // Slot exist 
         slotInventory = ref GetSlotInventoryAt(index);
         return true;
      }

      private ref SlotInventory GetSlotInventoryAt(int index)
      {
         return ref slotsInventory[index];
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