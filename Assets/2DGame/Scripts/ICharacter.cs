using _2DGame.Scripts.Character;
using _2DGame.Scripts.Item;

namespace _2DGame.Scripts
{
    /// <summary>
    /// Interface to implement character methods and setting in a class.
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// Setting for the character, you can set the speed, skin etc
        /// </summary>
        public CharacterScriptableObject CharacterSetting { get; }
      
    }
}
