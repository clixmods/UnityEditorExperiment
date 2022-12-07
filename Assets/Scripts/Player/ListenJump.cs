using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenJump : MonoBehaviour
{
    [SerializeField] private InputScriptableEventButton _inputScriptableEventJump;

    [SerializeField] private string message = "I have Jump";

    // Start is called before the first frame update
    void Start()
    {
        _inputScriptableEventJump.Event += OnJumped;
    }

    void OnJumped()
    {
        Debug.Log($"{message} from gameobject {gameObject.name}");
    }
}