using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Eatable
{
    [SerializeField]
    private BoxCollider2D collider;

    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    void Start()
    {
        collider.enabled = false;

        if(GameState.level == 0)
        {
            points = 100;
        }
        else if(GameState.level == 1)
        {
            points = 500;
        }
        else
        {
            points = 1000;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            HandleObjectWasEaten();
        }
    }

    public void SetPoints()
    {
        if(GameState.level == 0)
        {
            points = 100;
        }
        else if(GameState.level == 1)
        {
            points = 500;
        }
        else
        {
            points = 1000;
        }
    }

    public void ResetFruit()
    {
        text.gameObject.SetActive(false);
        collider.enabled = false;
        SetPoints();
    }
}
