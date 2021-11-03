using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField, Tooltip("Las helices del avion, que giraran al moverse")]
    private GameObject propeller;
    
    [Range(0, 5), SerializeField, Tooltip("Velocidad lineal máxima del avion en m/s")]
    private float maxSpeed;
    [Range(0, 100), SerializeField, Tooltip("Velocidad de giro en grados")]
    private float rotationSpeed;
    [Range(0, 8000), SerializeField, Tooltip("Velocidad de giro en grados, indiferente para el movimiento")]
    private float rotationPropellerSpeed;
    
    private float horizontalInput, verticalInput, speedInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical and horizontal input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        speedInput = Input.GetAxis("Jump"); //Añadido en el espacio la tecla para avanzar

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * maxSpeed * speedInput);
        
        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);
        
        // tilt the plane right/left based on right/left arrow keys
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * horizontalInput);

        if (speedInput != 0) //Si el avion se mueve, rotan las helices
            propeller.transform.Rotate(Vector3.forward * rotationPropellerSpeed * Time.deltaTime * speedInput);
    }
}
