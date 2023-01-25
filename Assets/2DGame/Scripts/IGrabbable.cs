using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    /// <summary>
    /// Behavior when the grabbable object is grabbed
    /// </summary>
    /// <param name="inventory"></param>
    public void OnGrab(InventoryScriptableObject inventory);
}
