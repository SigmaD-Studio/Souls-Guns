using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AIFlip : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindAnyObjectByType<PlayerController>().gameObject;
    }


    void Update()
    {
        FlipSprite();
    }

    void FlipSprite()
    {
        
        Vector3 playerToMouseDir = player.transform.position - transform.position;

        if (playerToMouseDir.x != 0)
        {
            spriteRenderer.flipX = playerToMouseDir.x < 0;
        }
    }
}
