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
        float angleA = cameraFov / 2f;
        float angleB = -angleA;
       
        float distance = _camera.farClipPlane ;
        var i = (angleA / 360 ) *  (2 * 3.14f) ;
        var endA = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0) * distance;
        var y = (angleB / 360 ) *  (2 * 3.14f) ;
        var endB = new Vector3(Mathf.Cos(y), Mathf.Sin(y), 0) * distance;
        

        Vector3 directionA = transform.TransformPoint(endB) - transform.position;
        Vector3 directionB = transform.TransformPoint(endA) - transform.position;
        
        
        directionA =  Quaternion.Euler(offset) * directionA;
        directionB =  Quaternion.Euler(offset) * directionB;
        
        Vector3 directionObject = objectTarget.transform.position - transform.position;
        Debug.DrawRay(transform.position, directionA);
        Debug.DrawRay(transform.position, directionB);

        bool isGoodDistance = directionObject.magnitude < distance;
        var dotA = Vector3.Dot(directionA.normalized, directionObject.normalized);
        dotA -= cameraFov/180f;
        Handles.Label(endA+ transform.position, dotA.ToString());
        var dotB = Vector3.Dot(directionB.normalized, directionObject.normalized);
        dotB -= cameraFov/180f;
        Handles.Label(endB+ transform.position, dotB.ToString());
        
        
        
        if ( isGoodDistance && dotA >= 0.5f && dotB  >= 0.5f && dotA + dotB  <= 2 )
        {
            Debug.DrawRay(transform.position, directionObject,Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, directionObject,Color.yellow);
        }
        Debug.Log("DOT A :"+dotA );
        Debug.Log("DOT B :"+dotB);
        
        
    }
}
