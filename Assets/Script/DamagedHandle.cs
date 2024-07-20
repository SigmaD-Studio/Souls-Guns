using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedHandle : MonoBehaviour
{
    UIHPHandler ui;

    private void Start()
    {
        ui = GetComponent<UIHPHandler>();
    }

    public void DamageTaken()
    {
        if (ui != null)
        {
            ui.Damaged();
        }
    }
}
