using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    public void ReturnLobby()
    {
        SceneManager.LoadScene("0");
    }
}
