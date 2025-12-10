using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint;
    public Transform player;
    // Add a default starting point (assign this in the Inspector to your actual start point in the scene)
    public Transform startPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Set the current checkpoint to the starting point when the game begins
        if (startPoint != null)
        {
            // We use the startPoint transform position as the initial safe point
            // We don't assign it as the CurrentCheckpoint GameObject reference yet unless it IS a real checkpoint object.
        }
        else
        {
            Debug.LogError("Assign the Start Point Transform in the Inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // (Your existing update code remains the same)
    }

    public void RespawnPlayer()
    {
        // Check if we have touched at least one checkpoint
        if (CurrentCheckpoint != null)
        {
            // If yes, respawn at the checkpoint's position
            FindObjectOfType<PlayerController>().transform.position = CurrentCheckpoint.transform.position;
            Debug.Log("Respawning at Checkpoint.");
        }
        else if (startPoint != null)
        {
            // If not, respawn at the designated starting position
            FindObjectOfType<PlayerController>().transform.position = startPoint.position;
            Debug.Log("Respawning at Start Point.");
        }
        else
        {
            // Handle the extreme case where neither is set
            Debug.LogError("Cannot respawn: Neither Checkpoint nor Start Point assigned.");
        }
    }
}

