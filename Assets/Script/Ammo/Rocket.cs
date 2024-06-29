using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosionPrefab; // The explosion prefab to instantiate on impact

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 0.1f); // Destroy the explosion after 1 second
        }
        else
        {
            Debug.LogWarning("ExplosionPrefab is not assigned.");
        }
        Destroy(gameObject); // Destroy the rocket
    }
}