using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiWithGun : MonoBehaviour
{
    public float speed;
    public float atkRange;
    public float bust;
    public GameObject gun;

    private float distance;


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
        Flip(angle);

        if (distance > atkRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            float speeding = rb.velocity.magnitude;
            //ani.SetFloat("Speed", speed);
        }
        else
        {
            //ani.SetFloat("Speed", 0);

        }

        InRangeAttack(distance);


    }

    private void InRangeAttack(float distance)
    {
        
        if (distance <= atkRange)
        {
            for (int i = 0; i < bust; i++)
            {
                gun.SendMessage("Shoot");
            }
        }
    }



    private void Flip(float angle)
    {
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(-1f, 1f, 0f);
        }
        else if (angle < 90 || angle > -90)
        {
            transform.localScale = new Vector3(1f, 1f, 0f);
        }
    }

    
}
