using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 200;
    private int currentHealth;

    private EnemyBehaviour disableBehaviour;
    private NavMeshAgent disableAgent;
    private Animator anim;

    bool isDead = false;

    private void Start()
    {
        disableAgent = GetComponent<NavMeshAgent>();
        disableBehaviour = GetComponent<EnemyBehaviour>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        disableAgent.enabled = false;
        disableBehaviour.enabled = false;
        anim.SetBool("isDead", true);
        
        EnemyCounter counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<EnemyCounter>();
        counter.CountAfterDeath();
        Destroy(gameObject, 5f);
    }
}
