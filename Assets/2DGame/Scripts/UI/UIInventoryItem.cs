using System;
using _2DGame.Scripts.Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace _2DGame.Scripts.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIInventoryItem : MonoBehaviour ,  IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler , IPointerExitHandler
    {
        private const float SpeedAnim = 5f;
        
        [SerializeField] private TextMeshProUGUI textAmount;
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private Image icon;
        
        private RectTransform _rectTransform;
        private Vector3 _initialScale;
        private Vector3 _initialLocalPosition;
        private bool _isOver;
        private bool _isSelected;
        private UIInventory _uiInventory;
        private SlotInventory _slotInventory;

        public bool IsSelected => _isSelected;
        public SlotInventory SlotInventory => _slotInventory;
        
        public void GenerateButton(UIInventory uiInventory , SlotInventory slotInventory)
        {
            _slotInventory = slotInventory;
            _uiInventory = uiInventory;
            RefreshValuesOfItem();

        }
        private void LerpScaleToTarget(Vector2 targetScale)
        {
            var currentSizeRect = _rectTransform.localScale;
            var lerp = Vector2.Lerp(currentSizeRect, targetScale , Time.unscaledDeltaTime * SpeedAnim);
            _rectTransform.localScale = lerp;
        }

        private void RefreshValuesOfItem()
        {
            if (_slotInventory.item == null)
            {
                textAmount.text = string.Empty;
                textName.text = string.Empty;
                icon.sprite = null;
                icon.color = Color.clear;
            }
            else
            {
                if(_slotInventory.amount > 1)
                {
                    textAmount.text = _slotInventory.amount.ToString();
                }
                else
                {
                    textAmount.text = string.Empty;
                }
                textName.text = _slotInventory.item.name;
                icon.sprite = _slotInventory.item.Icon;
                icon.color = Color.white;
            }
        }

        #region OnPointer

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isSelected = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isSelected = false;
        }

        #endregion
        #region DragHandler
        public void OnBeginDrag(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.localPosition = _initialLocalPosition;
            if (eventData.hovered[^1].GetComponent<Canvas>() == null)
            {
                for (int i = 0; i < eventData.hovered.Count; i++)
                {
                    if (eventData.hovered[i].TryGetComponent<UIInventoryItem>(out var uiInventoryItem))
                    {
                        var slotTempTarget = uiInventoryItem._slotInventory;
                        uiInventoryItem.GenerateButton(_uiInventory, _slotInventory);
                        GenerateButton(_uiInventory, slotTempTarget);
                        break;
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
        #region MonoBehaviour
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialScale = _rectTransform.localScale;
            GetComponent<Button>();
        }
        private void Start()
        {
            _initialLocalPosition = _rectTransform.localPosition;
            _slotInventory.EventItemUpdate += RefreshValuesOfItem;
        }
        private void OnDisable()
        {
            _rectTransform.localScale = _initialScale;
            _isOver = false;
            _rectTransform.localPosition = _initialLocalPosition;
            //_isSelected = false;
        }
        private void Update()
        {
            if(_isOver)
            {
                LerpScaleToTarget(_initialScale * 1.25f);
            }
            else
            {
                LerpScaleToTarget(_initialScale);
            }
        }
        #endregion


      
    }
}
