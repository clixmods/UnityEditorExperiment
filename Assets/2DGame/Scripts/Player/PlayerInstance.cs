using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
public class PlayerInstance : MonoBehaviour, ICharacter
{
    private const string AssetNamePlayerSettingDefault = "PlayerSettingsDefault";
    private float _health = 1;
    [SerializeField] private CharacterScriptableObject characterSettings;
    [SerializeField] private InventoryScriptableObject inventory;
    #region Properties
    public CharacterScriptableObject CharacterSetting => characterSettings;
    public float Health => _health;
    public InventoryScriptableObject Inventory => inventory;
    #endregion
    /// <summary>
    /// Method used to get default values for somes parameters to guarantee a correct execution
    /// </summary>
    private void GetDefaultValues()
    {
        if (characterSettings == null)
        {
            characterSettings = Resources.Load<CharacterScriptableObject>(AssetNamePlayerSettingDefault);
        }
    }
    private void Start()
    {
        GetDefaultValues();
    }

    public void DoDamage(int amount)
    {
        throw new NotImplementedException();
    }
    #if UNITY_EDITOR
        private void OnValidate()
        {
            GetDefaultValues();
        }    
    #endif
}
