using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VectorTwoEventListener : MonoBehaviour
{
    [Tooltip("The event channel scritpable object")]
    public VectorTwoEventChannel channel;
    [Tooltip("Callback to respond to the unity event")]
    public UnityEvent<Vector2> response;

    private void OnEnable()
    {
        channel.RegisterListener(this);
    }

    private void OnDisable()
    {
        channel.UnregisterListener(this);
    }

    public void OnEventRaised(Vector2 value)
    {
        response.Invoke(value);
    }
}
