using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemIntGameObjectEventChannel", menuName = "Event/Item Int GameObject Event Channel")]
public class ItemIntGameObjectEventChannel : ScriptableObject
{
    private List<ItemIntGameObjectEventListener> listeners = new List<ItemIntGameObjectEventListener>();
    public void Raise(Item item, int interger, GameObject gameObject)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(item, interger, gameObject);
        }
    }

    public void RegisterListener(ItemIntGameObjectEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(ItemIntGameObjectEventListener listener)
    {
        listeners.Remove(listener);
    }
}
