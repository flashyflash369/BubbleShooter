using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField] private AudioClip shootAudio;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void OnEnable(){
        EventSystem.Playshoot+=PlayShootingFx;
    }
    void OnDisable(){
        EventSystem.Playshoot-=PlayShootingFx;
    }


    void PlayShootingFx()
    {
        audioSource.PlayOneShot(shootAudio);
    }


}
