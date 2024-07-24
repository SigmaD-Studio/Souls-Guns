using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class BaseWeapon : MonoBehaviour
{
    public string weaponName;
    public int maxAmmo;
    public int maxAmmoStorage;
    public float reloadTime;

    protected int currentAmmo;
    protected int currentAmmoStorage;
    public bool isReloading = false;
    protected bool isEquipped = false;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI gunNameText;
    public Slider reloadSlider;

    public virtual void FindUI()
    {
        ammoText = GameObject.Find("AmmoStorage").GetComponent<TextMeshProUGUI>();
        gunNameText = GameObject.Find("GunName").GetComponent<TextMeshProUGUI>();
        reloadSlider = GameObject.Find("GunSlider").GetComponent<Slider>();
    }

    public virtual void Start()
    {
        FindUI();
        currentAmmo = maxAmmo;
        currentAmmoStorage = maxAmmoStorage;
    }

    public virtual void OnEnable()
    {
        if (isEquipped)
        {
            GetComponent<Collider2D>().enabled = false;
            UpdateUI();
        }
    }

    public virtual void UpdateUI()
    {
        if (gunNameText != null) gunNameText.text = weaponName;
        if (ammoText != null) ammoText.text = $"{currentAmmo} / {currentAmmoStorage}";
        if (reloadSlider != null)
        {
            reloadSlider.maxValue = maxAmmo;
            reloadSlider.value = currentAmmo;
            reloadSlider.gameObject.SetActive(isEquipped);
        }
    }

    public virtual IEnumerator Reload()
    {
        isReloading = true;

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

        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(currentAmmoStorage, ammoNeeded);
        currentAmmo += ammoToReload;
        currentAmmoStorage -= ammoToReload;

        isReloading = false;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
        }

        UpdateUI();
    }

    public void isEquiping(bool value)
    {
        isEquipped = value;
        if (isEquipped)
        {
            UpdateUI();
        }
    }
}
