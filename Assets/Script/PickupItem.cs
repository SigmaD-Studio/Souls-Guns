using UnityEngine;
using System.Collections.Generic;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    private List<BaseWeapon> weapons = new List<BaseWeapon>();
    private BaseWeapon currentWeapon;
    private int equippingWepNum = 0;

    private void Start()
    {
        for (int i = 0; i < weaponHolder.childCount; i++)
        {
            BaseWeapon weapon = weaponHolder.GetChild(i).GetComponent<BaseWeapon>();
            if (weapon != null)
            {
                weapons.Add(weapon);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && (currentWeapon == null || !currentWeapon.isReloading))
        {
            EquipNextWeapon();
        }
    }

    private void EquipNextWeapon()
    {
        equippingWepNum++;
        if (equippingWepNum >= weapons.Count)
        {
            equippingWepNum = 0;
        }
        EquipWeapon(equippingWepNum);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            PickUpWeapon(collision.gameObject);
        }
    }

    private void PickUpWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(weaponHolder);
        weapon.transform.localPosition = new Vector3(0.35f, 0, 0);
        weapon.transform.localRotation = Quaternion.identity;
        weapon.SetActive(false);

        BaseWeapon baseWeapon = weapon.GetComponent<BaseWeapon>();
        if (baseWeapon != null)
        {
            weapons.Add(baseWeapon);
        }
    }

    private void EquipWeapon(int index)
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        currentWeapon = weapons[index];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.isEquiping(true);
    }
}
