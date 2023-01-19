using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings", menuName = "Settings/Player Settings ", order = 0)]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField] private float _speed = 4;
    public float Speed => _speed;
}
