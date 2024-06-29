using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Minigun : MonoBehaviour
{
    public Transform firePoint; // The point where bullets are instantiated
    public Transform muzzleFlashPoint; // The point where muzzle flash appears
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash prefab to instantiate
    public float bulletSpeed = 20f; // Speed of the bullets
    public float fireRate = 0.05f; // Time between shots (high fire rate)
    public float spreadAngle = 5f; // Bullet spread angle
    public int maxAmmo = 100; // Maximum ammo capacity
    public float reloadTime = 4f; // Time it takes to reload
    public int maxAmmoStorage = 500; // Maximum ammo storage
    public TextMeshProUGUI ammoText; // Reference to UI text for displaying ammo count
    public TextMeshProUGUI gunNameText; // Reference to UI text for displaying gun name
    public string gunName = "Minigun"; // Name of the gun
    public Slider reloadSlider; // Reference to UI Slider for reload progress

    private int currentAmmo; // Current ammo count in the gun
    private int ammoStorage; // Ammo storage count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoStorage = maxAmmoStorage;
        UpdateAmmoUI();
        if (reloadSlider != null)
        {
            reloadSlider.maxValue = 1f; // Set the max value of the slider to 1 for normalized progress
            reloadSlider.value = 0f; // Initialize the slider value to 0
            reloadSlider.gameObject.SetActive(false); // Initially hide the slider
        }
        if (gunNameText != null)
        {
            gunNameText.text = gunName; // Set the gun name text
        }
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

        if (Input.GetButton("Fire1") && fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireRate;
        }

        fireTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        currentAmmo--;

        // Instantiate the bullet with spread
        float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
        Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = rotation * Vector2.right * bulletSpeed;

        // Instantiate muzzle flash at the muzzle flash point
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        Destroy(muzzleFlash, 0.05f); // Destroy the muzzle flash after a short delay

        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true); // Show the reload slider
            reloadSlider.value = 0f; // Reset the slider value
        }

        float elapsedTime = 0f;
        while (elapsedTime < reloadTime)
        {
            if (reloadSlider != null)
            {
                reloadSlider.value = elapsedTime / reloadTime; // Update slider value based on elapsed time
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        int ammoNeeded = maxAmmo - currentAmmo;
        if (ammoStorage >= ammoNeeded)
        {
            currentAmmo = maxAmmo;
            ammoStorage -= ammoNeeded;
        }
        else
        {
            currentAmmo += ammoStorage;
            ammoStorage = 0;
        }

        isReloading = false;

        if (reloadSlider != null)
        {
            reloadSlider.value = 1f; // Ensure slider value is set to 1 after reload
            reloadSlider.gameObject.SetActive(false); // Hide the reload slider
        }

        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {ammoStorage}";
        }
    }
}
