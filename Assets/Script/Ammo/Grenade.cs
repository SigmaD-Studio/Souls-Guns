using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    public Transform firePoint; // The point where the Grenade is thrown from
    public GameObject grenadePrefab; // The Grenade prefab to instantiate
    public GameObject explosionPrefab; // The explosion prefab to instantiate on impact
    public float throwSpeed = 10f; // Speed of the Grenade when thrown
    public float fuseTime = 3f; // Time until the grenade explodes after throwing
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionDamage = 250f; // Damage dealt by the explosion
    public int maxAmmo = 3; // Maximum number of grenades

    private int currentAmmo; // Current ammo count
    private bool isThrowing = false; // Flag to prevent multiple throws

    private Coroutine explodeCoroutine; // Reference to the explode coroutine

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isThrowing)
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        if (grenadePrefab != null && currentAmmo > 0)
        {
            isThrowing = true; // Prevent multiple throws

            currentAmmo--;

            GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();

            Vector2 throwDirection = firePoint.right * throwSpeed;
            rb.velocity = throwDirection;

            // Start the explode coroutine for this grenade
            explodeCoroutine = StartCoroutine(ExplodeAfterTime(grenade));
        }
        else
        {
            Debug.LogError("Cannot throw grenade: out of ammo or grenadePrefab is null.");
        }
    }

    IEnumerator ExplodeAfterTime(GameObject grenade)
    {
        yield return new WaitForSeconds(fuseTime);

        Explode(grenade);
    }

    void Explode(GameObject grenade)
    {
        // Check if the grenade is still active (not destroyed prematurely)
        if (grenade == null)
            return;

        // Instantiate the explosion effect
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, grenade.transform.position, Quaternion.identity);
            Destroy(explosion, 0.1f); // Destroy the explosion effect after a short delay
        }

        // Apply damage to nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(grenade.transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider's GameObject has an Enemy component
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }
        }

        // Destroy the grenade object
        Destroy(grenade);

        isThrowing = false; // Allow throwing again after explosion

        if (currentAmmo <= 0)
        {
            Destroy(gameObject); // Destroy the Grenade script's game object when out of ammo
        }
    }

    // Optional: Draw the explosion radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
