using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Collision
{
    Point,
    Circle
}

public class ShapeDrawer : MonoBehaviour
{
    /// <summary>
    /// Constant of PI, cached here to not use Mathf class
    /// </summary>
    private const float PI = 3.14f;

    /// <summary>
    /// Size of the shape, also the distance between the origin and the edge of the shape.
    /// </summary>
    [Header("Shape")] [Range(1, 360), SerializeField]
    private float radius = 2;

    /// <summary>
    /// Size of the shape
    /// </summary>
    [Range(2, 30), SerializeField] private int resolution = 5;

    /// <summary>
    /// Density of vertices in the shape
    /// </summary>
    [Range(2, 30), SerializeField] private int density = 2;

    [Header("Collision")] [SerializeField] private Collision collisionType;
    [SerializeField] private Transform targetPoint;
    private ShapeDrawer _targetShapeDrawer;

    public float Radius => radius;
    public Vector3 Position => transform.position;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (targetPoint.TryGetComponent<ShapeDrawer>(out var shapeDrawer))
        {
            _targetShapeDrawer = shapeDrawer;
        }
        else
        {
            collisionType = Collision.Point;
            _targetShapeDrawer = null;
        }
    }

    private void OnDrawGizmos()
    {
        WatchCollision();
    }

    private float Distance(Vector2 a, Vector2 b)
    {
        return (b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y);
    }


    private float MaxDistance()
    {
        switch (collisionType)
        {
            case Collision.Point:
                return radius * radius;
            case Collision.Circle:
            {
                return (radius + _targetShapeDrawer.radius) * (radius + _targetShapeDrawer.radius);
            }
        }

        return 0;
    }

    private void WatchCollision()
    {
        if (targetPoint != null)
        {
            Vector3 point = new Vector3(targetPoint.position.x, targetPoint.position.y, 0);
            Vector3 pointCircle = new Vector3(transform.position.x, transform.position.y, 0);
            float distance = Distance(pointCircle, point);
            if (distance < MaxDistance())
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
    /// Draw the shape 
    /// </summary>
    /// <param name="color">Select a color to draw</param>
    private void Draw(Color color)
    {
        Vector3 center = transform.position;
        float step = 2 * PI / resolution;
        int pointsAmount = 0;
        for (float i = 0; i < 2 * PI; i += step)
        {
            pointsAmount++;
        }

        Vector3[] points = new Vector3[pointsAmount];
        int index = 0;
        for (float i = 0; i < 2 * PI; i += step)
        {
            float x = center.x + (radius * Mathf.Cos(i));
            float y = center.y - (radius * Mathf.Sin(i));
            points[index] = new Vector3(x, y, 0);
            index++;
        }

        // Draw each line of the shape
        for (int i = 0; i < points.Length - 1; i++)
        {
            Debug.DrawLine(points[i], points[i + 1], color);
        }

        Debug.DrawLine(points[^1], points[0], color);
        // Draw vertices
        for (int i = 0; i < points.Length; i++)
        {
            Debug.DrawLine(points[i], points[(i + density) % points.Length], color);
        }
    }
#endif
}