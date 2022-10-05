using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StringComponent : MonoBehaviour
{
    [SerializeField] private string _myBeautifulString = "HIPPIE KAI MOTHER FUCKA";

    //[Range()]
    [SerializeField][Scene] private int test = 0;

    [SerializeField] [Scene] private string testStr= "bite";

    [SerializeField] [Scene] private float testfloat = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
