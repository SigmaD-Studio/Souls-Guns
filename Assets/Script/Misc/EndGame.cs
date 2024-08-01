using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            PlayerPrefs.SetInt("LoadNextScene", 0);
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
