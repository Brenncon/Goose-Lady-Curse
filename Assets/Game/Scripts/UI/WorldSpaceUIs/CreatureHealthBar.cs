using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class CreatureHealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Update()
    {
        transform.LookAt(transform.position+Camera.main.transform.rotation*Vector3.forward,Camera.main.transform.rotation*Vector3.up);
    }

    public void SetValue(float value)
    {
        healthBar.value = value;
    }

    public void SetMaxValue(float value)
    {
        healthBar.maxValue = value;
    }
}
