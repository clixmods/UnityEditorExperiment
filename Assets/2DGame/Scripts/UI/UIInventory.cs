using System;
using System.Linq;
using _2DGame.Scripts.Item;
using UnityEngine;
namespace _2DGame.Scripts.UI
{
    public class UIInventory : MonoBehaviour 
    {
        /// <summary>
        /// Constant of PI, cached here to not use Mathf class
        /// </summary>
        private const float PI = 3.14f;
        /// <summary>
        /// Size of the shape, also the distance between the origin and the edge of the shape.
        /// </summary>
        private float _radius = 2;
        private int _resolution = 5;
        [SerializeField] private InventoryScriptableObject _inventory;
        [SerializeField] private GameObject buttonItem;
        private GameObject[] buttonsItem = Array.Empty<GameObject>();
        private SlotInventory _selectedSlot;
        public void SetSelectedSlot(SlotInventory slotInventory)
        {
            _inventory.SetSelectedSlot(slotInventory);
        }
        private void OnDrawGizmosSelected()
        {
            if(!Application.isPlaying)
                RefreshUI();
        }
        private void RefreshUI()
        {
            RefreshUI(false);
        }
        private void RefreshUI(bool destroyPreviousButton)
        {
            _radius = ((RectTransform)transform).rect.max.magnitude;
            _resolution = _inventory.SlotsAmount;
            foreach (Transform child in transform)
            {
                if (!buttonsItem.Contains(child.gameObject))
                {
                    if (Application.isPlaying)
                    {
                        Destroy(child.gameObject);
                    }
                    else
                    {
                        DestroyImmediate(child.gameObject);
                    }
                }
            }
            if (destroyPreviousButton || buttonsItem.Length != _resolution)
            {
                buttonsItem = new GameObject[_resolution];
            }
            Draw(Color.blue);
        }
        /// <summary>
        /// Draw the shape 
        /// </summary>
        /// <param name="color">Select a color to draw</param>
        private void Draw(Color color)
        {
            Vector3 center = Vector3.zero;
            float step = 2 * PI / _resolution;
            int pointsAmount = 0;
            for (float i = 0; i < 2 * PI; i += step)
            {
                pointsAmount++;
            }
            // Determine where buttons can be placed in the circular inventory
            Vector3[] points = new Vector3[pointsAmount];
            int index = 0;
            for (float i = 0; i < 2 * PI; i += step)
            {
                float x = center.x + (_radius * Mathf.Cos(i));
                float y = center.y - (_radius * Mathf.Sin(i));
                points[index] = new Vector3(x, y, 0);
                index++;
            }
            // Draw each line of the shape
            for (int i = 0; i < _resolution; i++)
            {
                if(buttonsItem[i] == null)
                    buttonsItem[i] = Instantiate(buttonItem,points[i],Quaternion.identity,transform);
                // Attribute good position 
                buttonsItem[i].transform.localPosition = points[i];
                // Get item and setup the UI Item Button in inventory
                var slotInventory = _inventory.GetSlotsInventory()[i];
                var uiInventoryItem = buttonsItem[i].GetComponent<UIInventoryItem>();
                uiInventoryItem.GenerateButton(this,slotInventory);
            }
        }
        private void Awake()
        {
            _inventory.EventObjectAdd += RefreshUI;
            RefreshUI(true);
        }
        private void OnDisable()
        {
            for (int i = 0; i < buttonsItem.Length; i++)
            {
                var uiInventoryItem = buttonsItem[i].GetComponent<UIInventoryItem>();
                if (uiInventoryItem.IsSelected)
                {
                    _inventory.SetSelectedSlot(uiInventoryItem.SlotInventory);
                }
            }
        }
    }
}
