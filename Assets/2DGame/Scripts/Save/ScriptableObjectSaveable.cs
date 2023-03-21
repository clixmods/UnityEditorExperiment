using UnityEngine;

namespace _2DGame.Scripts.Save
{
    public abstract class ScriptableObjectSaveable : ScriptableObject, ISaveData
    {
        public abstract void OnLoad(string data);
        public abstract void OnSave(out SaveData saveData);
    }
}