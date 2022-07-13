using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _audio;
    public AudioSource _liquidaudio;

    public AudioClip _chooseFlask;
    public AudioClip _flightFlask;

    public AudioClip _applasure;
    public AudioClip _congr;
    public AudioClip _confetti;

    void Start()
    {
    }

    public void PlayLiquidSound()
    {
        _liquidaudio.Play();
    }
    public void StopLiquidSound()
    {
        _liquidaudio.Stop();
    }

    public void PlayChooseFlaskSound()
    {
        _liquidaudio.PlayOneShot(_chooseFlask);
    }

    public void PlayFlightFlaskSound()
    {
        _liquidaudio.PlayOneShot(_flightFlask);
    }

    public void PlayWinSound()
    {
        _audio.PlayOneShot(_applasure);
        _audio.PlayOneShot(_confetti);
        _audio.PlayOneShot(_congr);
    }
}
