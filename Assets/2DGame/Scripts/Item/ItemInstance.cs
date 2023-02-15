using UnityEngine;
namespace _2DGame.Scripts.Item
{
    [AddComponentMenu("2DGame/Item/Item Instance")]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemInstance : MonoBehaviour, IGrabbable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ItemScriptableObject itemGrabbable;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        private bool _isGrabbed;
        private Vector2 _spriteRendererSize = new Vector2(2, 1);
        /// <summary>
        /// Method used to get default values for somes parameters to guarantee a correct execution
        /// </summary>
        private void GetDefaultValues()
        {
            _boxCollider2D ??= GetComponent<BoxCollider2D>();
            _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            if (_spriteRenderer != null && itemGrabbable != null)
            {
                _spriteRenderer.sprite = itemGrabbable.Icon;
                // Give good proportion for the sprite Icon
                _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
                _spriteRendererSize = itemGrabbable.Icon.rect.size.normalized;
                _spriteRenderer.size = _spriteRendererSize;
                _boxCollider2D.isTrigger = true;
            }
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetDefaultValues();
            gameObject.name = $"Bonus : {itemGrabbable.name}";
        }
#endif
        private void Awake()
        {
            GetDefaultValues();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Prevent multiple OnTriggerEnter while the item is grabbed and not yet destroy
            if (_isGrabbed)
            {
                return;
            }
            if (other.TryGetComponent<IInventory>(out var inventory))
            {
                OnGrab(inventory.Inventory);
            }
        }
        public void OnGrab(InventoryScriptableObject targetInventory)
        {
            bool itemIsAddedToInventory = targetInventory.AddItem(itemGrabbable);
            if (itemIsAddedToInventory)
            {
                _isGrabbed = true;
                Destroy(gameObject);
            }
        }
    }
}
