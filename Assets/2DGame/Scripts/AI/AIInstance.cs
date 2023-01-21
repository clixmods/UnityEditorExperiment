using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInstance : MonoBehaviour, ICharacter
{
    public float Health { get; }
    public void DoDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
    public CharacterScriptableObject CharacterSetting { get; }
    public InventoryScriptableObject Inventory { get; }
    private CharacterMovement2D _characterMovement2D;

    private void Start()
    {
        _characterMovement2D = GetComponent<CharacterMovement2D>();
    }

    private void Update()
    {
        _characterMovement2D.SetVelocity(FindObjectOfType<PlayerInstance>().transform.position - transform.position);
    }
}
