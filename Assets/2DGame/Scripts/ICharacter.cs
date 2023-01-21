using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public float Health { get; }
    public void DoDamage(int amount);
    public CharacterScriptableObject CharacterSetting { get; }
    public InventoryScriptableObject Inventory { get; }
}
