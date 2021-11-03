using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsX : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        //Destroy(other.gameObject); //El perro no se muere al coger una pelota.
    }
}
