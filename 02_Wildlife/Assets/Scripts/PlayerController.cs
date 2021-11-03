using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Velocidad lineal máxima del objeto en m/s")]
    private float speed = 5.0f;

    private float horizontalInput;
    
    [SerializeField, Tooltip("Rango de los limites del jugador en el eje X")]
    private int xRange = 15;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento personaje
        horizontalInput = Input.GetAxis("Horizontal");
        this.transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        
        if(this.transform.position.x < -xRange)
            this.transform.position = new Vector3(-xRange, this.transform.position.y, this.transform.position.z);
        else if(this.transform.position.x > xRange)
            this.transform.position = new Vector3(xRange, this.transform.position.y, this.transform.position.z);
        
        //Acciones personaje
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Lanzar proyectil: Crear (y este se mueve gracias al script que ya tienen de moverse hacia adelante)
            Instantiate(projectilePrefab, transform.position, this.transform.rotation);

        }
    }
}