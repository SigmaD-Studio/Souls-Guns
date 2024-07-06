using UnityEngine;

public class ShotgunAmmo : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public float range = 10f; // Maximum distance the bullet can travel
    public float damage = 25f; // Damage inflicted by the bullet

    private float distanceTraveled = 0f; // Distance the bullet has traveled

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

            // Destroy the bullet upon hitting the enemy
            DestroyBullet();
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
