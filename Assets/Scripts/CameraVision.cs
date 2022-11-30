using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CameraVision : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Transform objectTarget;
    public Vector3 offset = new Vector3(00, 90, 0);
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        var objectPosition =new Vector2( objectTarget.position.x,objectTarget.position.y);
        var cameraPosition = new Vector2(_camera.transform.position.x, _camera.transform.position.y);
    }

    private void OnDrawGizmos()
    {
        float cameraFov = _camera.fieldOfView;
        float angleA = cameraFov/2f;
        var cameraPosition = transform.position;
        
        // Distance perce
        float distance = _camera.farClipPlane ;

        float fA = angleA * (Mathf.PI / 180);
        var endA = new Vector3(Mathf.Cos( fA), Mathf.Sin(fA ),0)  * distance;

        float fB = -angleA * (Mathf.PI / 180);
        var endB = new Vector3(Mathf.Cos(fB ), Mathf.Sin(fB) ,0 ) * distance;
        
        
        
        Vector3 directionA = transform.TransformPoint (endB) - cameraPosition;
        Vector3 directionB = transform.TransformPoint (endA) - cameraPosition;
        
        directionA =  Quaternion.Euler(offset) * directionA;
        directionB =  Quaternion.Euler(offset) * directionB;
        
      
        Debug.DrawRay(cameraPosition, directionA);
        Debug.DrawRay(cameraPosition, directionB);

        Vector3 directionObject = objectTarget.transform.position - cameraPosition;

        float targetHypothenuse = distance / MathF.Cos(angleA);
     
        
        Handles.Label(objectTarget.transform.position, directionObject.magnitude.ToString());
        
        var dotA = Vector3.Dot(directionA, directionObject);
        dotA =  Mathf.Acos(dotA / (directionA.magnitude*directionObject.magnitude));
        dotA *= 180f/Mathf.PI;
        dotA = MathF.Round(dotA, 2);
        Handles.Label(endA + cameraPosition, dotA.ToString());
     
        var dotB = Vector3.Dot(directionB, directionObject);
        dotB =  Mathf.Acos(dotB / (directionB.magnitude*directionObject.magnitude));
        dotB  *= 180f/Mathf.PI;
        dotB = MathF.Round(dotB, 2);
        Handles.Label(endB + cameraPosition, dotB.ToString());
        float Hypothenuse;
        if (dotA < dotB)
        {
            float cosinus = MathF.Cos((angleA - dotA )* MathF.PI/180 );
            Hypothenuse = distance / cosinus ;
            targetHypothenuse = distance / MathF.Cos(angleA* (Mathf.PI / 180));
        }
        else
        {
            Hypothenuse = distance / MathF.Cos((angleA - dotB)* MathF.PI/180);
            targetHypothenuse = distance / MathF.Cos(-angleA* (Mathf.PI / 180));
        }
        bool isGoodDistance = directionObject.magnitude <= Hypothenuse;
        float dotAddition = dotA + dotB;
        dotAddition = MathF.Round(dotAddition, 2);
 
        if (  dotAddition <= cameraFov &&  isGoodDistance)
        {
            Debug.DrawRay(cameraPosition, directionObject,Color.green);
        }
        else
        {
            Debug.DrawRay(cameraPosition, directionObject,Color.yellow);
        }

    }
}
