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
    public float bulletSpeed = 20f; // Speed of the bullet
    public float fireRate = 0.1f; // Time between shots
    public int maxAmmo = 30; // Maximum ammo capacity
    public int maxAmmoStorage = 90; // Maximum ammo storage capacity
    public float reloadTime = 2f; // Time it takes to reload
    public float muzzleFlashDuration = 0.05f; // Duration of the muzzle flash
    public float spreadAngle = 5f; // Bullet spread angle
    public string gunName = "Assault Rifle"; // Name of the gun

    private int currentAmmo; // Current ammo count
    private int currentAmmoStorage; // Current ammo storage count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading


    public TextMeshProUGUI ammoText; // Reference to UI text for displaying ammo count
    public TextMeshProUGUI gunNameText; // Reference to UI text for displaying gun name
    public Slider reloadSlider; // Reference to UI Slider for reload progress

    void FindUI()
    {
        ammoText = GameObject.Find("AmmoStorage").GetComponent<TextMeshProUGUI>();
        gunNameText = GameObject.Find("GunName").GetComponent<TextMeshProUGUI>();
        reloadSlider = GameObject.Find("GunSlider").GetComponent<Slider>();
    }

    private bool isEquiped = false;
    public void isEquiping(bool value)
    {
        isEquiped = value;
    }

    void Start()
    {
        FindUI();
        currentAmmo = maxAmmo;
        currentAmmoStorage = maxAmmoStorage;

    }

    private void OnEnable()
    {
        if (isEquiped)
        {
            UpdateUI();
            GetComponent<Collider2D>().enabled = false;
        }

    }

    void UpdateUI()
    {

        gunNameText.text = gunName;
        ammoText.text = "" + currentAmmo + " / " + currentAmmoStorage;
        reloadSlider.value = currentAmmo; // Update slider value based on current ammo
        reloadSlider.maxValue = maxAmmo;  // Set the max value of the slider to the max ammo

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

    void Update()
    {
        if (isEquiped == true)
        {
            if (isReloading)
            {
                return;
            }

            if (currentAmmo <= 0)
            {
                if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(ReloadOneBullet());
                }
                // No need to destroy the gun object when out of ammo
                return;
            }

            if (Input.GetButton("Fire1") && fireTimer <= 0)
            {
                Shoot();
                fireTimer = fireRate;
            }

            fireTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        currentAmmo--;

        // Instantiate the bullet with spread
        for (int i = 0; i < 5; i++)
        {
            float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = rotation * Vector2.right * bulletSpeed;
        }
        

        StartCoroutine(ShowMuzzleFlash());

        UpdateUI();
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
        currentAmmoStorage--; // Decrease one bullet from ammo storage

        if (currentAmmo < maxAmmo && currentAmmoStorage > 0)
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

        UpdateUI();
    }

    IEnumerator ShowMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, flashPoint.position, flashPoint.rotation, firePoint);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.SetActive(false);
        Destroy(muzzleFlash);
    }

}
