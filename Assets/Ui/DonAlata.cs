using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DonAlata : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject stopButton;
    [SerializeField] private TMP_Text welcomeText;

    private void Start()
    {
        // Set up hover listeners for buttons
        AddHoverListener(startButton, "START!");
        AddHoverListener(stopButton, "STOP!");
    }

    private void Update()
    {
        // Detect clicks anywhere else in the scene
        if (Input.GetMouseButtonDown(0))
        {
            welcomeText.text = "In Bathindale, there are no options.  There is ONLY Start and Stop. Choose Wisely";
        }
    }

    private void AddHoverListener(GameObject button, string hoverMessage)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entry.callback.AddListener((data) => { welcomeText.text = hoverMessage; });
        trigger.triggers.Add(entry);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
