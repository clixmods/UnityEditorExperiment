using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public float Health { get; }
    public void DoDamage(int amount);
    public InventoryScriptableObject Inventory { get; }
}
