using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 60f, translationSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left * translationSpeed * Time.deltaTime);
        transform.localPosition += Vector3.left * translationSpeed * Time.deltaTime; //OJO si se hace igual las direcciones relativas cambian
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }
}