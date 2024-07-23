using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PickupItem : MonoBehaviour
{

    [SerializeField] private Transform weaponHolder;
    private GameObject currentWeapon;
    bool equipped = false;
    int equipingWepNum = 0;

    private void Start()
    {
         
    }
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.R))
        {

            
        }


        if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
        {
            equipingWepNum++;
            if (equipingWepNum >= weaponHolder.transform.childCount)
            {
                equipingWepNum = 0;
            }

            for (int i = 0; i < weaponHolder.transform.childCount; i++)
            {
                if (i == equipingWepNum)
                {
                    EquipWeapon(i);
                }
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            PickUpWeapon(collision.gameObject);
            
        }
    }

    void PickUpWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(weaponHolder);
        weapon.transform.localPosition = new Vector3(0.35f, 0, 0);
        weapon.transform.localRotation = Quaternion.identity;
        weapon.SetActive(false); 
    }

    void EquipWeapon(int index)
    {

        for (int i = 0;i < weaponHolder.transform.childCount; i++)
        {
            weaponHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
        currentWeapon = weaponHolder.GetChild(index).gameObject;
        currentWeapon.SetActive(true);
        currentWeapon.SendMessage("isEquiping", true);
        currentWeapon.SendMessage("UpdateUI");
    }
}
