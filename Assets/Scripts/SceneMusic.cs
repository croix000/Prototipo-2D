using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] AudioClip music;
    void Start()
    {
        if (SoundManager.Instance.player.clip != music)
        {
            SoundManager.Instance.LoadMusic(music);
            SoundManager.Instance.PlayMusic();
        }
    }

}
