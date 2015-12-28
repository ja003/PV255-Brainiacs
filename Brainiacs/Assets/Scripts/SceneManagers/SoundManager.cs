﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    //public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    //public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    public float volume;


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    public void StartBackgroundMusic(AudioClip clip)
    {
        PlaySingle(clip, true);
    }

    public void PlaySingle(AudioClip clip)
    {
        PlaySingle(clip, false);
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip, bool loop)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        //efxSource.clip = clip;

        //Play the clip.
        //efxSource.Play();
        AudioSource[] AScomp = gameObject.GetComponents<AudioSource>();
        AudioSource availableAS = null;
        foreach(AudioSource audioS in AScomp)
        {
            if (!audioS.isPlaying)
            {
                availableAS = audioS;
                break;
            }
        }

        //assign random pitch to sound
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        if (availableAS != null)
        {
            if (loop)
            {
                availableAS.loop = true;
            }
            availableAS.clip = clip;
            availableAS.pitch = randomPitch;
            availableAS.volume = volume;
            availableAS.Play();
        }
        else
        {
            AudioSource newAS = gameObject.AddComponent<AudioSource>();
            if (loop)
            {
                newAS.loop = true;
            }
            newAS.clip = clip;
            newAS.pitch = randomPitch;
            newAS.volume = volume;
            newAS.Play();
        }
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        //float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        //efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        //efxSource.clip = clips[randomIndex];


        PlaySingle(clips[randomIndex]);
        //Play the clip.
        //efxSource.Play();

        //taky nahovno
        //efxSource.PlayOneShot(efxSource.clip);
    }
}