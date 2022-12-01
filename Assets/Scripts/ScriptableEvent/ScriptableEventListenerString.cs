using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityTemplateProjects.ScriptableEvent;

public class ScriptableEventListenerString : ScriptableEventListener<string>
{
    [SerializeField] 
    private ScriptableEventString scriptableEvent;

    public UnityEvent<StringComponent> oof;
    // Start is called before the first frame update
    private void Start()
    {
        scriptableEvent.Event += Callback;
    }

    private void Callback(string message)
    {
        Debug.Log("Yeah boi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
