using System;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(InventoryScriptableObject))]
public class InventoryScriptableObjectEditor : Editor
{
    private InventoryScriptableObject myTarget;
    private ItemScriptableObject[] listOfItems;
    private ReorderableList lists;

    private void OnEnable()
    {
        myTarget = (InventoryScriptableObject)target;
        listOfItems = myTarget.GetItems();
        {
            lists = new ReorderableList(listOfItems, typeof(ItemScriptableObject),false,true,true,false);
            lists.drawElementCallback = DrawElementItem; // Delegate to draw the elements on the list
            lists.drawHeaderCallback = DrawHeader; // Skip this line if you set displayHeader to 'false' in your ReorderableList constructor.
            lists.onAddCallback = OnAddCallback;
            //lists.onChangedCallback = reorderableList =>
        }
    }

    private void OnAddCallback(ReorderableList list)
    {
        myTarget.AddItem(Resources.Load<ItemScriptableObject>("Item_default"));
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        lists.DoLayoutList();
       // if (listOfItems != null)
        
    }
    
    private void DrawElementItem(Rect rect, int index, bool isactive, bool isfocused)
    {
        // GameObject name field
        Rect textFieldRect = new Rect(rect.x+32 , rect.y, 150, EditorGUIUtility.singleLineHeight);
        var item = listOfItems[index];
        EditorGUI.ObjectField(textFieldRect,item.name + $"[{myTarget.GetItemQuantity(item)}]", item, typeof(ItemScriptableObject), false);
    }

    private void DrawHeader(Rect rect)
    {
        EditorGUI.TextField(rect, "yo");
        // throw new System.NotImplementedException();
    }
}
