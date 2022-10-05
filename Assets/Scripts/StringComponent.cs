using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stats
{ 
    [Range(0,100)] 
    [SerializeField]
    private int Health;
    [SerializeField]
    private int maxhealth;
    [Range(0,100)] 
    [SerializeField] 
    private int Mana;
    [SerializeField]
    private int maxmana;
    public Stats(int health, int mana)
    {
        this.Health = health;
        this.Mana = mana;
        maxhealth = 100;
        maxmana = 100;
    }
}

public class StringComponent : MonoBehaviour
{
    [SerializeField] private string _myBeautifulString = "HIPPIE KAI MOTHER FUCKA";

    //[Range()]
    [SerializeField][Scene] private int test = 0;

    [SerializeField] [Scene] private string testStr= "bite";

    [SerializeField] [Scene] private float testfloat = 1f;


    [SerializeField] private Stats _stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
