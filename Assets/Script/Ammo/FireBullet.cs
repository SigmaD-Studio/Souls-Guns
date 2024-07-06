using UnityEngine;
using System.Collections;
public class FlamethrowerBullet : MonoBehaviour
{
    public float collisionDamage = 25f; // Damage inflicted on collision
    public float burnDamagePerSecond = 10f; // Damage per second for burning effect
    public float burnDuration = 3f; // Duration of the burning effect

    private bool hasCollided = false; // Flag to track if bullet has collided

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasCollided)
        {
            // Check if the bullet collided with an enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Deal collision damage
                enemy.TakeDamage(collisionDamage);

                // Start the burn effect
                StartCoroutine(BurnEnemy(enemy));
            }

            // Set the collided flag to true to prevent multiple collisions
            hasCollided = true;

            // Destroy the bullet after collision
            Destroy(gameObject);
        }
    }

    IEnumerator BurnEnemy(Enemy enemy)
    {
        float timer = 0f;
        while (timer < burnDuration)
        {
            if (enemy == null)
                yield break; // Exit coroutine if enemy is destroyed

            // Apply burn damage per second
            enemy.TakeDamage(burnDamagePerSecond * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
