using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHPHandler : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] GameObject[] lifeHearts;

    private void Start()
    {
        foreach (GameObject go in lifeHearts)
        {
            go.active = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < life; i++)
        {
            lifeHearts[i].active = true;
        }
    }




    public void Damaged()
    {
        life--;
        foreach (GameObject go in lifeHearts)
        {
            go.active = false;
        }
    }



}
