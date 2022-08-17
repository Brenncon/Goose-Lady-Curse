using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : Interactable
{
    public Item item;
    public float floatAmplitude = 0.5f;
    public int stackNumber = 1;
    private float y;

    void Start()
    {
        y = Random.Range(-1f, 1f);
    }
    void Update()
    {
        if (y >= 1)
        {
            y = -1;
        }
        y += Time.deltaTime;
        //transform.Rotate(0, 15f * Time.deltaTime, 0);
        transform.Translate(floatAmplitude*Vector3.up * Mathf.Sin(y * Mathf.PI) * Time.deltaTime);
    }

    private void Awake()
    {
        gameObject.tag = "Item";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
