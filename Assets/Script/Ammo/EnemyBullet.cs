using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed; // Speed of the bullet
    public float range; // Maximum distance the bullet can travel

    public float BulletTimeOut; // Distance the bullet has traveled



    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    void Update()
    {
        // Move the bullet forward based on its speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an enemy
        DamagedHandle player = other.GetComponent<DamagedHandle>();
        if (player != null)
        {
            DestroyBullet();
        }
        else
        {
            // Destroy the bullet if it hits any other object
            DestroyBullet();

        }

        void DestroyBullet()
        {
            // Clean up the bullet GameObject
            Destroy(gameObject);
        }
    }
}
