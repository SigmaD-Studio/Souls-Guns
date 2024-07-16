using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAim : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {

    }
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>().gameObject;

    }

    void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {

        Vector3 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        Flip(angle);

    }


    private void Flip(float handAngle)
    {
        if (handAngle > 90 || handAngle < -90)
        {
            transform.localScale = new Vector3(0.82f, -0.82f, 0f);
        }
        else if (handAngle < 90 || handAngle > -90)
        {
            transform.localScale = new Vector3(0.82f, 0.82f, 0f);
        }
    }
}
