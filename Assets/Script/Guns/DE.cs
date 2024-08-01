using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DE : BaseWeapon
{
    public Transform firePoint;
    public Transform flashPoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 0.1f;
    public float muzzleFlashDuration = 0.05f;
    public float spreadAngle = 5f;

    private float fireTimer;


    public AudioClip shootSound; // Audio clip for shooting sound
    public AudioClip reloadSound; // Audio clip for reloading sound
    private AudioSource audioSource; // Audio source component

    public override void Start()
    {
        base.Start();
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
        currentAmmo--;

        float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
        Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = rotation * Vector2.right * bulletSpeed;

        StartCoroutine(ShowMuzzleFlash());

        UpdateUI();
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

    }
    public override IEnumerator Reload()
    {
        isReloading = true;
        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = 0f;
        }

        float timer = 0f;
        while (timer < reloadTime)
        {
            if (reloadSlider != null)
            {
                reloadSlider.value = Mathf.Lerp(0f, maxAmmo, timer / reloadTime);
            }
            timer += Time.deltaTime;
            yield return null;
        }

        currentAmmo = maxAmmo; // Refill current ammo to maximum capacity

        isReloading = false;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false); // Hide the reload slider
        }

        UpdateUI();
    }

    void InitializeAmmo()
    {
        currentAmmo = maxAmmo;
        currentAmmoStorage = int.MaxValue; // Set currentAmmoStorage to a very large value to simulate infinite ammo
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
