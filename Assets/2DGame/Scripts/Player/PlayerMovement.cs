using UnityEngine;
using UnityEngine.InputSystem;
namespace _2DGame.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerInstance))]
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInstance _playerInstance;
        private Rigidbody2D _rigidbody2D;
        #region Properties
        private float speed => _playerInstance.CharacterSetting.Speed;
        #endregion
        private void Start()
        {
            _playerInstance = GetComponent<PlayerInstance>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 direction;
            direction = context.ReadValue<Vector2>();
            _rigidbody2D.velocity = direction * speed;
        }
    }
}
