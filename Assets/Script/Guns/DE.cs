using UnityEngine;
using System.Collections;

public class DE : MonoBehaviour
{
    public Transform firePoint; // The point where bullets are instantiated
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public float bulletSpeed = 20f; // Speed of the bullets
    public float fireRate = 0.3f; // Time between shots
    public int maxAmmo = 6; // Maximum ammo capacity
    public float reloadTime = 2f; // Time it takes to reload

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

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
