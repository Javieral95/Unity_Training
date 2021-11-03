using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money") || other.CompareTag("Bomb"))
            Destroy(other.gameObject);
    }
}
