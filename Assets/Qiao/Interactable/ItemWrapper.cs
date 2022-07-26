using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : Interactable
{
    public Item item;

    public int stackNumber = 1;

    private void Awake()
    {
        gameObject.tag = "Item";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
