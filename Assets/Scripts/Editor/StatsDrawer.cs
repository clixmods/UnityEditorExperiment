using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(Stats))]
    public class StatsDrawer : PropertyDrawer
    {
        //public SerializedProperty 
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var propertyHealth = property.FindPropertyRelative("health");
            var propertyMana = property.FindPropertyRelative("mana");
            
            // Stock value
            int healthValue =  propertyHealth.intValue;
            int maxhealthValue = property.FindPropertyRelative("maxHealth").intValue;
            
            int manaValue =  propertyMana.intValue;
            int maxmanaValue = property.FindPropertyRelative("maxMana").intValue;
            
            // Draw slider for health and mana
            propertyHealth.intValue = EditorGUILayout.IntSlider("Health" , healthValue, 0, maxhealthValue);
            propertyMana.intValue = EditorGUILayout.IntSlider( "Mana" , manaValue, 0, maxmanaValue);

            Rect rHealth = EditorGUILayout.BeginVertical();
            EditorGUI.ProgressBar(rHealth, (float)healthValue/(float)maxhealthValue , $"{healthValue}/{maxhealthValue}" );
            GUILayout.Space(16);
            EditorGUILayout.EndVertical();
            Rect rMana = EditorGUILayout.BeginVertical();
            EditorGUI.ProgressBar(rMana, (float)manaValue/(float)maxmanaValue , $"{manaValue}/{maxmanaValue}" );
            GUILayout.Space(16);
            EditorGUILayout.EndVertical();
            
            EditorGUI.EndProperty(); 
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }
    }
