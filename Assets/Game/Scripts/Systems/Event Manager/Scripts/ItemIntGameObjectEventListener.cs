using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemIntGameObjectEventListener : MonoBehaviour
{
    [Tooltip("The event channel scritpable object")]
    public ItemIntGameObjectEventChannel channel;
    [Tooltip("Callback to respond to the unity event")]
    public UnityEvent<Item, int, GameObject> response;

    private void OnEnable()
    {
        channel.RegisterListener(this);
    }

    private void OnDisable()
    {
        channel.UnregisterListener(this);
    }

    public void OnEventRaised(Item item, int interger, GameObject gameObject)
    {
        response.Invoke(item, interger, gameObject);
    }
}
