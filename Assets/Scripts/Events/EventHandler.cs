using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public event Action<int> MyAction;
    
    public delegate void CallbackDamage( ref int amount);
    public event CallbackDamage eventDamage;

    [SerializeField] private bool _startInvoke;
    public int health = 50;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (_startInvoke)
        {
            eventDamage?.Invoke( ref health );
            _startInvoke = false;
        }
       
    }
}
