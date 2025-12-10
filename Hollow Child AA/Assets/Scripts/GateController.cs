using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject largeGate;
    public LightPuzzleReceiver[] allReceivers;
    public void CheckPuzzleStatus()
    {
        foreach (var receiver in allReceivers)
        {
            if (!receiver.isPowered) return;
        }
        largeGate.SetActive(false); // Unlock/Open the gate
    }
}

