using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float speed = 30f; // Speed of the bullet
    public float range = 100f; // Maximum distance the bullet can travel
    public float damage = 50f; // Damage inflicted by the bullet
    public int maxPiercingCount = 2; // Maximum number of enemies the bullet can pierce through

    private float distanceTraveled = 0f; // Distance the bullet has traveled
    private int piercingCount = 0; // Number of enemies pierced

    void Update()
    {
        MoveBullet();

        // Check if the bullet has reached its maximum range
        if (distanceTraveled >= range)
        {
            DestroyBullet();
        }
    }

    void MoveBullet()
    {
        // Move the bullet forward based on its speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Track the distance the bullet has traveled
        distanceTraveled += speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Deal damage to the enemy
            enemy.TakeDamage(damage);

            // Increment the piercing count
            piercingCount++;

            // Destroy the bullet if it has pierced the maximum number of enemies
            if (piercingCount > maxPiercingCount)
            {
                DestroyBullet();
            }
        }
        else
        {
            // Destroy the bullet if it hits any other object
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        // Clean up the bullet GameObject
        Destroy(gameObject);
    }
}
