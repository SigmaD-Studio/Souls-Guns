using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class RocketLauncher : MonoBehaviour
{
    public string weaponName = "Rocket Launcher"; // Name of the weapon
    public Transform firePoint; // The point where rockets are instantiated
    public Transform backFlashPoint; // The point where back flash is instantiated
    public GameObject rocketPrefab; // The rocket prefab to instantiate
    public GameObject explosionPrefab; // The explosion prefab to instantiate on impact
    public GameObject backFlashPrefab; // The back flash prefab to instantiate
    public GameObject destructionEffectPrefab; // Prefab for destruction effect
    public float rocketSpeed = 20f; // Speed of the rocket
    public float fireRate = 1f; // Time between shots
    public int maxAmmo = 5; // Maximum ammo capacity
    public int maxAmmoStorage = 15; // Maximum ammo storage capacity
    public float reloadTime = 3f; // Time it takes to reload
    public float backFlashDuration = 0.1f; // Duration of the back flash
    public TextMeshProUGUI ammoText; // Reference to UI text for displaying ammo count
    public Slider reloadSlider; // Reference to UI Slider for reload progress

    private int currentAmmo; // Current ammo count
    private int currentAmmoStorage; // Current ammo storage count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading

    void Start()
    {
        currentAmmo = maxAmmo;
        currentAmmoStorage = maxAmmoStorage;
        UpdateAmmoUI();
        if (reloadSlider != null)
        {
            reloadSlider.maxValue = currentAmmo; // Set the max value of the slider to the reload time
            reloadSlider.value = currentAmmo; // Set the current value of the slider to the reload time
            reloadSlider.gameObject.SetActive(true); // Initially hide the slider
        }
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if(currentAmmoStorage <= 0 && currentAmmo <= 0)
        {
            return;
        }

        else if (currentAmmo <= 0)
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

        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadSlider.maxValue = reloadTime;
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true); // Show the reload slider
        }

        float timer = 0f;
        while (timer < reloadTime)
        {
            if (reloadSlider != null)
            {
                reloadSlider.value = timer; // Update slider value based on reload progress
            }
            timer += Time.deltaTime;
            yield return null;
        }

        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(currentAmmoStorage, ammoNeeded);
        currentAmmo += ammoToReload;
        currentAmmoStorage -= ammoToReload;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true); // Hide the reload slider
        }

        isReloading = false;

        UpdateAmmoUI();
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

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "" + currentAmmo + " / " + currentAmmoStorage;
        }

        if (reloadSlider != null)
        {
            reloadSlider.value = currentAmmo; // Update slider value based on current ammo

            bool shouldShowReloadSlider = currentAmmoStorage > 0 || currentAmmo > 0; // Show slider if either storage or ammo is greater than zero
            reloadSlider.gameObject.SetActive(shouldShowReloadSlider);

            if (currentAmmo == 0 && !isReloading)
            {
                reloadSlider.value = 1f; // Set the slider value to maximum to indicate completed reload
            }
            else if (isReloading && currentAmmo < maxAmmo)
            {
                reloadSlider.value = Mathf.Lerp(0f, maxAmmo, (maxAmmo - currentAmmo) / (float)maxAmmo); // Update slider value based on reload progress
            }
        }
    }
}
