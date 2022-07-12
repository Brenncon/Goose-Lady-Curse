using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CraftButton : MonoBehaviour
{
    public UnityEvent<Item, int> craftEvent, consumeReagentEvent;

    void Start()
    {
        // Initialize
        GetComponent<Button>().interactable = false;
    }

    // Method to craft an item
    // Here assume that the player can only craft if it has enough reagents
    public void CraftItem()
    {
        // Get the current recipe
        CraftingRecipe recipe = CraftingManager.Instance.craftingRecipe;

        // Grab the current stackNumber that the player input
        int stackNumber = int.Parse(CraftingManager.Instance.craftingNumberInputField.text);

        // If there is an item seclected, and the input stack number is greater than 0
        if (recipe != null && stackNumber > 0)
        {
            // For the amount of item that the player want to craft
            for (int i = 0; i < stackNumber; i++)
            {
                // If the inventory is not full or there exist an item stack in the inventory where it is less than the max stack number
                // 1 is tempory here because there doesn't not exist a stack number for the item in the recipe yet
                if (CraftingManager.Instance.inventoryFull == false || CraftingManager.Instance.playerInventory.FindAvailableStack(recipe.item, 1))
                {
                    // Add that 1 of that item to the inventory
                    craftEvent.Invoke(recipe.item, 1);

                    // Consume reagent
                    // For every reagent
                    for (int j = 0; j < recipe.reagents.Length; j++)
                    {
                        // Consume that reagent
                        consumeReagentEvent.Invoke(recipe.reagents[j].item, recipe.reagents[j].requiredAmount);
                    }
                }
                // If the inventory is full
                else
                {
                    // Throw a warning message to the player, debug message for now
                    Debug.Log("Inventory is full, unable to craft");

                    // Do not craft
                    break;
                }
            }

            // Updates the reagents
            CraftingManager.Instance.CheckReagents();
        }
    }
}
