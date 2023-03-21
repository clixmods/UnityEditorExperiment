using System;
using _2DGame.Scripts.Character;
using _2DGame.Scripts.Item;
using _2DGame.Scripts.Save;
using UnityEngine;

namespace _2DGame.Scripts.Player
{
    [RequireComponent((typeof(Rigidbody2D)))]
    public class PlayerInstance : MonoBehaviourSaveable, ICharacter
    {
        private const string AssetNamePlayerSettingDefault = "PlayerSettingsDefault";
        private float _health = 1;
        private WeaponController _weaponController;
        [SerializeField] private CharacterScriptableObject characterSettings;
        [SerializeField] private InventoryScriptableObject inventory;
        private SlotInventory _slotInventorySelected;
        private SlotInventory _ammoSlotInventoryWeapon;
        [SerializeField] private Rigidbody2D ammoRb;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float ammoSpeed;
        [SerializeField] private GameObject weapon;
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
                _weaponController.SetView(_slotInventorySelected.item.SpriteWorld);
                if (inventory.TryGetSlotFromItem(_slotInventorySelected.item.AmmoItem, out var ammoSlotInventory))
                {
                    _ammoSlotInventoryWeapon = ammoSlotInventory;
                }
                else
                {
                    _ammoSlotInventoryWeapon = null;
                }
            }
            else
            {
                _weaponController.SetView(null);
            }
        }
        // TODO : Move weapon behaviour in weapon controller
        private void WeaponControllerOnEventWeaponFire()
        {
            if ( _ammoSlotInventoryWeapon != null && _ammoSlotInventoryWeapon.amount > 0)
            {
                Debug.Log("Weapon fire");
                _ammoSlotInventoryWeapon.amount--;
                Rigidbody2D ammo = Instantiate(ammoRb, spawnPoint.position, Quaternion.identity);
                ammo.velocity = transform.TransformDirection(weapon.transform.localPosition * Vector2.right);
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

        #region Save & Load
        class PlayerSaveData : SaveData
        {
            public Vector3 position;
            public Quaternion rotation;
        }
        public override void OnLoad(string data)
        {
            PlayerSaveData playerSaveData = JsonUtility.FromJson<PlayerSaveData>(data);
            transform.position = playerSaveData.position;
            transform.rotation = playerSaveData.rotation;
        }
        public override void OnSave(out SaveData saveData)
        {
            PlayerSaveData playerSaveData = new PlayerSaveData();

            playerSaveData.position = transform.position;
            playerSaveData.rotation = transform.rotation;
            
            saveData = playerSaveData;
        }
        #endregion
        
    }
}
