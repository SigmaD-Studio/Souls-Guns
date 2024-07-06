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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Get tagged Player");
            if (Input.GetKey(KeyCode.F)) 
            {
                Debug.Log("Get key f");
                ani.SetTrigger("Open");
            }
        }
    }

    public void OpenChest()
    {
        GetComponent<LootBag>().InstanateLoot(drop.transform.position);
    }
}
