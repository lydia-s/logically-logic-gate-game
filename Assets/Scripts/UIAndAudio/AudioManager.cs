using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip dropGateClip;
    public AudioClip clearRowClip;
    public AudioSource musicSource;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void PlayDropSound()
    {
        musicSource.clip = dropGateClip;
        musicSource.Play();
    }
    public void PlayClearRowSound()
    {
        musicSource.clip = clearRowClip;
        musicSource.Play();

    }




}
