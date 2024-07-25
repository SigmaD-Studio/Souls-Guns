using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public float distance;
    


    private void Awake()
    {
        
    }
    void Start()
    {

    }

    void Update()
    {
        RotateGun();
        HandleShooting();
        distance = Vector2.Distance(transform.position, player.transform.position);
    }

    private void RotateGun()
    {
        Vector3 PlayerPos = player.transform.position;
        Vector3 direction = (PlayerPos - enemy.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        Flip(angle);

    }


    private void Flip(float handAngle)
    {
        if (handAngle > 90 || handAngle < -90)
        {
            transform.localScale = new Vector3(1.5f, -1.5f, 0f);
        }
        else if (handAngle < 90 || handAngle > -90)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 0f);
        }
    }

    private void HandleShooting()
    {   
        if (distance < 6f)
        {
            // Shoot
            
        }
    }
}
