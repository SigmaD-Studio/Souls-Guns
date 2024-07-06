using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Sniper : MonoBehaviour
{
    public Transform firePoint; // The point where bullets are instantiated
    public Transform flashPoint; // The point where muzzle flash is instantiated
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash prefab to instantiate
    public float bulletSpeed = 50f; // Speed of the bullet
    public float fireRate = 1f; // Time between shots
    public int maxAmmo = 5; // Maximum ammo capacity
    public int maxAmmoStorage = 15; // Maximum ammo storage capacity
    public float reloadTime = 3f; // Time it takes to reload
    public float muzzleFlashDuration = 0.1f; // Duration of the muzzle flash
    public TextMeshProUGUI ammoText; // Reference to UI text for displaying ammo count
    public Slider reloadSlider; // Reference to UI Slider for ammo display
    public TextMeshProUGUI gunNameText; // Reference to UI text for displaying gun name

    public string gunName = "Sniper Rifle"; // Name of the gun

    private int currentAmmo; // Current ammo count
    private int currentAmmoStorage; // Current ammo storage count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading

    void Start()
    {
        reloadSlider.maxValue = maxAmmo;
        reloadSlider.value = currentAmmo;

        currentAmmo = maxAmmo;
        currentAmmoStorage = maxAmmoStorage;
        UpdateAmmoUI();

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

    IEnumerator Reload()
    {
        isReloading = true;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true); // Show the reload slider
            reloadSlider.value = 0f; // Reset the slider value
        }

        float timer = 0f;
        while (timer < reloadTime)
        {
            if (reloadSlider != null)
            {
                reloadSlider.value = Mathf.Lerp(0f, maxAmmo, timer / reloadTime); // Update slider value based on reload progress
            }
            timer += Time.deltaTime;
            yield return null;
        }
        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(currentAmmoStorage, ammoNeeded);
        currentAmmo += ammoToReload;
        currentAmmoStorage -= ammoToReload;

        isReloading = false;

        UpdateAmmoUI();
    }

    IEnumerator ShowMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, flashPoint.position, flashPoint.rotation, flashPoint);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.SetActive(false);
        Destroy(muzzleFlash);
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
