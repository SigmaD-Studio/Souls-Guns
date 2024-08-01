using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReturnLobby();
        }
    }
    public void ReturnLobby()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(player);
    }
}
