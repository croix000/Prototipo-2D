using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {

        Cursor.visible = true;
    }
    public void  PlayGame()
    {
        PlayerPrefs.SetInt("Health", 2);
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {

        Application.Quit();
    }
}
