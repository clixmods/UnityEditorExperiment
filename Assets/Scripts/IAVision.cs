using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class IAVision : MonoBehaviour
{
    enum LimitVisionType
    {
        ZeroToOne,
        Angle
    }
    [SerializeField] private float radius = 1;
    [SerializeField] private LimitVisionType typeCalcul;
    [LimitVision,SerializeField] private float limitVision = 0;
    
    public bool manual = false;
    public Vector3 transformUp;

    public Vector3 transformRight;
    
    
    // Start is called before the first frame update
    void Start()
    {
        transformRight = transform.right;
        transformUp = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        // Angle * r pour vitesse angulaire
        // produit scalaire : valeur dun vecteur en le projetant sur un autre

        if (!manual)
        {
            transformRight = transform.right;
            transformUp = transform.up;
        }

        // // Vector3 arf = new Vector3(Mathf.Cos(limitVision), MathF.Sin(limitVision));
        //
        Debug.DrawLine(transform.position,transform.TransformPoint(transformRight* radius)  );

        Vector3 endPosition;
        float i;
        switch (typeCalcul)
        {
            case LimitVisionType.ZeroToOne:
                i = limitVision * (2 * 3.14f);
                endPosition = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0);
                break;
            case LimitVisionType.Angle:
                i = (limitVision / 360 ) *  (2 * 3.14f) ;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
        endPosition = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0);
        
        
        Debug.DrawLine(transform.position,transform.TransformPoint((endPosition)* radius )   );
        //
        //
        
         //Debug.DrawLine(transform.position, transform.TransformPoint( (transformUp+arf)  * radius)  , Color.green ) ;
    }
    
    
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class LimitVisionAttribute : PropertyAttribute { }
