using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Collision
{
    Point,
    Circle
}
public class ShapeGenerator : MonoBehaviour
{
    private const float PI = 3.14f;

    [SerializeField] private Collision collisionType;
    [Range(1,360),SerializeField] private float radius = 2;
    [Range(2,30),SerializeField] private int resolution = 5;
    [Range(2,30),SerializeField] private int density = 2;

    [SerializeField] private Transform targetPoint;
    [SerializeField] private ShapeGenerator targetShapeGenerator;
    private float _distance;

    public float Radius => radius;
    public Vector3 Position => transform.position;

    private float Distance(Vector2 a, Vector2 b)
    {
        return (b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y);

    }
    private void OnDrawGizmos()
    {
        switch (collisionType)
        {
            case Collision.Point:
                WatchCollisionWithPoint();
                break;
            case Collision.Circle:
                WatchCollisionWithCircle();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
      
    }

    private void WatchCollisionWithPoint()
    {
        if (targetPoint != null)
        {
            Vector3 point = new Vector3(targetPoint.position.x, targetPoint.position.y, 0);
            Vector3 pointCircle = new Vector3(transform.position.x, transform.position.y, 0);
            _distance = Distance(pointCircle, point);
            if (_distance < radius * radius)
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
    
    private void WatchCollisionWithCircle()
    {
        if (targetShapeGenerator != null)
        {
            Vector3 point = new Vector3(targetShapeGenerator.Position.x, targetShapeGenerator.Position.y, 0);
            Vector3 pointCircle = new Vector3(transform.position.x, transform.position.y, 0);
            _distance = Distance(pointCircle, point);
            if (_distance < (radius  +  targetShapeGenerator.radius ) * (radius  +  targetShapeGenerator.radius ) )
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

        //Cela doit donner comme résultat, par exemple pour un densité égale à 2, avec 5 vertices de dessiner un pentagramme et avec 6 vertices un hexagramme.
        // Draw vertices
        for (int i = 0; i < points.Length ; i++)
        {
            Debug.DrawLine(points[i], points[(i + density) % points.Length] , color);
        }
    }
}