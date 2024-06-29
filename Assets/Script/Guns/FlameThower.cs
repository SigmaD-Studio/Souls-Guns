using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FlamethrowerController : MonoBehaviour
{
    public Transform firePoint; // The point where flames are emitted
    public GameObject flamePrefab; // Prefab for the flame physics object
    public float fuelCapacity = 100f; // Maximum fuel capacity
    public float fuelConsumptionRate = 10f; // Rate at which fuel is consumed per second
    public float reloadTime = 2f; // Time it takes to reload fuel
    public float shootForce = 10f; // Force applied to the flames
    public float shootSpread = 15f; // Spread angle of flame direction
    public string flamethrowerName = "Flamethrower"; // Name of the flamethrower
    public TextMeshProUGUI fuelText; // Reference to UI text for displaying fuel level
    public TextMeshProUGUI flamethrowerNameText; // Reference to UI text for displaying flamethrower name
    public Slider reloadSlider; // Reference to UI Slider for reload progress
    public float maxStorage = 300f; // Maximum storage capacity

    private float currentFuel; // Current fuel level
    private float currentStorage; // Current storage level
    private bool isShooting; // Flag to check if flamethrower is shooting
    private bool isReloading; // Flag to check if reloading fuel

    void Start()
    {
        InitializeFuel();
        UpdateFuelUI();
        if (reloadSlider != null)
        {
            reloadSlider.maxValue = fuelCapacity; // Set the max value of the slider to the fuel capacity
            reloadSlider.value = currentFuel; // Set the current value of the slider to the current fuel level
            reloadSlider.gameObject.SetActive(false); // Initially hide the slider
        }
        if (flamethrowerNameText != null)
        {
            flamethrowerNameText.text = flamethrowerName; // Set the flamethrower name text
        }
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

        if (currentFuel <= 0 && !isReloading && currentStorage > 0)
        {
            StartCoroutine(ReloadFuel());
        }

        // Check if out of fuel and storage to destroy the flamethrower
        if (currentFuel <= 0 && currentStorage <= 0)
        {
            Destroy(gameObject);
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
        UpdateFuelUI();
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
                reloadSlider.value = Mathf.Lerp(0f, fuelCapacity, timer / reloadTime); // Update slider value based on reload progress
            }
            timer += Time.deltaTime;
            yield return null;
        }

        if (currentStorage > fuelCapacity)
        {
            currentFuel = fuelCapacity;
            currentStorage -= fuelCapacity;
        }
        else
        {
            currentFuel = currentStorage;
            currentStorage = 0;
        }

        isReloading = false;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false); // Hide the reload slider
        }

        UpdateFuelUI();

        // Check again if out of fuel and storage to destroy the flamethrower
        if (currentFuel <= 0 && currentStorage <= 0)
        {
            Destroy(gameObject);
        }
    }

    void InitializeFuel()
    {
        currentFuel = fuelCapacity;
        currentStorage = maxStorage;
    }

    void UpdateFuelUI()
    {
        if (fuelText != null)
        {
            fuelText.text = "" + currentFuel.ToString("F1") + " / "+ currentStorage.ToString("F1");
        }

        if (reloadSlider != null)
        {
            reloadSlider.value = currentFuel; // Update slider value based on current fuel

            bool shouldShowReloadSlider = currentFuel < fuelCapacity; // Show slider if fuel is not at maximum
            reloadSlider.gameObject.SetActive(shouldShowReloadSlider);

            if (currentFuel == 0 && !isReloading)
            {
                reloadSlider.value = 1f; // Set the slider value to maximum to indicate completed reload
            }
            else if (isReloading && currentFuel < fuelCapacity)
            {
                reloadSlider.value = Mathf.Lerp(0f, fuelCapacity, (fuelCapacity - currentFuel) / fuelCapacity); // Update slider value based on reload progress
            }
        }
    }
}
