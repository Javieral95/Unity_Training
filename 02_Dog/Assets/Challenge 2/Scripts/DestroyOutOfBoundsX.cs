﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBoundsX : MonoBehaviour
{
    private float leftLimit = -35;
    private float bottomLimit = 0;

    // Update is called once per frame
    void Update()
    {
        // Destroy dogs if x position less than left limit
        if (transform.position.x < leftLimit)
            Destroy(gameObject);
        
        // Destroy (and GameOver) balls if y position is less than bottomLimit
        else if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
            Debug.Log("GAME OVER!!!");
            Time.timeScale = 0;
        }


    }
}
