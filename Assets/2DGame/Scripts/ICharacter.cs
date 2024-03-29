using _2DGame.Scripts.Character;
using _2DGame.Scripts.Item;

namespace _2DGame.Scripts
{
    /// <summary>
    /// Interface to implement character methods and setting in a class.
    /// </summary>
    public interface ICharacter
    {
        public float Health { get; }
        public void DoDamage(int amount);
        /// <summary>
        /// Setting for the character, you can set the speed, skin etc
        /// </summary>
        public CharacterScriptableObject CharacterSetting { get; }
        /// <summary>
        /// Inventory of the character
        /// </summary>
        public InventoryScriptableObject Inventory { get; }
    }
}
