using UnityEngine;

namespace _2DGame.Scripts.Save
{
    /// <summary>
    /// ScriptableObject compatible with DataPersistantSystem
    /// </summary>
    public abstract class ScriptableObjectSaveable : ScriptableObject, ISave
    {
        public abstract void OnLoad(string data);
        public abstract void OnSave(out SaveData saveData);
    }
}