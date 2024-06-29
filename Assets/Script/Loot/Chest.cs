using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Chest : MonoBehaviour
{
    [SerializeField]GameObject drop;
    Animator ani;
    Collision2D col;
    private void Start()
    {

        ani = GetComponent<Animator>();
        col = GetComponent<Collision2D>();  
    }

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {*/
            
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ani.SetTrigger("Open");
        }
    }

    public void OpenChest()
    {
        GetComponent<LootBag>().InstanateLoot(drop.transform.position);
    }
}
