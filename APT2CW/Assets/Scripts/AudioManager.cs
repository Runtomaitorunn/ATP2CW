using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;



    public void PlayAudioByName(string name)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            foreach (AudioClip clip in audioClips)
            {
                if (clip.name == name)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    return;
                }
                else
                    Debug.Log("can't find the audio clip!!!!");
            }
        }
        else
            Debug.Log("can't find the audio source!!");
        
    }

}
