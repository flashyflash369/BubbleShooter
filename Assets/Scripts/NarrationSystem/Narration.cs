using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Narration", menuName = "Narration", order = 0)]
public class Narration : ScriptableObject {

    // Properties
    public int StoryPoint;
    public GameObject baseUI;
    public List<string> narrationText;


    public float duration; 


}
