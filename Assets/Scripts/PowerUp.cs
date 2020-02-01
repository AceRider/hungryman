using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp : Eatable
{
    public event Action TurnGhostsBlue;

    // Start is called before the first frame update
    void Start()
    {
        points = 50;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TurnGhostsBlue?.Invoke();
            HandleObjectWasEaten();
        }
    }
}
