using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "VectorTwoEventChannel", menuName = "Event/Vector Two Event Channel")]
public class VectorTwoEventChannel : ScriptableObject
{
    private List<VectorTwoEventListener> listeners = new List<VectorTwoEventListener>();
    public void Raise(Vector2 value)
    {
        //Debug.Log(listeners.Count);
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(VectorTwoEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(VectorTwoEventListener listener)
    {
        listeners.Remove(listener);
    }
}
