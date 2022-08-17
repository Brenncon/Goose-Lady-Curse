using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Temp script for showing recipe on enable. Wait for further design decision
/// </summary>
public class TempShow : MonoBehaviour
{
    [SerializeField] string path;

    public UnityEvent<string> displayRecipes;

    void Start()
    {
        // Display the only crafting page
        displayRecipes.Invoke(path);
    }
}
