using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]GameObject drop;
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                GetComponent<LootBag>().InstanateLoot(transform.position);
            }
        }
    }*/


    private void Update()
    {
     if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<LootBag>().InstanateLoot(drop.transform.position);
        }
  
    }
}
