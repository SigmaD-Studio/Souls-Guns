using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab for explosion effect
    public float explosionRadius = 5f; // Radius of explosion for damage
    public float explosionDamage = 100f; // Damage dealt by the explosion
    public float explosionDuration = 0.1f; // Duration of the explosion effect

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, explosionDuration); // Destroy the explosion effect after a short delay
        }

        // Apply damage to nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider's GameObject has an Enemy component
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Calculate damage based on distance
                Vector2 direction = collider.transform.position - transform.position;
                float distance = direction.magnitude;
                float falloff = 1 - Mathf.Clamp01(distance / explosionRadius);
                float damage = explosionDamage * falloff;

                // Apply damage to the enemy
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject); // Destroy the rocket object after explosion
    }
}
