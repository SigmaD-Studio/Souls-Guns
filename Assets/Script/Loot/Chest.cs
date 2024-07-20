using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject drop;
    Animator ani;
    bool playerInTrigger = false; // Flag to check if the player is in the trigger area

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Get key f");
            ani.SetTrigger("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Get tagged Player");
            playerInTrigger = true; // Player entered the trigger area
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = false; // Player exited the trigger area
        }
    }

    public void OpenChest()
    {
        GetComponent<LootBag>().InstanateLoot(drop.transform.position);
    }
}
