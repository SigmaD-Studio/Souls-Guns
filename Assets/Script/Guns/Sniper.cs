using UnityEngine;
using System.Collections;

public class Sniper : MonoBehaviour
{
    public Transform firePoint; // The point where bullets are instantiated
    public Transform flashPoint; // The point where muzzle flash is instantiated
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash prefab to instantiate
    public float bulletSpeed = 50f; // Speed of the bullet
    public float fireRate = 1f; // Time between shots
    public int maxAmmo = 5; // Maximum ammo capacity
    public float reloadTime = 3f; // Time it takes to reload
    public float muzzleFlashDuration = 0.1f; // Duration of the muzzle flash


    private int currentAmmo; // Current ammo count
    private float fireTimer; // Timer to handle fire rate
    private bool isReloading = false; // Flag to check if reloading
    private bool isZoomed = false; // Flag to check if zoomed in

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

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

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
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, flashPoint.position, flashPoint.rotation, flashPoint);
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.SetActive(false);
        Destroy(muzzleFlash);
    }
}
