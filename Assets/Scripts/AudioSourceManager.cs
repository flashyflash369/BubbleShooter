using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioSourceManager : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField] private AudioClip shootAudio;
    [SerializeField] private AudioClip jumpAudio;



    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void OnEnable(){
        EventSystem.Playshoot+=PlayShootingFx;
        //Event for jump
        AudiosEvents.Instance.OnJumpPressed += PlayJumpFx;
    }
    void OnDisable(){
        EventSystem.Playshoot-=PlayShootingFx;
    }


    void PlayShootingFx()
    {
        audioSource.PlayOneShot(shootAudio);
    }


        //Events triggered on jump
    private void TriggerJumpSFX_OnJumpPressed(object sender, EventArgs e)
    {
        audioSource.PlayOneShot(jumpAudio);
    }

    private void PlayJumpFx(object sender, EventArgs e)
    {
        audioSource.PlayOneShot(jumpAudio);
    }


}
