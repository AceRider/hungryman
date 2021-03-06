﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dot : Eatable
{
    public event Action RemoveDot;

    // Start is called before the first frame update
    void Start()
    {
        points = 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            RemoveDot?.Invoke();
            HandleObjectWasEaten();
        }
    }
}
