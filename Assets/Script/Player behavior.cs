using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Playerbehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private BoxCollider2D _boxCollider;
    private AudioSource _audioSource;

    private float speedX, speeedY;

    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] Transform hand;
    [SerializeField] GameObject _arrow;



    private float angle;






    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateHand();
    }


    private void RotateHand()
    {

        angle = Utility.AngleTowardsMouse(hand.position);
        hand.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }


    private void Movement()
    {
        speedX = Input.GetAxisRaw("Horizontal");

        speeedY = Input.GetAxisRaw("Vertical");


        _rb.velocity = new Vector2(speedX * moveSpeed, speeedY * moveSpeed);
        
        float moving = _rb.velocity.magnitude;
        float handAngle = hand.rotation.z;
        
        if (moving > 0)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }

        _anim.SetFloat("Speed", moving);

        Flip(handAngle);


    }

    private void Flip(float handAngle)
    {
        if (handAngle > 0.7 || handAngle < -0.7)
        {
            transform.localScale = new Vector3(-2f, 2f, 0f);
            hand.transform.localScale = new Vector3(-1f, 1f, 0f);
        }
        else if (handAngle < 0.7 || handAngle > -0.7)
        {
            transform.localScale = new Vector3(2f, 2f, 0f);
            hand.transform.localScale = new Vector3(1f, 1f, 0f);
        }
    }
}
