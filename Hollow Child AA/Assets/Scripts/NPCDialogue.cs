using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    // Make sure this is assigned in the Unity Inspector
    public DialogueManager dialogueManager; 

    void OnTriggerEnter2D(Collider2D other) {
        // Use lowercase 'tag' property
        if(other.tag == "Player"){ 
            string[] dialogue = {
                "Liora: Dont look at the darkness… look at me iam here, just follow the light", 
                "Hollow Child:  Why…is it watching me?????"
            };
            
            // Start the dialogue sequence using the manager's public method
            if (dialogueManager != null)
            {
                dialogueManager.StartDialogue(dialogue);
                
                // >>> LINE REMOVED: No longer destroying the collider <<<
                // Destroy(GetComponent<Collider2D>(), 5f); 
            }
            else
            {
                Debug.LogError("DialogueManager reference is missing in the Inspector!");
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Optional fallback to find the object in the scene if not assigned in Inspector
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


