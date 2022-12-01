using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableEvent
{
    [CreateAssetMenu(fileName = "new_ScriptableEvent" , menuName ="Events/Scriptable Event")]
    public abstract class ScriptableEvent : ScriptableObject
    {
        public event Action Event;

        public void LaunchEvent()
        {
            Event?.Invoke();
        }
    
    }

    [CreateAssetMenu(fileName = "new_ScriptableEventT" , menuName ="Events/Scriptable Event")]
    public abstract class ScriptableEvent<T> : ScriptableObject
    {
        public event Action<T> Event;

        public void LaunchEvent(T value)
        {
            Event?.Invoke(value);
        }
    
    }
}

