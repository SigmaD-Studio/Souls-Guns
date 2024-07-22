using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public float range = 20f; // Maximum distance the bullet can travel

    private float distanceTraveled = 0f; // Distance the bullet has traveled

    void Update()
    {
        // Move the bullet forward based on its speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Track the distance the bullet has traveled
        distanceTraveled += speed * Time.deltaTime;

        // Check if the bullet has reached its maximum range
        if (distanceTraveled >= range)
        {
            DestroyBullet();
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        DamagedHandle player = other.GetComponent<DamagedHandle>();
        if (player != null)
        {
            DestroyBullet();
        }
        else
        {
            // Destroy the bullet if it hits any other object
            DestroyBullet();
        }
    }*/

    void DestroyBullet()
    {
        // Clean up the bullet GameObject
        Destroy(gameObject);
    }
}
