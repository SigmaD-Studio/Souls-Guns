using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Aim : MonoBehaviour
{
    public Transform player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator aimAnimator;

    
    private void Awake()
    {
        aimAnimator = player.GetComponent<Animator>();
    }
    void Start()
    {
        mainCamera = Camera.main;

    }

    void Update()
    {
        RotateGun();
    }

    private void RotateGun()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - player.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        Flip(angle);

    }


    private void Flip(float handAngle)
    {
        if (handAngle > 90 || handAngle < -90)
        {
            transform.localScale = new Vector3(1f, -1f, 0f);
        }
        else if (handAngle < 90|| handAngle > -90)
        {
            transform.localScale = new Vector3(1f, 1f, 0f);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            aimAnimator.SetTrigger("Shoot");
        }
    }
}