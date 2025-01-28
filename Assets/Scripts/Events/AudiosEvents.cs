using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudiosEvents : MonoBehaviour
{

    public static AudiosEvents Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        Instance = this;
    }


    public event EventHandler OnJumpPressed;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpPressed?.Invoke(this, EventArgs.Empty);
        }
    }

}
