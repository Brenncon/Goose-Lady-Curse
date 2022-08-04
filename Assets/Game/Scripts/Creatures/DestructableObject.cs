using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestructableObject : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
   
    [SerializeField]
    private float defence;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    public void TakeDamage(float damage)
    {
        
        float actualDamage = 0;
        actualDamage = Mathf.Clamp(damage, 0, damage - defence);
        currentHealth -= actualDamage;
        Debug.Log(gameObject.name + " took "+ actualDamage+" damage ");
    }
}
