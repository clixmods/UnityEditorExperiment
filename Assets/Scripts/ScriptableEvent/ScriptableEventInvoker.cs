using System.Collections;
using System.Collections.Generic;
using ScriptableEvent;
using UnityEngine;

public class ScriptableEventInvoker : MonoBehaviour
{
    [SerializeField] 
    private ScriptableEvent<string> triggerEvent;
    // Start is called before the first frame update
    void Start()
    {
        triggerEvent.LaunchEvent("oof");
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ScriptableEventInvoker<T> : MonoBehaviour
{
    [SerializeField] private T _value;
    [SerializeField] 
    private ScriptableEvent<T> triggerEvent;
    // Start is called before the first frame update
    void Start()
    {
        triggerEvent.LaunchEvent(_value);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
