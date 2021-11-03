using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //Arrastrable como componente
{
    //Evitar que las variables sean publicas, para hacer privada y que salga poner el atributo [SerializeField]

    //[HideInInspector] //Para no verlo en Unity
    [Range(0, 20), SerializeField, Tooltip("Velocidad lineal máxima del coche en m/s")]
    private float speed = 15.0f;

    [Range(0, 50), SerializeField, Tooltip("Velocidad de gir1o en grados")]
    private float turnSpeed = 40;

    private float horizontalInput, verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        //Mirar Unity Manual Order of Execution
        Debug.Log("Start del " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        // Move vehicule
        //this.transform.Translate(Vector3.forward); //0,0,1

        //Hacia adelante
        // S = s0 + V*t*(direccion)  --> (Espacio igual a espacio anterior + velocidad por tiempo)
        this.transform.Translate(speed * Time.deltaTime * Vector3.forward *
                                 verticalInput); //Aplicada ecuacion anterior con velocidad 20m/s

        //Giro
        if (verticalInput > 0)
            this.transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up * horizontalInput); //Cambia el angulo
        else if (verticalInput < 0)
            this.transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up *
                                  (horizontalInput * -1)); //Cambia el angulo marcha atras
    }
}