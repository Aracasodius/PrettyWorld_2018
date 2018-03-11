using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemPickup))]
public class ItemPickupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemPickup itemPickup = (ItemPickup)target;
        DrawDefaultInspector();

        if(GUILayout.Button("Force Pickup"))
        {
            itemPickup.PickUp();
        }
    }

}
