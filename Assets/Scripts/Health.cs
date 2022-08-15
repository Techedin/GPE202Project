using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, Pawn dealer)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            Die(dealer);
        }
        Debug.Log(dealer.name + " did " + damage + " damage to " + gameObject.name);
    }

    public void Heal(float healingAmount)
    {
        currentHealth = currentHealth + healingAmount;
    }

   
    public void Die(Pawn pawn)
    {
        Destroy(gameObject);
    }

}
