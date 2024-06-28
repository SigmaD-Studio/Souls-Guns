using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour
{
    public Transform firePoint; // The point where rockets are instantiated
    public Transform backFlashPoint; // The point where back flash is instantiated
    public GameObject rocketPrefab; // The rocket prefab to instantiate
    public GameObject explosionPrefab; // The explosion prefab to instantiate on impact
    public GameObject backFlashPrefab; // The back flash prefab to instantiate
    public float rocketSpeed = 20f; // Speed of the rocket
    public float fireRate = 1f; // Time between shots
    public int maxAmmo = 5; // Maximum ammo capacity
    public float reloadTime = 3f; // Time it takes to reload
    public float backFlashDuration = 0.1f; // Duration of the back flash

    private int currentAmmo; // Current ammo count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireRate;
        }

        fireTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        currentAmmo--;

        if (rocketPrefab != null && firePoint != null)
        {
            GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.right * rocketSpeed;
            }

            Rocket rocketScript = rocket.GetComponent<Rocket>();
            if (rocketScript != null)
            {
                rocketScript.explosionPrefab = explosionPrefab; // Pass the explosion prefab to the rocket
            }

            StartCoroutine(ShowBackFlash());
        }
        else
        {
            Debug.LogWarning("RocketPrefab or FirePoint is not assigned.");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    IEnumerator ShowBackFlash()
    {
        if (backFlashPrefab != null && backFlashPoint != null)
        {
            GameObject backFlash = Instantiate(backFlashPrefab, backFlashPoint.position, backFlashPoint.rotation);
            backFlash.SetActive(true);
            yield return new WaitForSeconds(backFlashDuration);
            backFlash.SetActive(false);
            Destroy(backFlash);
        }
        else
        {
            Debug.LogWarning("BackFlashPrefab or BackFlashPoint is not assigned.");
        }
    }
}

