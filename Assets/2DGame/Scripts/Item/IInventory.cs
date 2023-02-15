namespace _2DGame.Scripts.Item
{
    public interface IInventory
    {
        /// <summary>
        /// Inventory of the character
        /// </summary>
        public InventoryScriptableObject Inventory { get; }
    }
}