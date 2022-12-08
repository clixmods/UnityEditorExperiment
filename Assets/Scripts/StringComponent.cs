using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stats
{
    [SerializeField]
    private int health;
    [SerializeField]  
    private int maxHealth;
    
    [SerializeField] 
    private int mana;
    
    [SerializeField]  
    private int maxMana;
    public Stats(int health, int mana)
    {
        this.health = health;
        this.mana = mana;
        maxHealth = 300;
        maxMana = 100;
    }
}

public class StringComponent : MonoBehaviour
{
    [SerializeField] private string _myBeautifulString = "HIPPIE KAI MOTHER FUCKA";
    [SerializeField][Scene] private int sceneInt = 0;
    [SerializeField] [Scene] private string sceneString = "none";
    [SerializeField] [Scene] private float sceneFloat = 0f;
    
    [SerializeField] private Stats _stats = new Stats(100,100);
    // Start is called before the first frame update
    void Start()
    {
    
    }
}
