using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public float atk_range;
    public float atk_damage;
    public float rof;
    

    private float distance;
    private Animator ani;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //inRangeAttack();
    }


    void inRangeAttack()
    {
        if (distance <= atk_range)
        {
            StartCoroutine("Attack");
            //player.GetComponent<Player_Health>().TakeDamage(atk_damage);
        }
    }

    IEnumerator Attack()
    {
        ani.SetTrigger("Attack");
        yield return new WaitForSeconds(rof);
    }

    
}
