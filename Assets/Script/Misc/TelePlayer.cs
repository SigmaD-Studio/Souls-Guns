using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePlayer : MonoBehaviour
{

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
    }

}

