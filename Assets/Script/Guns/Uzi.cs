using UnityEngine;
using System.Collections;

public class Uzi : BaseWeapon
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

    public override void Start()
    {
        base.Start();
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
