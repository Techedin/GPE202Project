using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public Pawn ownerPawn;
    public float killPoints;
    public float currentHealth;
    public float maxHealth;
    public Image healthMeter;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        killPoints = 15;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage, Pawn dealer)
    {
       healthMeter = gameObject.GetComponentInChildren<Image>();
        currentHealth = currentHealth - damage;
        healthMeter.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die(dealer);
        }
        Debug.Log(dealer.name + " did " + damage + " damage to " + gameObject.name);
    }

    public void Heal(float healingAmount)
    {
        healthMeter = gameObject.GetComponentInChildren<Image>();
        healthMeter.fillAmount = currentHealth / maxHealth;
        currentHealth = currentHealth + healingAmount;
    }


    public void Die(Pawn pawn)
    {
        pawn.controller.AddPoints((int)killPoints);
        ownerPawn.controller.OnDied();
        Destroy(gameObject);
    }

}
