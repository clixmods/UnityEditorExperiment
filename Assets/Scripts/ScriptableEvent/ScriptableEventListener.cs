using System.Collections;
using System.Collections.Generic;
using ScriptableEvent;
using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListener : MonoBehaviour
{
    [SerializeField] 
    private ScriptableEvent.ScriptableEvent scriptableEvent;

    public UnityEvent CallbackEvent;
    // Start is called before the first frame update
    private void Start()
    {
        scriptableEvent.Event += Callback;
    }

    private void Callback()
    {
        Debug.Log("Yeah boi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ScriptableEventListener<T> : MonoBehaviour
{
    [SerializeField] 
    protected ScriptableEvent<T> scriptableEvent;

    public UnityEvent<T> oof;
    // Start is called before the first frame update
    private void Start()
    {
        scriptableEvent.Event += Callback;
    }

    private void Callback(T message)
    {
        Debug.Log("Yeah boi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
