using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private string[] dialogueSentences;
    private int index = 0;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;
    public Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    // Public method to start the dialogue from another script (like NPCDialogue)
    public void StartDialogue(string[] sentences)
    {
        this.dialogueSentences = sentences;
        index = 0;
        // Start the typing coroutine
        StartCoroutine(TypeDialogue());
    }
    
    public IEnumerator TypeDialogue()
    {
        dialogueBox.SetActive(true);
        // Freeze the player's position during dialogue
        if (playerRB != null)
        {
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        textDisplay.text = ""; // Clear text before starting
        foreach (char letter in dialogueSentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        // Once typing is done, show the continue button
        if(textDisplay.text == dialogueSentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NextSentences()
    {
        Debug.Log("Inside NextSentence");
        continueButton.SetActive(false);
        
        if (index < dialogueSentences.Length - 1)
        {
            // Move to the next sentence
            index++;
            StartCoroutine(TypeDialogue());
        }
        else
        {
            // End of dialogue
            textDisplay.text = "";
            dialogueBox.SetActive(false);
            this.dialogueSentences = null;
            index = 0;

            // Unfreeze the player to allow movement
            if (playerRB != null)
            {
                playerRB.constraints = RigidbodyConstraints2D.None;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


