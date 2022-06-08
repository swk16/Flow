using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;

    [SerializeField] protected float currentHealth;
    public virtual void TakeDamage()
    {
        currentHealth -= 10f;
        if (currentHealth <= 0f)
        {
            Die();
        }                           

    }
    public virtual void Die()
    {
        currentHealth = 0f;
        gameObject.SetActive(false);

    }

}
