﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public bool sfxOn;
    public bool musicOn;
    public float volume;

    public AudioSource player;


    [SerializeField] AudioClip[] gameOST;
    public int playing;
    void Awake()
    {

        if (Instance == null)
        {

            if (!parseIntToBool(PlayerPrefs.GetInt("sfx", 1)))
            {
                sfxOn = false;
            }
            else
            {
                sfxOn = true;
            }

            if (!parseIntToBool(PlayerPrefs.GetInt("music", 1)))
            {
                musicOn = false;
            }
            else
            {
                musicOn = true;
            }


            volume = PlayerPrefs.GetFloat("volume", 1);

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            player = GetComponent<AudioSource>();
            //Rest of your Awake code

        }
        else
        {
            Destroy(this);
        }
    }


    public void toggleSFX()
    {

        sfxOn = !sfxOn;
        PlayerPrefs.SetInt("sfx", parseBoolToInt(sfxOn));
    }

    public void toggleMusic()
    {

        musicOn = !musicOn;
        PlayMusic();
        PlayerPrefs.SetInt("music", parseBoolToInt(musicOn));
    }

    public bool parseIntToBool(int number)
    {

        return number == 1;

    }
    public int parseBoolToInt(bool condition)
    {
        if (condition)
        {
            return 1;
        }
        else
            return 0;

    }


    public void setVolume(float vol)
    {

        volume = vol;
        PlayerPrefs.SetFloat("volume", volume);

        player.volume = volume;
    }


    public void PlayMusic()
    {

        if (musicOn)
        {
            player.Play();
        }
        else
        {

            player.Stop();
        }

    }

    public void PlayGameMusic(int lvlIndex)
    {



        player.volume = volume;
        player.Stop();

        player.clip = gameOST[lvlIndex];
        playing = lvlIndex;
        if (musicOn)
        {
            player.Play();
        }


    }


    public void LoadMusic(AudioClip clip)
    {


        player.volume = volume;
        player.Stop();
        player.clip = clip;
        playing = 0;
    }
    public void PlaySFX(AudioSource clip)
    {

        if (sfxOn)
            clip.Play();

    }

}


