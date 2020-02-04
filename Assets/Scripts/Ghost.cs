using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class Ghost : Eatable
{
    [SerializeField]
    GameObject ghost;

    [SerializeField]
    GhostMove ghostMoveScript;

    public event Action GhostCollidedWithPlayer;
    Vector3 initialPosition;

    [SerializeField]
    Sprite ghostSprite;

    [SerializeField]
    Sprite blueGhostSprite;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    BoxCollider2D boxCollider2D;

    private bool isGhostBlue = false;

    void Start()
    {
        points = 0;
        initialPosition = ghost.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            if (isGhostBlue)
            {
                points = (int)Math.Pow(2, GameState.consecutiveGhostsEaten + 1) * 100;
                GameState.consecutiveGhostsEaten++;
                HandleObjectWasEaten();
                ghostMoveScript.enabled = false;
                StartCoroutine(HandleAftermathOfGhostEaten());
            }
            else
            {
                GhostCollidedWithPlayer?.Invoke();
            }
        }
    }

    IEnumerator HandleAftermathOfGhostEaten()
    {
        int timer = 0;
        while (timer < 1)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }
        spriteRenderer.enabled = true;
        ghostMoveScript.enabled = true;
        boxCollider2D.enabled = true;

        ResetGhost();
    }

    public void ResetGhost()
    {
        ghost.transform.position = initialPosition;
        ghostMoveScript.ResetGhostMovement();
        TurnGhostRegularColor();
    }

    public void TurnGhostBlue()
    {
        spriteRenderer.sprite = blueGhostSprite;
        isGhostBlue = true;
    }

    public void TurnGhostRegularColor()
    {
        spriteRenderer.sprite = ghostSprite;
        isGhostBlue = false;
    }
}
