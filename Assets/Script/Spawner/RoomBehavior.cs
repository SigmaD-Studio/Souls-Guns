using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    Spawner _spawner;
    bool _enabled = false;
    public GameObject Gate;

    private void Start()
    {
        _spawner = GetComponent<Spawner>(); 
    }
    private void Update()
    {
        GameObject lastEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (lastEnemy == null && _enabled)
        {
            Gate.SetActive(false);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_enabled)
        {
            _spawner.SpawnEnemy();
            _enabled = true;
        }
    }
    




    
}
