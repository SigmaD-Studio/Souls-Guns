using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAK : MonoBehaviour
{

    public Transform firePoint; // The point where bullets are instantiated
    public Transform flashPoint; // The point where muzzle flash appears
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public GameObject muzzleFlashPrefab; // The muzzle flash prefab to instantiate
    public float bulletSpeed = 20f; // Speed of the bullet
    public float muzzleFlashDuration = 0.05f; // Duration of the muzzle flash
    public float spreadAngle = 5f; // Bullet spread angle


    public void Shoot()
    {
       
            float angle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = rotation * Vector2.right * bulletSpeed;


            StartCoroutine(ShowMuzzleFlash());
        
        
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
