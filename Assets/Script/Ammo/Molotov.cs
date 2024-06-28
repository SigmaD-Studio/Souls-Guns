using UnityEngine;
using System.Collections;

public class Molotov : MonoBehaviour
{
    public Transform firePoint; // The point where the Molotov is thrown from
    public GameObject molotovPrefab; // The Molotov prefab to instantiate
    public GameObject explosionPrefab; // The explosion prefab to instantiate on impact
    public GameObject firePrefab; // The fire prefab to instantiate on impact
    public float throwSpeed = 10f; // Speed of the Molotov when thrown
    public float fireDuration = 5f; // Duration of the fire
    public int maxAmmo = 3; // Maximum number of Molotovs
    public AnimationCurve throwCurve; // Curve defining the trajectory
    public float throwHeight = 5f; // Height of the throw arc
    public float gravity = 9.81f; // Gravity scale (positive for downwards)

    private int currentAmmo; // Current ammo count
    private bool isReloading = false; // Flag to check if reloading

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (currentAmmo <= 0)
        {
            // Handle when ammo is out (optional)
            Debug.Log("Out of Molotovs!");
            Destroy(gameObject); // Destroy the Molotov script's game object when out of ammo
            return;
        }

        if (Input.GetButtonDown("Fire1") && !isReloading)
        {
            ThrowMolotov();
        }
    }

    void ThrowMolotov()
    {
        currentAmmo--;

        if (molotovPrefab != null)
        {
            GameObject molotov = Instantiate(molotovPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = molotov.GetComponent<Rigidbody2D>();

            Vector2 throwDirection = firePoint.right * throwSpeed;
            Vector2 gravityVector = Vector2.down * gravity; // Apply gravity downwards

            rb.velocity = CalculateThrowVelocity(throwDirection, gravityVector);

            BurnArea burnArea = molotov.GetComponent<BurnArea>();
            if (burnArea != null)
            {
                burnArea.explosionPrefab = explosionPrefab;
                burnArea.firePrefab = firePrefab;
                burnArea.fireDuration = fireDuration;
            }
            else
            {
                Debug.LogError("BurnArea component not found on molotovPrefab.");
            }
        }
        else
        {
            Debug.LogError("molotovPrefab is null. Please assign a valid prefab in the Inspector.");
        }
    }

    Vector2 CalculateThrowVelocity(Vector2 initialVelocity, Vector2 gravityVector)
    {
        float timeToTarget = -2 * throwHeight / gravityVector.y;
        Vector2 result = initialVelocity + gravityVector * timeToTarget;

        return result;
    }
}
