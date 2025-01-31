using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Don't touch
    // Static instance property
    public static GameManager Instance { get; private set; }
    public GameObject gameObject;

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
         Time.timeScale =1;
    }
    #endregion

    //Events

    void Update()
    {
        if(PlayerStats.instance.Health<=0)
        {
            Time.timeScale = 0 ;
            gameObject.SetActive(true);
            UISystems.instance.FinalSoapPoints.text =(PlayerStats.instance.score*(Time.time/10)).ToString();
        }
    }

    public void Respawn()
    {
       // Get the name of the currently active scene and reload it
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        Time.timeScale =1;
    }
    public void Quit()
    {
         #if UNITY_EDITOR
        // If running in the Unity Editor, stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running in a build, quit the game
        Application.Quit();
        #endif
    }


}
