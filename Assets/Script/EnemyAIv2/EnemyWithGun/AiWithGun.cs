using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiWithGun : MonoBehaviour
{
    public float speed;
    public float atkRange;
    public float burstCount;
    public GameObject gun;
    public float rof;
    public float burstDelay;


    private float distance;
    private bool isBursting = false;


    private Rigidbody2D rb;
    private Animator ani;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerController>().gameObject;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance > atkRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            float speeding = rb.velocity.magnitude;
            ani.SetFloat("Speed", speed);
        }
        else
        {
            ani.SetFloat("Speed", 0);

        }

        InRangeAttack(distance);


    }

    private void InRangeAttack(float distance)
    {

        /*timer += Time.deltaTime;
        if (distance <= atkRange && timer >= rof)
        {     
            gun.SendMessage("Shoot");
            timer = 0f;

        }*/
        if (distance <= atkRange && !isBursting)
        {
            StartCoroutine("BurstFire");
        }
        
    }

    private IEnumerator BurstFire()
    {
        
        isBursting = true;
        yield return new WaitForSeconds(burstDelay);
        for (int i = 0; i < burstCount; i++)
        {
            gun.SendMessage("Shoot");

            if (i < burstCount - 1) // Delay between shots in a burst
            {
                yield return new WaitForSeconds(rof);
            }
        }

         // Delay between bursts
        isBursting = false;
    }


    

    
}
