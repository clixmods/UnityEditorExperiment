using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    private const float PI = 3.14f;
    
    [Range(1,360),SerializeField] private float radius = 2;
    [Range(2,30),SerializeField] private int resolution = 5;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float _distance; 

    private float Distance(Vector2 a, Vector2 b)
    {
        return (b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y);

    }
    private void OnDrawGizmos()
    {
        if (targetPoint != null)
        {
            Vector3 point = new Vector3(targetPoint.position.x, targetPoint.position.y, 0);    
            Vector3 pointCircle = new Vector3(transform.position.x, transform.position.y, 0);
            _distance = Distance(pointCircle, point);
            if ( _distance < radius* radius)
            {
                Draw(Color.green);
            }
            else
            {
                Draw(Color.white);
            }
        }
        else
        {
            Draw(Color.white);
        }
       
    }
    /// <summary>
    /// Draw a circle 
    /// </summary>
    /// <param name="color"> Select a color to draw</param>
    private void Draw(Color color)
    {
        Vector3 ogPosition = transform.position;
        float step = 2 * PI / resolution;
        int index = 0;
        for (float i = 0; i < 2 * PI; i += step) index++;

        Vector3[] points = new Vector3[index];
        index = 0;
        for (float i = 0; i < 2 * PI; i += step)
        {
            float x = ogPosition.x + (radius * Mathf.Cos(i));
            float y = ogPosition.y - (radius * Mathf.Sin(i));
            points[index] = new Vector3(x, y, 0);
            index++;
        }

        for (int i = 0; i < points.Length - 1; i++)
        {
            Debug.DrawLine(points[i], points[i + 1] , color);
        }

        Debug.DrawLine(points[^1], points[0] , color);
    }
}
