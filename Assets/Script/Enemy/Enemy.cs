using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the enemy
    [SerializeField] private float currentHealth; // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        gameObject.tag = "Enemy"; // Set the tag of the enemy
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce health by damage amount

        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if health is 0 or less
        }
    }

    void Die()
    {
        // Add death effects, animations, etc. here

        Destroy(gameObject); // Destroy the enemy game object
    }

    void OnDrawGizmosSelected()
    {
        // If you want to visualize the enemy's health in the Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
