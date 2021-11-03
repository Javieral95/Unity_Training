using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //Collider es con quien se ha chocado. Collision contiene AMBOS objetos y mas.
    {
        if (other.CompareTag("Projectile")) //Solo si es una bala
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
