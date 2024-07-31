using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int maxEnemyCount;
    public int minEnemyCount;
    public GameObject[] Enemys;
    public GameObject Gate;
    
    private int EstimateEnemyCount;
    
    public void SpawnEnemy()
    {

        EstimateEnemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);

            Debug.Log("Player entered Room");
            Gate.SetActive(true);
            for (int i = 0; i < EstimateEnemyCount; i++)
            {
                int randomEnemy = Random.Range(0, Enemys.Length);
                GameObject SpawnEnemy = Instantiate(Enemys[randomEnemy], RandomPos(gameObject.GetComponent<Collider2D>()), Quaternion.identity);

            }
    }

    Vector2 RandomPos(Collider2D collision)
    {
        var pos = new Vector3(
            Random.Range(collision.bounds.min.x, collision.bounds.max.x),
            Random.Range(collision.bounds.min.y, collision.bounds.max.y),
            0

         );
        return pos;
    }

    
}
