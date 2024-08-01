using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        
        PlayerPrefs.SetInt("SceneLoaded", 0);
        PlayerPrefs.SetInt("Life", 6);
    }

    
}
