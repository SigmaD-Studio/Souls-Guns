using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            PlayerPrefs.SetInt("LoadNextScene", 3);
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
