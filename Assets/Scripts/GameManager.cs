using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject doorGo;
    [SerializeField] Player player;
    [SerializeField]
    private CameraShaker shaker;

    [SerializeField] AudioSource coinSFX;
    [SerializeField] AudioSource hurtSFX;
    [SerializeField] AudioSource loseSFX;
    [SerializeField] TextMeshProUGUI crystalsCollectedLabel;

    [SerializeField] TextMeshProUGUI healthLabel;
    int currentPortrait;
    [SerializeField] GameObject[] portraits;
    [SerializeField] GameObject deadPlayer;

    int crystalsCollected;
    int crystalsNeeded=3;
    private void Start()
    {
        player.onCrystalCollected += CrystalCollection;
        player.onDiamondCollected += Win;
        player.onShootExecuted += ShakeScreen;
        player.onPlayerDeath += HandleGameOver;
        player.onDamageReceived += HandleDamage;

        crystalsCollectedLabel.text = "X " + crystalsCollected + "/" + crystalsNeeded;
        StartHealthUI();
    }


    void CrystalCollection()
    {
        SoundManager.Instance.PlaySFX(coinSFX);
        crystalsCollected++;
        crystalsCollectedLabel.text = "X " + crystalsCollected +"/"+ crystalsNeeded;
    }

    void ShakeScreen() {

        shaker.Shake();
    }
    void Win() {

        SceneManager.LoadScene("Menu");
    }

    void HandleDamage() {
        ShakeScreen();
        SoundManager.Instance.PlaySFX(hurtSFX);
        UpdateHealthUI();
    }
    void UpdateHealthUI() {

        portraits[currentPortrait].SetActive(false);
        currentPortrait = player.GetHealth();
        portraits[currentPortrait].SetActive(true);
        healthLabel.text = "X " + currentPortrait;

    }
    void StartHealthUI()
    {

        foreach (GameObject portrait in portraits)
        {
            portrait.SetActive(false);
        }
        currentPortrait = PlayerPrefs.GetInt("Health", 2) ;
        portraits[currentPortrait].SetActive(true);
        healthLabel.text = "X " + currentPortrait;

    }
    void HandleGameOver()
    {
        SoundManager.Instance.PlaySFX(loseSFX);
        GameObject.Instantiate(deadPlayer, player.transform.position, Quaternion.identity);
        player.gameObject.SetActive(false);

    }
}
