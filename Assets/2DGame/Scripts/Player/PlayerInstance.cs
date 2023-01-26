using System;
using _2DGame.Scripts.Character;
using _2DGame.Scripts.Item;
using UnityEngine;

namespace _2DGame.Scripts.Player
{
    [RequireComponent((typeof(Rigidbody2D)))]
    public class PlayerInstance : MonoBehaviour, ICharacter
    {
        private const string AssetNamePlayerSettingDefault = "PlayerSettingsDefault";
        private float _health = 1;
        private WeaponController _weaponController;
        [SerializeField] private CharacterScriptableObject characterSettings;
        [SerializeField] private InventoryScriptableObject inventory;
        private SlotInventory _slotInventorySelected;
        private SlotInventory _ammoSlotInventoryWeapon;


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
            _slotInventorySelected = slotInventory;
            if (_slotInventorySelected.item.Type == ItemType.Weapon)
            {
                _weaponController.SetView(_slotInventorySelected.item.SpriteWorld);
                if (inventory.TryGetSlotFromItem(_slotInventorySelected.item.AmmoItem, out var ammoSlotInventory))
                {
                    _ammoSlotInventoryWeapon = ammoSlotInventory;
                }
            }
            else
            {
                _weaponController.SetView(null);
            }
        }
        private void WeaponControllerOnEventWeaponFire()
        {
            if ( _ammoSlotInventoryWeapon.amount > 0)
            {
                Debug.Log("Weapon fire");
                _ammoSlotInventoryWeapon.amount--;
            }
        }
        public void DoDamage(int amount)
        {
            throw new NotImplementedException();
        }
        
        private void Start()
        {
            _weaponController = GetComponent<WeaponController>();
            _weaponController.EventWeaponFire += WeaponControllerOnEventWeaponFire;
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
