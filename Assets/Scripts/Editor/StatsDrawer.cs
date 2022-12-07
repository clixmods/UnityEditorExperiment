using UnityEditor;
using UnityEngine;


    [CustomPropertyDrawer(typeof(Stats))]
    public class StatsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var propertyStats = property;
            // Stock value
            int healthValue = propertyStats.FindPropertyRelative("Health").intValue;
            int maxhealthValue = propertyStats.FindPropertyRelative("maxhealth").intValue;
            int manaValue = propertyStats.FindPropertyRelative("Mana").intValue;
            int maxmanaValue = propertyStats.FindPropertyRelative("maxmana").intValue;
            // Draw slider for health and mana
            Rect IntSliderHealth = EditorGUILayout.BeginVertical();
            propertyStats.FindPropertyRelative("Health").intValue = EditorGUI.IntSlider(IntSliderHealth, healthValue, 0, maxhealthValue);
            GUILayout.Space(16);
            EditorGUILayout.EndVertical();
            Rect IntSliderMana = EditorGUILayout.BeginVertical();
            propertyStats.FindPropertyRelative("Mana").intValue = EditorGUI.IntSlider(IntSliderMana, manaValue, 0, maxmanaValue);
            GUILayout.Space(16);
            EditorGUILayout.EndVertical();
            
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
