using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHPHandler : MonoBehaviour
{
    int life = 6;
    [SerializeField] GameObject[] lifeHearts;
    [SerializeField] GameObject Death;

    private void Start()
    {
        life = PlayerPrefs.GetInt("Life");
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

        if (life <= 0)
        {
            Death.gameObject.SetActive(true);
        }
        PlayerPrefs.SetInt("Life", life);
    }



}
