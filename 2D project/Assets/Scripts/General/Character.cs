using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
 [Header("基本属性")]
    public float maxHealth;
    
    public float currentHealth;

    [Header("受伤无敌")]
    public float invulnerableDuration;

    private float invulnerableCounter;

    public bool invulnerable;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if(invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if(invulnerable)
            return;

        //Debug.Log(attacker.damage);
        if(currentHealth - attacker.damage>0){
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
        }
        else
        {
            currentHealth = 0;
            //触发死亡
        }
        
    }

    private void TriggerInvulnerable()
    {
        if(!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
