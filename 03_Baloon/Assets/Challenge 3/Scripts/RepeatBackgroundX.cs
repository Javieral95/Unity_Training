using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private float _backgroundHeight;
    public float BackgroundHeight
    {
        get => _backgroundHeight; //Es como return _backgroundHeight
    }

    private float backgroundHeightOffset = 3f;

    private void Start()
    {
        var tmpSize = GetComponent<BoxCollider>().size;
        startPos = transform.position; // Establish the default starting position 
        repeatWidth = tmpSize.x / 2; // Set repeat width to half of the background
        _backgroundHeight = tmpSize.y - backgroundHeightOffset;
    }

    private void Update()
    {
        // If background moves left by its repeat width, move it back to start position
        if (startPos.x - transform.position.x > repeatWidth)
            transform.position = startPos;
        
    }

 
}


