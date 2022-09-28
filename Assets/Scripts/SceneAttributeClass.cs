using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class SceneAttribute : PropertyAttribute
{
    public int min = 0;
    public int max = 0;
    public string oof = "test";
    
}