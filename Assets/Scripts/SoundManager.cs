using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    public static SoundManager soundManager;

    public AudioSource backgroundAudioSource;
    public AudioClip[] backgroundClips;

    public AudioSource gameAudioSource;
    public AudioClip katanaSound;
    public AudioClip shurikenSound;
    public AudioClip shinobiDeadSound;
    public AudioClip enemyDeadSound;
    public AudioClip explosionSound;
    public AudioClip katanaHitSound;
    public AudioClip clickSound;
    public AudioClip gameOverSoundtrack;
    
    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        AudioClip backgroundClip = backgroundClips[Random.Range(0, backgroundClips.Length)];

        float startTimeOffset = 1f;
        float endTimeOffset = 20f;
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.time = startTimeOffset + Random.Range(0, backgroundClip.length - endTimeOffset);
        backgroundAudioSource.Play();
    }

    public void PlayKatanaSound()
    {
        gameAudioSource.clip = katanaSound;
        gameAudioSource.Play();
    }
    
    public void PlayShurikenSound()
    {
        gameAudioSource.clip = shurikenSound;
        gameAudioSource.volume = 0.1f;
        gameAudioSource.Play();
    }
    
    public void PlayShinobiDeadSound()
    {
        gameAudioSource.clip = shinobiDeadSound;
        gameAudioSource.Play();
    }
    
    public void PlayExplosionSound()
    {
        gameAudioSource.clip = explosionSound;
        gameAudioSource.Play();
    }
    
    public void PlayKatanaHitSound()
    {
        gameAudioSource.clip = katanaHitSound;
        gameAudioSource.volume = 0.7f;
        gameAudioSource.Play();
    }

    public void PlayClickSound()
    {
        gameAudioSource.clip = clickSound;
        gameAudioSource.Play();
    }

    public void PlayGameOverSoundtrack()
    {
        backgroundAudioSource.clip = gameOverSoundtrack;
        backgroundAudioSource.time = 1f;
        backgroundAudioSource.Play();
    }
}
