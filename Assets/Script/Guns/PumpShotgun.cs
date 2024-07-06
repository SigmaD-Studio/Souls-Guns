using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ShotgunController : MonoBehaviour
{
    public Transform firePoint; // The point where bullets are instantiated
    public Transform flashPoint; // The point where muzzle flash appears
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash prefab to instantiate
    public float bulletSpeed = 20f; // Speed of the bullets
    public float fireRate = 1f; // Time between shots
    public float spreadAngle = 15f; // Spread angle in degrees
    public int maxAmmo = 8; // Maximum ammo capacity
    public int maxAmmoStorage = 32; // Maximum ammo storage capacity
    public float reloadTime = 2f; // Time it takes to reload one bullet
    public float muzzleFlashDuration = 0.05f; // Duration of the muzzle flash
    public TextMeshProUGUI ammoText; // Reference to UI text for displaying ammo count
    public TextMeshProUGUI gunNameText; // Reference to UI text for displaying gun name
    public Slider reloadSlider; // Reference to UI Slider for reload progress

    private int currentAmmo; // Current ammo count
    private int ammoStorage; // Ammo storage count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading
    private int pelletCount; // Total count of pellets fired

    // Number of pellets per shot
    private int pelletsPerShot = 5;

    void Start()
    {
        reloadSlider.maxValue = maxAmmo;
        reloadSlider.value = currentAmmo;
        InitializeAmmo();
        UpdateAmmoUI();
        if (gunNameText != null)
        {
            gunNameText.text = "Shotgun"; // Set the gun name text (assuming this is for a shotgun)
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
            if (ammoStorage > 0)
            {
                StartCoroutine(ReloadOneBullet()); // Start reloading one bullet
            }
            else
            {
                // Handle out of ammo condition here (e.g., stop shooting)
                return;
            }
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

        for (int i = 0; i < pelletsPerShot; i++)
        {
            pelletCount++; // Increment the pellet count

            float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = rotation * Vector2.right * bulletSpeed;
        }

        StartCoroutine(ShowMuzzleFlash());

        UpdateAmmoUI();
    }

    IEnumerator ReloadOneBullet()
    {
        isReloading = true;
        reloadSlider.value = currentAmmo;
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true); // Show the reload slider
            reloadSlider.value = 0f; // Reset the slider value
        }

        yield return new WaitForSeconds(reloadTime); // Wait for reloadTime

        currentAmmo++; // Add one bullet to current ammo
        ammoStorage--; // Decrease one bullet from ammo storage

        if (currentAmmo < maxAmmo && ammoStorage > 0)
        {
            StartCoroutine(ReloadOneBullet()); // If there's more to reload, continue reloading
        }
        else
        {
            isReloading = false;

            if (reloadSlider != null)
            {
                reloadSlider.value = currentAmmo; // Ensure slider value is set to current ammo after reload
                reloadSlider.gameObject.SetActive(false); // Hide the reload slider
            }
        }

        UpdateAmmoUI();
    }

    void InitializeAmmo()
    {
        currentAmmo = maxAmmo;
        ammoStorage = maxAmmoStorage; // Initialize ammo storage
        pelletCount = 0; // Initialize pellet count
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {ammoStorage}";
        }

        if (reloadSlider != null)
        {
            reloadSlider.value = currentAmmo; // Update slider value based on current ammo

            bool shouldShowReloadSlider = ammoStorage > 0 || currentAmmo > 0; // Show slider if either storage or ammo is greater than zero
            reloadSlider.gameObject.SetActive(shouldShowReloadSlider);

            if (currentAmmo == 0 && !isReloading)
            {
                reloadSlider.value = 1f; // Set the slider value to maximum to indicate completed reload
            }
            else if (isReloading && currentAmmo < maxAmmo)
            {
                reloadSlider.value = currentAmmo; 
                //reloadSlider.value = Mathf.Lerp(0f, maxAmmo, (maxAmmo - currentAmmo) / (float)maxAmmo);
            }
        }
    }

    IEnumerator ShowMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, flashPoint.position, flashPoint.rotation);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.SetActive(false);
        Destroy(muzzleFlash);
    }
}
