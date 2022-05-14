using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scriptable object stores the data of an item stack
/// </summary>

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    // itemID is the ID of that item
    public int itemID = -1;

    // itemDisplayName is the display name of that item on the screen
    public string itemDisplayName = "";

    // itemIcon is the 2D item icon appears in the inventory or amour slots
    public Sprite itemIcon = null;

    // item3DModelPrefeb is the 3D Prefeb asset that will show up on the screen outside of the inventory
    public Transform item3DModelPrefeb = null;

    // stackNumber is the number of item that is currently in the stack
    public int stackNumber = -1;

    // maxStackNumber is the maximum number of item that a stack can hold
    [SerializeField] protected int maxStackNumber = 99;
}
