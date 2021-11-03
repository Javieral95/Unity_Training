using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField, Tooltip("Velocidad lineal máxima al lanzar el objeto en m/s")]
    private float speed = 5.0f;
    

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
