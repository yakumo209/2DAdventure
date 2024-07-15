using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float cantbehurtTime;
    private float cantbehurtCounter;
    public bool cantbehurt;
    public UnityEvent<Transform> onTakeDamage;
    public UnityEvent onDead;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(Attack attacker)
    {
        if (cantbehurt)
        {
            return;
        }

        if (currentHealth-attacker.damage>0)
        {
            currentHealth -= attacker.damage;
            TriggerCantbehurt();
            onTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            onDead?.Invoke();
        }
        
    }

    private void TriggerCantbehurt()
    {
        if (!cantbehurt)
        {
            cantbehurt = true;
            cantbehurtCounter = cantbehurtTime;
        }
    }

    private void Update()
    {
        cantbehurtCounter -= Time.deltaTime;
        if (cantbehurtCounter<=0)
        {
            cantbehurt = false;
        }
    }
}
