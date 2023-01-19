using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    
    ///
    /// <summary>
    /// Constant of PI, cached here to not use Mathf class
    /// </summary>
    private const float PI = 3.14f;
    /// <summary>
    /// Size of the shape, also the distance between the origin and the edge of the shape.
    /// </summary>
    [Header("Shape")] 
    [Range(1, 360),SerializeField] private float radius = 2;
    [Range(2, 30), SerializeField] private int resolution = 5;
    [Range(2, 30), SerializeField] private int density = 2;

    [SerializeField] private InventoryScriptableObject _inventory;
    [SerializeField] private GameObject buttonItem;
    private GameObject[] buttonsItem = Array.Empty<GameObject>();
    private void OnDrawGizmosSelected()
    {
        radius = ((RectTransform)transform).rect.width * 0.5f;
        resolution = _inventory.GetItems().Length;
        foreach (var button in buttonsItem)
        {
            DestroyImmediate(button);
        }
        buttonsItem = new GameObject[resolution];
        
        Draw(Color.blue);
    }

    private void Update()
    {
        radius = ((RectTransform)transform).rect.width * 0.5f;
        resolution = _inventory.GetItems().Length;
        foreach (var button in buttonsItem)
        {
            DestroyImmediate(button);
        }
        buttonsItem = new GameObject[resolution];
        
        Draw(Color.blue);
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
        for (int i = 0; i < points.Length-1 ; i++)
        {
            buttonsItem[i] = Instantiate(buttonItem,points[i],Quaternion.identity,transform);
            var item = _inventory.GetItems()[i];
            buttonsItem[i].GetComponent<UIInventoryItem>().GenerateButton(item, _inventory.GetItemQuantity(item));
            
            //Debug.DrawLine(points[i], points[i + 1], color);
        }
        Debug.DrawLine(points[^1], points[0], color);
        // Draw vertices
        for (int i = 0; i < points.Length; i++)
        {
            Debug.DrawLine(points[i], points[(i + density) % points.Length], color);
        }
    }
}
