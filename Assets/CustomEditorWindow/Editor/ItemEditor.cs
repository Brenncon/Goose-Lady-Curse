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
    public Modifier[] modifiers;

    //A pre-existing item to have its stats loaded 
    public ScriptableObject ItemToLoad;
    private Consumable consumable;
    private Item item;
    public Equipment equipment;
    public Container container;

    //Variuable declaration
    int _selected = 0;
    string[] _options = new string[4] { "Item", "Consumable", "Equipment", "Container" };
    public Modifier mod1;
    public Modifier mod2;
    public Modifier mod3;
    public Modifier mod4;
    public Modifier mod5;
    public string nameOfObjectToCreate;
    public string pathToCreateNewObject;
    public EquipmentSlot equipmentSlot;

    // volume is the number of items that the container can hold;
    public int volume = 1;

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

        ItemToLoad = (ScriptableObject)EditorGUILayout.ObjectField("Item To Load", ItemToLoad, typeof(ScriptableObject), allowSceneObjects: true);

        this._selected = EditorGUILayout.Popup("Pick What To Load/Create", _selected, _options);

        if (EditorGUI.EndChangeCheck())
        {

        }

        mod1 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 1", mod1, typeof(Modifier), allowSceneObjects: true);
        mod2 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 2", mod2, typeof(Modifier), allowSceneObjects: true);
        mod3 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 3", mod3, typeof(Modifier), allowSceneObjects: true);
        mod4 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 4", mod4, typeof(Modifier), allowSceneObjects: true);
        mod5 = (Modifier)EditorGUILayout.ObjectField("Consumable Effect 5", mod5, typeof(Modifier), allowSceneObjects: true);

        equipmentSlot = (EquipmentSlot)EditorGUILayout.ObjectField("Equipment Slot", equipmentSlot, typeof(EquipmentSlot), allowSceneObjects: true);

        //slot1 = (ItemSlot)EditorGUILayout.ObjectField("Item Slot 1", slot1, typeof(ItemSlot), allowSceneObjects: true);

        volume = EditorGUILayout.IntField("Volume", volume);

        nameOfObjectToCreate = EditorGUILayout.TextField("New Scriptableobject Name", nameOfObjectToCreate);
        pathToCreateNewObject = EditorGUILayout.TextField("Path To Folder (Optional)", pathToCreateNewObject);

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
            else if (_options[_selected].ToString() == "Equipment")
            {
                equipment = (Equipment)ItemToLoad;
                itemID = equipment.itemID;
                itemDisplayName = equipment.itemDisplayName;
                itemIcon = equipment.itemIcon;
                item3DModelPrefeb = equipment.item3DModelPrefeb;
                maxStackNumber = equipment.maxStackNumber;
                equipmentSlot = equipment.equipmentSlot;
                modifiers = equipment.modifiers;
                int numOfModifiers = modifiers.Length;

                if (numOfModifiers == 1)
                {
                    mod1 = modifiers[0];
                }
                else if (numOfModifiers == 2)
                {
                    mod1 = modifiers[0];
                    mod2 = modifiers[1];
                }
                else if (numOfModifiers == 3)
                {
                    mod1 = modifiers[0];
                    mod2 = modifiers[1];
                    mod3 = modifiers[2];
                }
                else if (numOfModifiers == 4)
                {
                    mod1 = modifiers[0];
                    mod2 = modifiers[1];
                    mod3 = modifiers[2];
                    mod4 = modifiers[3];
                }
                else if (numOfModifiers == 5)
                {
                    mod1 = modifiers[0];
                    mod2 = modifiers[1];
                    mod3 = modifiers[2];
                    mod4 = modifiers[3];
                    mod5 = modifiers[4];
                }
            }
            else if (_options[_selected].ToString() == "Container")
            {
                container = (Container)ItemToLoad;
                itemID = container.itemID;
                itemDisplayName = container.itemDisplayName;
                itemIcon = container.itemIcon;
                item3DModelPrefeb = container.item3DModelPrefeb;
                maxStackNumber = container.maxStackNumber;
                volume = container.volume;
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

                if (mod1 != null)
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
            else if (_options[_selected].ToString() == "Equipment")
            {
                Undo.RecordObject(ItemToLoad, "Setting Value");
                (ItemToLoad as Equipment).itemID = itemID;
                (ItemToLoad as Equipment).itemDisplayName = itemDisplayName;
                (ItemToLoad as Equipment).itemIcon = itemIcon;
                (ItemToLoad as Equipment).item3DModelPrefeb = item3DModelPrefeb;
                (ItemToLoad as Equipment).maxStackNumber = maxStackNumber;
                (ItemToLoad as Equipment).equipmentSlot = equipmentSlot;

                Modifier[] newModifiersArray = new Modifier[4];

                if (mod1 != null)
                {
                    newModifiersArray[0] = mod1;
                }

                if (mod2 != null)
                {
                    newModifiersArray[1] = mod2;
                }

                if (mod3 != null)
                {
                    newModifiersArray[2] = mod3;
                }

                if (mod4 != null)
                {
                    newModifiersArray[3] = mod4;
                }

                if (mod5 != null)
                {
                    newModifiersArray[4] = mod5;
                }

                (ItemToLoad as Equipment).modifiers = newModifiersArray;
            }

            else if (_options[_selected].ToString() == "Container")
            {
                Undo.RecordObject(ItemToLoad, "Setting Value");
                (ItemToLoad as Container).itemID = itemID;
                (ItemToLoad as Container).itemDisplayName = itemDisplayName;
                (ItemToLoad as Container).itemIcon = itemIcon;
                (ItemToLoad as Container).item3DModelPrefeb = item3DModelPrefeb;
                (ItemToLoad as Container).maxStackNumber = maxStackNumber;
                (ItemToLoad as Container).volume = volume;
            }
        }

        //Creates a blank item/consumable at the scirptable objects folder in the game folder. With a name 
        //created by the inputted feild
        if (GUILayout.Button("Create Item"))
        {
            Debug.Log("Create Clicked");

            if (_options[_selected].ToString() == "Item")
            {
                if (pathToCreateNewObject != null || pathToCreateNewObject == "" || pathToCreateNewObject == " ")
                {
                    item = ScriptableObject.CreateInstance<Item>();

                    AssetDatabase.CreateAsset(item, pathToCreateNewObject + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
                else
                { 
                    item = ScriptableObject.CreateInstance<Item>();

                    AssetDatabase.CreateAsset(item, "Assets/Game/Scriptable Objects/" + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
            }
            else if (_options[_selected].ToString() == "Consumable")
            {
                if (pathToCreateNewObject != null || pathToCreateNewObject == "" || pathToCreateNewObject == " ")
                {
                    consumable = ScriptableObject.CreateInstance<Consumable>();

                    AssetDatabase.CreateAsset(consumable, pathToCreateNewObject + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    consumable = ScriptableObject.CreateInstance<Consumable>();

                    AssetDatabase.CreateAsset(consumable, "Assets/Game/Scriptable Objects/" + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
            }
            else if (_options[_selected].ToString() == "Equipment")
            {
                if (pathToCreateNewObject != null || pathToCreateNewObject == "" || pathToCreateNewObject == " ")
                {
                    equipment = ScriptableObject.CreateInstance<Equipment>();

                    AssetDatabase.CreateAsset(equipment, pathToCreateNewObject + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    equipment = ScriptableObject.CreateInstance<Equipment>();

                    AssetDatabase.CreateAsset(equipment, "Assets/Game/Scriptable Objects/" + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
            }
            else if (_options[_selected].ToString() == "Container")
            {
                if (pathToCreateNewObject != null || pathToCreateNewObject == "" || pathToCreateNewObject == " ")
                {
                    container = ScriptableObject.CreateInstance<Container>();

                    AssetDatabase.CreateAsset(container, pathToCreateNewObject + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    container = ScriptableObject.CreateInstance<Container>();

                    AssetDatabase.CreateAsset(container, "Assets/Game/Scriptable Objects/" + nameOfObjectToCreate + ".asset");
                    AssetDatabase.SaveAssets();
                }
            }

        }
    }
}
