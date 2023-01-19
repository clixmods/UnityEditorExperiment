using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Item/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isStackable;
    [Range(1,128)]
    [SerializeField] private int _stackMaxQuantity;
    public string name => _name;
    public Sprite icon => _icon;
    public bool IsStackable => _isStackable;
    public int StackMaxQuantity => _stackMaxQuantity;
}
