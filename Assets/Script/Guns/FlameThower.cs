using UnityEngine;
using System.Collections;

public class FlamethrowerController : MonoBehaviour
{
    public Transform firePoint; // The point where flames are emitted
    public GameObject flamePrefab; // Prefab for the flame physics object
    public float fuelCapacity = 100f; // Maximum fuel capacity
    public float fuelConsumptionRate = 10f; // Rate at which fuel is consumed per second
    public float reloadTime = 2f; // Time it takes to reload fuel
    public float shootForce = 10f; // Force applied to the flames
    public float shootSpread = 15f; // Spread angle of flame direction

    private float currentFuel; // Current fuel level
    private bool isShooting; // Flag to check if flamethrower is shooting
    private bool isReloading; // Flag to check if reloading fuel

    void Start()
    {
        currentFuel = fuelCapacity;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetButton("Fire1") && currentFuel > 0)
        {
            StartFlamethrower();
        }
        else
        {
            StopFlamethrower();
        }

        if (currentFuel <= 0)
        {
            StartCoroutine(ReloadFuel());
        }
    }

    void StartFlamethrower()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootFlames());
        }

        currentFuel -= fuelConsumptionRate * Time.deltaTime;
    }

    void StopFlamethrower()
    {
        if (isShooting)
        {
            isShooting = false;
        }
    }

    IEnumerator ShootFlames()
    {
        while (isShooting && currentFuel > 0)
        {
            float spreadAngle = Random.Range(-shootSpread / 2f, shootSpread / 2f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, spreadAngle);

            GameObject flame = Instantiate(flamePrefab, firePoint.position, firePoint.rotation * rotation);
            Rigidbody2D rb = flame.GetComponent<Rigidbody2D>();
            rb.AddForce(flame.transform.right * shootForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.1f); // Adjust delay between each flame shot
        }
    }

    IEnumerator ReloadFuel()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentFuel = fuelCapacity;
        isReloading = false;
    }
}
