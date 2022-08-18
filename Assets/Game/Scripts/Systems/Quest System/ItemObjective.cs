using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;
using Project.Build.Commands;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemObjective", menuName = "Quest/ItemObjective")]
public class ItemObjective : Objective
{ 
    public Item objective;
    public int targetCount;
    [SerializeField, ReadOnly]
    private float currentCount;

    public override void Initialize()
    {
        base.Initialize();
        currentCount = 0;
    }
    public void PickupObjective(Item item, int stack)
    {
        if (objective != null)
        {
            if (objective.name == item.name)
            {
                currentCount += stack;
            }

            if (currentCount >= targetCount&&!completed)
            {
                OnCompletion(true); ;
            }
        }
    }

    public void DropoffObjective(Item item, int stack)
    {
        if (objective != null)
        {
            if (objective.name == item.name)
            {
                currentCount -= stack;
            }
            if (currentCount < targetCount&&completed)
            {
                OnCompletion(false);
            }
        }
    }
}
