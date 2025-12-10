using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABShooting : MonoBehaviour
{
    [SerializeField] private KeyCode shootKey = KeyCode.Return;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    public SpriteRenderer sr; 

    [Header("Level 1 Control")]
    public bool shootingEnabled = false; // Set this to FALSE initially in the Inspector

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        if(shootingEnabled && Input.GetKeyDown(shootKey))
        {
            Shooting();
        }
    }
    
    public void Shooting()
    {
        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }
    
    public void EnableShooting()
    {
        shootingEnabled = true;
        Debug.Log("Shooting Ability Enabled!");
    }
}


