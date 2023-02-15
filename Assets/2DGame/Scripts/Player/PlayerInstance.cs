using System;
using _2DGame.Scripts.Character;
using _2DGame.Scripts.Item;
using UnityEngine;

namespace _2DGame.Scripts.Player
{
    [RequireComponent((typeof(Rigidbody2D)))]
    public class PlayerInstance : MonoBehaviour, ICharacter, IInventory, IDamageable
    {
        private const string AssetNamePlayerSettingDefault = "PlayerSettingsDefault";
        private float _health = 1;
        private WeaponController _weaponController;
        [SerializeField] private CharacterScriptableObject characterSettings;
        [SerializeField] private InventoryScriptableObject inventory;
        private SlotInventory _slotInventorySelected;
       
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
        private void InventoryOnEventObjectSelect(SlotInventory slotInventory)
        {
            if (slotInventory.item == null)
            {
                return;
            }
            _slotInventorySelected = slotInventory;
            if (_slotInventorySelected.item.Type == ItemType.Weapon)
            {
                SlotInventory ammoSlotInventory = null;
                if (inventory.TryGetSlotFromItem(_slotInventorySelected.item.AmmoItem, ref ammoSlotInventory ))
                {
                    _weaponController.SetWeapon(_slotInventorySelected.item.SpriteWorld,ref ammoSlotInventory.GetRefAmount());
                }
                else
                {
                    _weaponController.SetWeapon(_slotInventorySelected.item.SpriteWorld );
                }
            }
        }
        public void DoDamage(int amount)
        {
            _health -= amount;
        }
        private void Start()
        {
            _weaponController = GetComponentInChildren<WeaponController>();
            GetDefaultValues();
            inventory.EventObjectSelect += InventoryOnEventObjectSelect;
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetDefaultValues();
        }    
#endif
    }
}
