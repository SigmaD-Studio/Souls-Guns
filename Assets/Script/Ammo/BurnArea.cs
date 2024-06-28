using UnityEngine;

public class BurnArea : MonoBehaviour
{
    public GameObject explosionPrefab; // The explosion prefab to instantiate on impact
    public GameObject firePrefab; // The fire prefab to instantiate on impact
    public float fireDuration; // Duration of the fire
    public float explosionDuration = 1.0f; // Duration of the explosion effect

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);

        GameObject fire = Instantiate(firePrefab, transform.position, transform.rotation);
        Destroy(fire, fireDuration);

        Destroy(gameObject); // Destroy the Molotov
    }
}
