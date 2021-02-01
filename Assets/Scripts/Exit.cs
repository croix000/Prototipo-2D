using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Exit : MonoBehaviour
{
    public string levelToLoad;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("Health", other.GetComponent<Player>().GetHealth());
            SceneManager.LoadScene(levelToLoad);
        }
    }

}

