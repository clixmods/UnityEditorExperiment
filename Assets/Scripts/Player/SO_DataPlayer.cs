using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class DataPlayer : ScriptableObject
    {
        public event Action HealthChanged;
        [field: SerializeField]
        public float Name { get; private set; }
        [field: SerializeField] 
        [field: Range(0,200)]
        public int Health { get; private set; }

        private int _currentLifePoints;
        public int CurrentLifePoints
        {
            get => _currentLifePoints;
            set
            {
                _currentLifePoints = Mathf.Clamp(value,0, Health);
                HealthChanged?.Invoke();
            } 
        }
        private void OnEnable()
        {
            _currentLifePoints = Health;
        }
    }
}