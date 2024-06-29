using UnityEngine;

public class NadeExplode : MonoBehaviour
{
    public GameObject explosionPrefab; // The explosion prefab to instantiate
    public float destroyDelay = 0.1f; // Delay before the explosion effect is destroyed

    public void Explode(Vector3 position, Quaternion rotation)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, rotation);
            Destroy(explosion, destroyDelay); // Destroy the explosion effect after a short delay
        }
    }
}
