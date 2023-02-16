using UnityEngine;
namespace _2DGame.Scripts.Character
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "Settings/Player Settings ", order = 0)]
    public class CharacterScriptableObject : ScriptableObject
    {
        [Tooltip("Speed of the character applied while it moves in 2D world")]
        [SerializeField] private float _speed = 4;
        /// <summary>
        /// Speed of the character applied while it moves in 2D world
        /// </summary>
        public float Speed => _speed;
        [Tooltip("The force applied for the character to jump")]
        [SerializeField] private float _jumpForce = 10;
        /// <summary>
        /// The force applied for the character to jump
        /// </summary>
        public float JumpForce => _jumpForce;
        [Tooltip("The appearance of the character in the 2D world ")]
        [SerializeField] private Sprite characterSprite;
        /// <summary>
        /// The appearance of the character in the 2D world 
        /// </summary>
        public Sprite CharacterSprite => characterSprite;
    }
}
