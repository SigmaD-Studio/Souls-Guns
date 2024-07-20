using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagedHandle : MonoBehaviour
{
    UIHPHandler ui;
    public float imutity;

    private SpriteRenderer sr;
    bool isTakingDamage = false;


    private void Start()
    {
        ui = GetComponent<UIHPHandler>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void DamageTaken()
    {
        if (ui != null && isTakingDamage == false)
        {
            StartCoroutine(TakingDamage());
            
        }
    }

    IEnumerator TakingDamage()
    {
        isTakingDamage = true;
        
        
        float timer = 0f;
        ui.Damaged();
        while (timer < imutity)
        {
            StartCoroutine(Flashing());
            timer += Time.deltaTime;  
            yield return null;
        }
        
        yield return null;
        isTakingDamage = false;

    }
    IEnumerator Flashing()
    {
        yield return new WaitForSeconds(0.3f);
        sr.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.3f);
        sr.color = new Color(1f, 1f, 1f, 1f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ui != null && collision.tag == "EnemyBullet") 
        {
            Destroy(collision.gameObject);
            DamageTaken();
        }
    }
}


