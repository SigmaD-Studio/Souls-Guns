using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(player, transform.position, Quaternion.identity);
    }

    
}
