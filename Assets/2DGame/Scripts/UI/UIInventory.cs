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
        // radius = ((RectTransform)transform).rect.width * 0.5f;
        // resolution = _inventory.GetItems().Length;
        // foreach (var button in buttonsItem)
        // {
        //     DestroyImmediate(button);
        // }
        // buttonsItem = new GameObject[resolution];
        //
        // Draw(Color.blue);
    }

    private void Update()
    {
        radius = ((RectTransform)transform).rect.width * 0.5f;
        resolution = _inventory.SlotsAmount;
        
        if (buttonsItem.Length != resolution)
        {
             foreach (var button in buttonsItem)
             {
                 Destroy(button);
             }
             buttonsItem = new GameObject[resolution];
        }
            
        
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
        // Determine where buttons can be placed in the circular inventory
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
        for (int i = 0; i < resolution; i++)
        {
            if(buttonsItem[i] == null)
                buttonsItem[i] = Instantiate(buttonItem,points[i],Quaternion.identity,transform);
            // Attribute good position 
            buttonsItem[i].transform.position = points[i];
            // Get item and setup the UI Item Button in inventory
            var slotInventory = _inventory.GetItems()[i];
            if(slotInventory.item != null)
                buttonsItem[i].GetComponent<UIInventoryItem>().GenerateButton(slotInventory.item, slotInventory.amount);
            
        }
        
    }
}
