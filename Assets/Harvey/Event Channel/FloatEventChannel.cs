using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Event/Float Event Channel")]
public class FloatEventChannel : ScriptableObject
{
    private List<FloatEventListener> listeners = new List<FloatEventListener>();

    public void Raise(float floatNumber)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(floatNumber);
        }
    }

    public void RegisterListener(FloatEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(FloatEventListener listener)
    {
        listeners.Remove(listener);
    }
}
