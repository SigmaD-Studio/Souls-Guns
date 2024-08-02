using UnityEngine;
using System.Collections;

public class PumpShotgun : BaseWeapon
{
    public Transform firePoint;
    public Transform flashPoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 1f; // Increased fire rate for shotguns
    public float muzzleFlashDuration = 0.05f;
    public int pellets = 8; // Number of pellets fired per shot
    public float spreadAngle = 15f; // Increased spread angle for shotguns


    public AudioClip shootSound; // Audio clip for shooting sound
    public AudioClip reloadSound; // Audio clip for reloading sound
    private AudioSource audioSource; // Audio source component

    private float fireTimer;

    public override void Start()
    {
        base.Start();
        // Initialize ammo counts
        currentAmmo = Mathf.Min(maxAmmo, currentAmmo);
        currentAmmoStorage = Mathf.Max(0, currentAmmoStorage);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isEquipped)
        {
            if (isReloading)
            {
                return;
            }

            if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
            {
                if (currentAmmoStorage > 0)
                {
                    StartCoroutine(Reload());
                }
                return;
            }

            if (Input.GetButton("Fire1") && fireTimer <= 0)
            {
                Shoot();
                fireTimer = fireRate;
            }

            fireTimer -= Time.deltaTime;
        }
        if (currentAmmo <= 0 && currentAmmoStorage <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        currentAmmo--;

        for (int i = 0; i < pellets; i++)
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

    public override IEnumerator Reload()
    {
        // Stop reloading if current ammo is already full
        if (currentAmmo >= maxAmmo || currentAmmoStorage <= 0)
        {
            isReloading = false;
            if (reloadSlider != null)
            {
                reloadSlider.gameObject.SetActive(false);
            }
            yield break;
        }

        isReloading = true;
        reloadSlider.value = currentAmmo;
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = 0f;
        }

        yield return new WaitForSeconds(reloadTime); // Wait for reloadTime
        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        currentAmmo = Mathf.Min(maxAmmo, currentAmmo + 1); // Reload one bullet
        currentAmmoStorage--;

        if (currentAmmoStorage > 0 && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload()); // Continue reloading if there's more ammo
        }
        else
        {
            isReloading = false;
            if (reloadSlider != null)
            {
                reloadSlider.value = currentAmmo;
                reloadSlider.gameObject.SetActive(false);
            }
        }

        UpdateUI();
    }

    // Ensure this method matches the signature in BaseWeapon if it is intended to override a method
    public IEnumerator ShowMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, flashPoint.position, flashPoint.rotation, firePoint);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.SetActive(false);
        Destroy(muzzleFlash);
    }
}
