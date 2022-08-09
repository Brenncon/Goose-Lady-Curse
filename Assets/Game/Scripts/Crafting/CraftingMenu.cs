using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingMenu : Menu
{
    [SerializeField] CraftingMenuHotKeyHandler craftingMenuHotKeyHandler;

    [SerializeField] GameObject craftingUI;

    // temp
    public UnityEvent<string> displayRecipes;

    public string path = "";
    // temp

    public void MenuToggle()
    {
        // If the menu is currently deactivated
        if (craftingUI.activeSelf == false)
        {
            // Activate the menu
            craftingMenuHotKeyHandler.OpenInventory();
        }
        // If the menu is currently activated
        else
        {
            // Deactivate the menu
            craftingMenuHotKeyHandler.CloseInventory();
        }
    }
}
