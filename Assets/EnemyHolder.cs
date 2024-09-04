using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    public float health = 100f; // Starting health

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death (e.g., play death animation, remove from game)
        Destroy(gameObject); // Simple way to remove the enemy
    }
}
