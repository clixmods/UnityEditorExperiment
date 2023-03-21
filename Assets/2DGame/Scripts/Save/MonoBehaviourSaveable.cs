﻿using UnityEngine;

namespace _2DGame.Scripts.Save
{
    public abstract class MonoBehaviourSaveable : MonoBehaviour , ISaveInstance
    {
        [SerializeField] [HideInInspector] private int _saveID = DataPersistentUtility.GenerateID();
        public abstract void OnLoad(string data);
        public abstract void OnSave(out SaveData saveData);
        public int SaveID  => _saveID;
    }
}