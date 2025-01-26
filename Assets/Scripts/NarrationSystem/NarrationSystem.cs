using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour {

    private Narration currentNarration;// Current active narration
    private int currentStoryPoint;
    private float narrationTimer;
    [SerializeField] private GameObject baseUI;
    private TMP_Text tmpText;
    private int currentTextIndex; // Index to track which text to display next


    public float typewriterSpeed = 0.05f; // Speed of the typewriter effect
    private Coroutine typewriterCoroutine; // Reference to the coroutine
    

    //List of Narrations
    public List<Narration> narrationList = new List<Narration>();

    //Start
    private void Start() {
        // Get the TMP_Text component from baseUI (or its children)
        tmpText = baseUI.GetComponentInChildren<TMP_Text>();

         if (tmpText != null)
        {
            // Set the text of the TMP_Text component
            tmpText.text = "Hello, world!";
        }
        else
        {
            Debug.LogWarning("TMP_Text component not found in baseUI or its children.");
        }
    }

   private void Update() {
        OnNextButtonClick();
   }

    //Event to send storypoint
    void OnEnable()
    {
        // Subscribe to the StoryPoint event
        EventSystem.OnStoryPointTriggered += HandleStoryPointTriggered;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        EventSystem.OnStoryPointTriggered -= HandleStoryPointTriggered;
    }


    //EventFunction 

    private void HandleStoryPointTriggered(int storyPointIndex)
{
    Debug.Log($"NarrationManager received story point {storyPointIndex}");
    currentStoryPoint = storyPointIndex;

    // Ensure the storyPointIndex is valid
    if (storyPointIndex >= 0 && storyPointIndex < narrationList.Count)
    {
        // Retrieve the Narration object and start it
        StartNarration(narrationList[storyPointIndex]);
    }
    else
    {
        Debug.LogWarning($"Invalid story point index: {storyPointIndex}");
    }
}



    // Trigger a story point using a switch
    void TriggerStoryPoint(int storyPoint)
    {
       
        StartNarrationFromList(storyPoint);

    }

    // Start narration by index from the list
    void StartNarrationFromList(int index)
    {
        if (index >= 0 && index < narrationList.Count)
        {
            StartNarration(narrationList[index]);
        }
        else
        {
            Debug.LogWarning($"Invalid narration index: {index}");
        }
    }

    // Start a narration
    void StartNarration(Narration narration)
    {
        // Set the current narration
        currentNarration = narration;
        currentTextIndex = 0; // Start at the first text

        // Show the UI
        baseUI.SetActive(true);

        // Display the first piece of text
        DisplayNextText();

    }

         // Display the next piece of text
        void DisplayNextText()
        {
        if (currentNarration != null && currentTextIndex < currentNarration.narrationText.Count)
        {
        // Stop any ongoing typewriter effect
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        // Start the typewriter effect for the current text
        typewriterCoroutine = StartCoroutine(TypewriterEffect(currentNarration.narrationText[currentTextIndex]));
        currentTextIndex++; // Move to the next text
         }
        else
        {
        // End the narration when all text has been displayed
        EndNarration();
        }
        }

    

     // Typewriter effect coroutine
    private IEnumerator TypewriterEffect(string text)
    {
        tmpText.text = ""; // Clear the text
        foreach (char c in text)
        {
            tmpText.text += c; // Add one character at a time
            yield return new WaitForSeconds(typewriterSpeed); // Wait for the specified speed
        }
    }

    // End the current narration
    void EndNarration()
    {
        Debug.Log("Narration ended.");
        currentNarration = null;
        baseUI.SetActive(false); // Hide the UI when narration ends
        EventSystem.OnStoryPointDestroy?.Invoke(currentStoryPoint);//destroy respective storypoint trigger
    }

    // You can also add a function to handle button clicks for continuing the narration
    public void OnNextButtonClick()
    {
         if (Input.GetMouseButtonDown(0)) // 0 = Left Mouse Button
        {
          DisplayNextText();
        }
    }
    
}