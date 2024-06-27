using UnityEngine;
using System.Collections;

public class ShotgunController : MonoBehaviour
{
    public Transform firePoint; // The point where bullets are instantiated
    public Transform flashPoint;
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash prefab to instantiate
    public int pelletsPerShot = 10; // Number of pellets per shotgun blast
    public float bulletSpeed = 20f; // Speed of the bullets
    public float fireRate = 1f; // Time between shots
    public float spreadAngle = 15f; // Spread angle in degrees
    public int maxAmmo = 8; // Maximum ammo capacity
    public float reloadTime = 2f; // Time it takes to reload
    public float muzzleFlashDuration = 0.05f; // Duration of the muzzle flash

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

        for (int i = 0; i < pelletsPerShot; i++)
        {
            float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = rotation * Vector2.right * bulletSpeed;
        }

        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    IEnumerator ShowMuzzleFlash()
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, flashPoint.position, flashPoint.rotation);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        Destroy(muzzleFlash);
    }
}
