using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public GameObject pauseFirstButton;
    public GameObject optionsButton;
    public GameObject optionsFirstButton;

    public Image sfxButtonImage;
    public Sprite sfxOn;
    public Sprite sfxOff;


    public Image musicButtonImage;
    public Sprite musicOn;
    public Sprite musicOff;


    public static PauseMenu Instance;
    public bool isPaused;

    public Slider volumeSlider;

    string saveData;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = SoundManager.Instance.volume;

        if (SoundManager.Instance.sfxOn)
        {
            sfxButtonImage.sprite = sfxOn;
        }
        else
        {
            sfxButtonImage.sprite = sfxOff;
        }


        if (SoundManager.Instance.musicOn)
        {
            musicButtonImage.sprite = musicOn;
        }
        else
        {
            musicButtonImage.sprite = musicOff;
        }

        volumeSlider.onValueChanged.AddListener(delegate {
            ValueChangeCheck();
        });

    }



    public void Quit() {

        Application.Quit();

    }
    public void SFX()
    {
        SoundManager.Instance.toggleSFX();

        if (SoundManager.Instance.sfxOn)
        {
            sfxButtonImage.sprite = sfxOn;
        }
        else
        {
            sfxButtonImage.sprite = sfxOff;
        }

    }
    public void Music()
    {
        SoundManager.Instance.toggleMusic();
        if (SoundManager.Instance.musicOn)
        {
            musicButtonImage.sprite = musicOn;
        }
        else
        {
            musicButtonImage.sprite = musicOff;
        }

    }



	    // Invoked when the value of the slider changes.
	    public void ValueChangeCheck()
    {
        SoundManager.Instance.setVolume(volumeSlider.value);
    }
   
}
