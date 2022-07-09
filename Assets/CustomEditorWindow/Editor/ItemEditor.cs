using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ItemEditor : EditorWindow
{
    // itemID is the ID of that item
    public int itemID = -1;

    // itemDisplayName is the display name of that item on the screen
    public string itemDisplayName = "";

    // itemIcon is the 2D item icon appears in the inventory or amour slots
    public Sprite itemIcon = null;

    // item3DModelPrefeb is the 3D Prefeb asset that will show up on the screen outside of the inventory
    public Transform item3DModelPrefeb = null;

    // maxStackNumber is the maximum number of items that a stack can hold
    public int maxStackNumber = 99;

    // A list of item effects for that consumable
    public List<Modifier> modifier = new List<Modifier>();

    //A pre-existing item to have its stats loaded 
    public ScriptableObject ItemToLoad;
    private Consumable consumable;
    private Item item;

    //Variuable declaration
    int _selected = 0;
    string[] _options = new string[2] { "Item", "Consumable" };
    public Modifier mod1;
    public Modifier mod2;
    public Modifier mod3;
    public Modifier mod4;
    public Modifier mod5;

    [MenuItem("Tools/ItemEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ItemEditor));      //GetWindow is a method inherited from the EditorWindow class
    }

    //Sets the interface UI 
    private void OnGUI()
    {
        GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

        itemID = EditorGUILayout.IntField("Item ID", itemID);
        itemDisplayName = EditorGUILayout.TextField("Item Display Name", itemDisplayName);
        itemIcon = (Sprite)EditorGUILayout.ObjectField("Item Icon", itemIcon, typeof(Sprite), allowSceneObjects: true);
        item3DModelPrefeb = (Transform)EditorGUILayout.ObjectField("Item 3D Model Prefab", item3DModelPrefeb, typeof(Transform), false);
        maxStackNumber = EditorGUILayout.IntField("Max Stack Number", maxStackNumber);

        ItemToLoad = (ScriptableObject)EditorGUILayout.ObjectField("Item/Consumable To Load", ItemToLoad, typeof(ScriptableObject), allowSceneObjects: true);

        this._selected = EditorGUILayout.Popup("Pick What To Load/Create", _selected, _options);

        if (EditorGUI.EndChangeCheck())
        {

        }

        mod1 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 1", mod1, typeof(Modifier), allowSceneObjects: true);
        mod2 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 2", mod2, typeof(Modifier), allowSceneObjects: true);
        mod3 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 3", mod3, typeof(Modifier), allowSceneObjects: true);
        mod4 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 4", mod4, typeof(Modifier), allowSceneObjects: true);
        mod5 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 5", mod5, typeof(Modifier), allowSceneObjects: true);

        EditorGUI.BeginChangeCheck();

        //If the load button is pressed, loads in the currently selected item or consumables stats and displays them 
        //on the screen. 
        if (GUILayout.Button("Load Item/Consumable Stats"))
        {
            Debug.Log("Load Clicked");

            if (_options[_selected].ToString() == "Consumable")
            {
                consumable = (Consumable)ItemToLoad;
                itemID = consumable.itemID;
                itemDisplayName = consumable.itemDisplayName;
                itemIcon = consumable.itemIcon;
                item3DModelPrefeb = consumable.item3DModelPrefeb;
                maxStackNumber = consumable.maxStackNumber;
                modifier = consumable.modifier;
                int numOfModifiers = modifier.Count;

                if (numOfModifiers == 1)
                {
                    mod1 = modifier[0];
                }
                else if (numOfModifiers == 2)
                {
                    mod1 = modifier[0];
                    mod2 = modifier[1];
                }
                else if (numOfModifiers == 3)
                {
                    mod1 = modifier[0];
                    mod2 = modifier[1];
                    mod3 = modifier[2];
                }
                else if (numOfModifiers == 4)
                {
                    mod1 = modifier[0];
                    mod2 = modifier[1];
                    mod3 = modifier[2];
                    mod4 = modifier[3];
                }
                else if (numOfModifiers == 5)
                {
                    mod1 = modifier[0];
                    mod2 = modifier[1];
                    mod3 = modifier[2];
                    mod4 = modifier[3];
                    mod5 = modifier[4];
                }
            }
            else if (_options[_selected].ToString() == "Item")
            {
                item = (Item)ItemToLoad;
                itemID = item.itemID;
                itemDisplayName = item.itemDisplayName;
                itemIcon = item.itemIcon;
                item3DModelPrefeb = item.item3DModelPrefeb;
                maxStackNumber = item.maxStackNumber;
            }
        }

        //If the save button is pressed, get the stats of the inputted feilds,
        //and save them to the currently selected item/consumable.
        if (GUILayout.Button("Save Item/Consumable Stats"))
        {
            Debug.Log("Save Clicked");

            if (_options[_selected].ToString() == "Item")
            {
                Undo.RecordObject(ItemToLoad, "Setting Value");
                (ItemToLoad as Item).itemID = itemID;
                (ItemToLoad as Item).itemDisplayName = itemDisplayName;
                (ItemToLoad as Item).itemIcon = itemIcon;
                (ItemToLoad as Item).item3DModelPrefeb = item3DModelPrefeb;
                (ItemToLoad as Item).maxStackNumber = maxStackNumber;
            }
            else if (_options[_selected].ToString() == "Consumable")
            {
                Undo.RecordObject(ItemToLoad, "Setting Value");
                (ItemToLoad as Consumable).itemID = itemID;
                (ItemToLoad as Consumable).itemDisplayName = itemDisplayName;
                (ItemToLoad as Consumable).itemIcon = itemIcon;
                (ItemToLoad as Consumable).item3DModelPrefeb = item3DModelPrefeb;
                (ItemToLoad as Consumable).maxStackNumber = maxStackNumber;

                List<Modifier> newModifierList = new List<Modifier>();

                if(mod1 != null)
                {
                    newModifierList.Add(mod1);
                }

                if (mod2 != null)
                {
                    newModifierList.Add(mod2);
                }

                if (mod3 != null)
                {
                    newModifierList.Add(mod3);
                }

                if (mod4 != null)
                {
                    newModifierList.Add(mod4);
                }

                if (mod5 != null)
                {
                    newModifierList.Add(mod5);
                }

                (ItemToLoad as Consumable).modifier = newModifierList;
            }
        }

        //Creates a blank item/consumable at the scirptable objects folder in the game folder. With a name 
        //created by the inputted feild
        if (GUILayout.Button("Create Item"))
        {
            Debug.Log("Create Clicked");

            if (_options[_selected].ToString() == "Item")
            {
                item = ScriptableObject.CreateInstance<Item>();

                AssetDatabase.CreateAsset(item, "Assets/Game/Scriptable Objects/" + itemDisplayName + ".asset");
                AssetDatabase.SaveAssets();
            }
            else if (_options[_selected].ToString() == "Consumable")
            {
                consumable = ScriptableObject.CreateInstance<Consumable>();

                AssetDatabase.CreateAsset(consumable, "Assets/Game/Scriptable Objects/" + itemDisplayName + ".asset");
                AssetDatabase.SaveAssets();
            }

        }
    }
}
