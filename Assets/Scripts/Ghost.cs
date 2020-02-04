using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    GameObject ghost;

    [SerializeField]
    GhostMove ghostMoveScript;

    public event Action GhostCollidedWithPlayer;
    Vector3 initialPosition;

    void Start()
    {
        
        initialPosition = ghost.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            GhostCollidedWithPlayer?.Invoke();
        }
    }
    
    public void ResetGhost()
    {
        ghost.transform.position = initialPosition;
        ghostMoveScript.ResetGhostMovement();

    }

}
