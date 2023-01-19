using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory", order = 0)]
public class InventoryScriptableObject : ScriptableObject
{
   [SerializeField] private Dictionary<ItemScriptableObject, int> Items = new Dictionary<ItemScriptableObject, int>();

#if UNITY_EDITOR
   public ItemScriptableObject[] GetItems()
   {
      if (Items == null || Items.Keys.Count == 0) return new ItemScriptableObject[0];
      var tempArray = new ItemScriptableObject[Items.Keys.Count];
      int i = 0;
      foreach (KeyValuePair<ItemScriptableObject, int> pair in Items)
      {
         tempArray[i] = pair.Key;
         i++;
      }
      return tempArray;
   }

   public int GetItemQuantity(ItemScriptableObject item)
   {
      return Items[item];
   }

   public bool AddItem(ItemScriptableObject item)
   {
      if (Items.ContainsKey(item))
      {
         Items[item]++;
         return true;
      }
      else
      {
         Items.Add(item,1);
         return true;
      }
   }
#endif
}
