using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void OnEnable()
    {
        
        
    }
    public void ReturnLobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("1");
        Destroy(player);
    }
}
