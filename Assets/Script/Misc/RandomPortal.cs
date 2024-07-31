using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int check = PlayerPrefs.GetInt("SceneLoaded");
        if (collision.CompareTag("Player") && check < 3)
        {

            int counter = PlayerPrefs.GetInt("LoadNextScene");
            counter = counter + 1;
            PlayerPrefs.SetInt("SceneLoaded", counter);


            int index = Random.Range(4, 7);
            PlayerPrefs.SetInt("LoadNextScene", index);
            
            



            SceneManager.LoadScene("LoadingScene");
        }
        else if (collision.CompareTag("Player") && check >= 3)
        {
            SceneManager.LoadScene("BossScene");
        }
    }
}
