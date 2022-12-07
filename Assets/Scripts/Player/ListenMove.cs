using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ListenMove : MonoBehaviour
{
    [SerializeField] private InputScriptableEventVector2 _inputScriptableEventMove;

    // Start is called before the first frame update
    void Start()
    {
        _inputScriptableEventMove.Event += OnMoved;
    }

    void OnMoved(Vector2 value)
    {
        Debug.Log($" Vector2 Value : {value} from gameobject {gameObject.name}");
    }
}