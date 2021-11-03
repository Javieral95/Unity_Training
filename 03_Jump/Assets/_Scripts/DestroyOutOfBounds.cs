using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < 0)
            Destroy(this.gameObject);
    }
}
