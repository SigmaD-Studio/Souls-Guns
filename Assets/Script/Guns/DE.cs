using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DE : MonoBehaviour
{
    public Transform firePoint; // The point where bullets are instantiated
    public Transform muzzleFlashPoint; // The point where muzzle flash appears
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash prefab to instantiate
    public float bulletSpeed = 20f; // Speed of the bullets
    public float fireRate = 0.3f; // Time between shots
    public int maxAmmo = 6; // Maximum ammo capacity
    public float reloadTime = 2f; // Time it takes to reload
    public float muzzleFlashDuration = 0.05f; // Duration of the muzzle flash
    public string gunName = "Desert Eagle"; // Name of the gun
    public TextMeshProUGUI ammoText; // Reference to UI text for displaying ammo count
    public TextMeshProUGUI gunNameText; // Reference to UI text for displaying gun name
    public Slider reloadSlider; // Reference to UI Slider for reload progress

    private int currentAmmo; // Current ammo count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading

    void Start()
    {
        InitializeAmmo();
        UpdateAmmoUI();
        if (reloadSlider != null)
        {
            reloadSlider.maxValue = maxAmmo; // Set the max value of the slider to the max ammo
            reloadSlider.value = currentAmmo; // Set the current value of the slider to the current ammo
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

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        StartCoroutine(ShowMuzzleFlash());

        UpdateAmmoUI();
    }

    IEnumerator ShowMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, muzzleFlashPoint.position, muzzleFlashPoint.rotation);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.SetActive(false);
        Destroy(muzzleFlash);
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

        currentAmmo = maxAmmo;

        isReloading = false;

        if (reloadSlider != null)
        {
            reloadSlider.value = maxAmmo; // Ensure slider value is set to maxAmmo after reload
            reloadSlider.gameObject.SetActive(false); // Hide the reload slider
        }

        UpdateAmmoUI();
    }

    void InitializeAmmo()
    {
        currentAmmo = maxAmmo;
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {maxAmmo}";
        }

        if (reloadSlider != null)
        {
            reloadSlider.value = currentAmmo; // Update slider value based on current ammo

            bool shouldShowReloadSlider = currentAmmo < maxAmmo; // Show slider if ammo is not at maximum
            reloadSlider.gameObject.SetActive(shouldShowReloadSlider);

            if (currentAmmo == 0 && !isReloading)
            {
                reloadSlider.value = 1f; // Set the slider value to maximum to indicate completed reload
            }
        }
    }
}
