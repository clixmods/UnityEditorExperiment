using _2DGame.Scripts.Character;
using _2DGame.Scripts.Item;
using _2DGame.Scripts.Player;
using UnityEngine;
namespace _2DGame.Scripts.AI
{
    public class AIInstance : MonoBehaviour, ICharacter
    {
        public float Health { get; }
        public void DoDamage(int amount)
        {
            throw new System.NotImplementedException();
        }
        public CharacterScriptableObject CharacterSetting => characterScriptableObject;
        public InventoryScriptableObject Inventory { get; }
        [SerializeField] private CharacterScriptableObject characterScriptableObject;
        private CharacterMovement2D _characterMovement2D;
        private WeaponController _weaponController;
        private void Start()
        {
            _characterMovement2D = GetComponent<CharacterMovement2D>();
            _weaponController = GetComponent<WeaponController>();
        }
        private void Update()
        {
            var target = FindObjectOfType<PlayerInstance>();
            var directionWithTarget = target.transform.position - transform.position;
            _characterMovement2D.SetVelocity(directionWithTarget);
            _weaponController.SetAimDirection(directionWithTarget);
        }
    }
}
