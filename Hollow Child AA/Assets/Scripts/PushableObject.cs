using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PushableObject : MonoBehaviour
{
    private FixedJoint2D joint; 

    public void ConnectToPlayer(Rigidbody2D playerRB)
    {
        joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = playerRB;
        
        // Manual anchor setup for robust connection
        joint.autoConfigureConnectedAnchor = false; 
        joint.anchor = Vector2.zero; 
        
        // Calculate the direction from the object to the player
        Vector2 directionToPlayer = (playerRB.transform.position - transform.position).normalized;
        joint.connectedAnchor = new Vector2(directionToPlayer.x * 0.5f, 0f); 
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.WakeUp(); 
        }
        Debug.Log("Object grabbed!");
    }

    public void DisconnectFromPlayer()
    {
        if (joint != null)
        {
            Destroy(joint);
            Debug.Log("Object released!");
        }
    }
}





