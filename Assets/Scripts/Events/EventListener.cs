using System;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [SerializeField] 
    private EventHandler _eventHandler;
    [SerializeField] private UnityEvent _OnTriggerInvoked;
    private void Start()
    {
        _eventHandler.eventDamage += Decrement;
        _eventHandler.eventDamage += Divide;
    }
    void Decrement( ref int amount)
    {
        amount--;
        Debug.Log(amount);
        _OnTriggerInvoked?.Invoke();
        //return amount;
    }
    void Divide( ref int amount)
    {
        amount /= 2;
        Debug.Log(amount);
        //return amount;
    }
}
