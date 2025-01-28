using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Don't touch
    // Static instance property
    public static GameManager Instance { get; private set; }

    // Awake method to enforce Singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign this instance
            DontDestroyOnLoad(gameObject); // Optional: Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    #endregion

    //Events



}
