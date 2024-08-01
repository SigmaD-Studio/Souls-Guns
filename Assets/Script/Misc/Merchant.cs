using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    bool detectPlayer = false;
    public GameObject chest;
    public Transform spawnPos;
    public GameObject sign;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer && Input.GetKeyDown(KeyCode.E))
        {
            GameObject bullet = Instantiate(chest, spawnPos.position, Quaternion.identity);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = true;
            sign.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = false;
            sign.gameObject.SetActive(false);
        }
    }
}
