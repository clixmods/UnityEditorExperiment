using _2DGame.Scripts.Item;

namespace _2DGame.Scripts
{
    public interface IGrabbable
    {
        /// <summary>
        /// Behavior when the grabbable object is grabbed
        /// </summary>
        /// <param name="inventory"></param>
        public void OnGrab(InventoryScriptableObject inventory);
    }
}
