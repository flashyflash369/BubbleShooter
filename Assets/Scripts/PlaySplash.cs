using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySplash : MonoBehaviour
{
   

    [SerializeField] private AudioClip shootAudio;

    [SerializeField] List<ParticleSystem> vfx = new List<ParticleSystem>();
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < vfx.Count; i++)
            {
                vfx[i].Play();
            }
        }
    }


}
