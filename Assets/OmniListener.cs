using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OmniListener : MonoBehaviour
{
    [SerializeField] private InputScriptableEventVector2 _EventMove;
    [SerializeField] private InputScriptableEventButton _EventJump;
    
    [SerializeField] private InputScriptableEvent<Vector2> _Event;
    [SerializeField] private InputScriptableEvent<float> _EventFloat;
    
    [SerializeField] private UnityEvent<Vector2> callbackMove;
    [SerializeField] private UnityEvent callbackJump;
    // Start is called before the first frame update
    void Start()
    {
        _EventMove.Event += callbackMove.Invoke;
        _EventJump.Event += callbackJump.Invoke;
    }
}
