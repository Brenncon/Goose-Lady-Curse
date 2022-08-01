using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDestoryEvent : MonoBehaviour
{
    public UnityEvent ObjectDestoryed;
    private void OnDestroy()
    {
        ObjectDestoryed.Invoke();
    }
}
