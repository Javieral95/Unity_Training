using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30f;

    private float lowerBound = -10f;

    private float lateralBounds = 20;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.z > topBound || this.transform.position.x > lateralBounds || this.transform.position.x < -lateralBounds)
            Destroy(this.gameObject);
        else if (this.transform.position.z < lowerBound)
        {
            Debug.Log("GAME OVER :)");
            Destroy(this.gameObject);

            Time.timeScale = 0; //Se congela el tiempo
        }
    }
}