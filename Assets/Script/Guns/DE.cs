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

    TextMeshProUGUI ammoText; // Reference to UI text for displaying ammo count
    TextMeshProUGUI gunNameText; // Reference to UI text for displaying gun name
    Slider reloadSlider; // Reference to UI Slider for reload progress

    private int currentAmmo; // Current ammo count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading

    GameObject UI;


    void FindUI()
    {
        UI = GameObject.Find("UICanvas");
        ammoText = GameObject.Find("AmmoStorage").GetComponent<TextMeshProUGUI>();
        gunNameText = GameObject.Find("GunName").GetComponent<TextMeshProUGUI>();
        reloadSlider = GameObject.Find("GunSlider").GetComponent<Slider>();
    }


    void Start()
    {
        FindUI();
        InitializeAmmo();
        
    }
    private void OnEnable()
    {
        if (isEquiped)
        {
            GetComponent<Collider2D>().enabled = false;
            UpdateUI();
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

    void Shoot()
    {
        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        StartCoroutine(ShowMuzzleFlash());

        UpdateUI();
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

        float timer = 0f;
        while (timer < reloadTime)
        {
            if (reloadSlider != null)
            {
                reloadSlider.value = timer / reloadTime; // Update slider value based on reload progress
            }
            timer += Time.deltaTime;
            yield return null;
        }

        currentAmmo = maxAmmo;

        isReloading = false;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true); // Hide the reload slider
        }

        UpdateUI();
    }

    void InitializeAmmo()
    {
        currentAmmo = maxAmmo;
    }

    void UpdateUI()
    {

        gunNameText.text = gunName;
        ammoText.text = "" + currentAmmo + " / " + maxAmmo;
        reloadSlider.value = currentAmmo; // Update slider value based on current ammo
        reloadSlider.maxValue = maxAmmo;  // Set the max value of the slider to the max ammo

        bool shouldShowReloadSlider = maxAmmo > 0 || currentAmmo > 0; // Show slider if either storage or ammo is greater than zero
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
    private bool isEquiped = false;
    public void isEquiping(bool value)
    {
        isEquiped = value;
        Debug.Log("IsEquiped");
    }
}
