using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Chest : MonoBehaviour
{
    private bool playerInRange = false;
    [SerializeField]GameObject drop;
    Animator ani;
    
    private void Start()
    {

        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is in range and the F key is pressed
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Get key F");
            ani.SetTrigger("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Get tagged Player");
            playerInRange = true; // Set the flag to true when player enters the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player left the trigger");
            playerInRange = false; // Set the flag to false when player exits the trigger
        }
    }

    public void OpenChest()
    {
        GetComponent<LootBag>().InstanateLoot(drop.transform.position);
        Destroy(gameObject);
    }
}
