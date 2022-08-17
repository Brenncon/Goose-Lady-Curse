using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Equipment Slot", menuName = "Character/Equipment Slot")]
public class EquipmentSlot : ScriptableObject
{
    [ReadOnly]public Equipment equipment = null;
    [ReadOnly]public bool isOccupied = false;
    public UnityEvent OnEquip;
    public UnityEvent<Item,int> OnUnequip;

    //void Awake()
    //{
    //    // If the equipment slot has an equipment at start for some reason, safety measure
    //    if (equipment != null)
    //    {
    //        // Set up the equipment slot as if the player equips the equipment
    //        // Set isOccupied to true
    //        isOccupied = true;

    //        //Apply item bounus
    //        for (int i = 0; i < equipment.modifiers.Length; i++)
    //        {
    //            equipment.modifiers[i].Apply();
    //        }
    //    }
    //}

    private void OnEnable()
    {
        equipment = null;
    }

    public void Equip(Equipment equipment)
    {
        if (isOccupied)
        {
            UnEquip();
        }
        isOccupied = true;
        this.equipment = equipment;
        OnEquip.Invoke();
        //Apply item bounus
        for (int i = 0; i < equipment.modifiers.Length; i++)
        {
            equipment.modifiers[i].Apply();
        }
    }

    public void UnEquip()
    {
        isOccupied = false;
        //Remove item bonus
        for (int i = 0; i < equipment.modifiers.Length; i++)
        {
            equipment.modifiers[i].Remove();
        }
        OnUnequip.Invoke(equipment, 1);
        equipment = null;
    }
}
