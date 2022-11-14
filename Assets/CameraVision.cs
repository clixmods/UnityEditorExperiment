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
    public float angle;

    public float cos;

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

        var endA = new Vector3(Mathf.Cos(angleA* (Mathf.PI / 180) ), Mathf.Sin(angleA* (Mathf.PI / 180))) * distance;
        var endB = new Vector3(Mathf.Cos(-angleA* (Mathf.PI / 180) ), Mathf.Sin(-angleA* (Mathf.PI / 180))) * distance;
        
        Vector3 directionA = transform.TransformPoint(endB) - cameraPosition;
        Vector3 directionB = transform.TransformPoint(endA) - cameraPosition;
        
        directionA =  Quaternion.Euler(offset) * directionA;
        directionB =  Quaternion.Euler(offset) * directionB;
        
      
        Debug.DrawRay(cameraPosition, directionA);
        Debug.DrawRay(cameraPosition, directionB);

        Vector3 directionObject = objectTarget.transform.position - cameraPosition;

        bool isGoodDistance = directionObject.magnitude < distance;
        
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
        
        
        var dotAddition = dotA + dotB;
        dotAddition = MathF.Round(dotAddition, 2);
        Debug.Log( "ADDTION VALUE DOT : "+dotAddition );
        if ( isGoodDistance &&  dotAddition <= cameraFov )
        {
            Debug.DrawRay(cameraPosition, directionObject,Color.green);
        }
        else
        {
            if (!isGoodDistance)
            {
                directionObject = directionObject /  ( (directionObject.magnitude / distance) );
                objectTarget.transform.position = directionObject + cameraPosition;
            }
            //objectTarget.transform.position = new Vector3(MathF.Cos(dotA), MathF.Sin(dotA))*distance;
            Debug.DrawRay(cameraPosition, directionObject,Color.yellow);
        }

    }
}
