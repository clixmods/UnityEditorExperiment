using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollbarPlus : MonoBehaviour
{
    private ScrollRect _scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0 ; i < _scrollRect.content.childCount ; i++)
        {
            Transform child = _scrollRect.content.GetChild(i); 
            if (child == EventSystem.current.currentSelectedGameObject.transform)
            {
                float scrollSize = _scrollRect.verticalScrollbar.size;
                float sizeOfContent = _scrollRect.content.childCount;
                _scrollRect.verticalScrollbar.value = Mathf.Clamp(( 1 - (i ) / sizeOfContent),0,1);
            }
        }
    }
}
