using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    [SerializeField, Tooltip("Objeto que seguirá la cámara")]
    private GameObject plane;

    [SerializeField, Tooltip("Offset de la camara, lo que se aleja del objeto.")]
    private Vector3 offset = new Vector3(0, 4, -8);

    private Vector3 defaultOffset = new Vector3();

    [SerializeField] private bool lockCamera = true;

    private float ctrlInput;
    private float mouseXInput;
    private float mouseYInput;

    private float sensibility = 4f;


    // Start is called before the first frame update
    void Start()
    {
        defaultOffset = new Vector3(offset.x, offset.y, offset.z);
    }

    // Update is called once per frame
    void Update()
    {
        ctrlInput = Input.GetAxis("Fire1");
        LockCamera(ctrlInput);
    }

    void LateUpdate()
    {
        this.transform.position = plane.transform.position + offset;

        if (!lockCamera) //Si la camara no esta bloqueada (vista desde el lado derecho como en el ejemplo) se permite rotar la camara con el raton.
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibility, Vector3.up) * offset;
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * (-1) * sensibility, Vector3.right) * offset; //-1 porque esta invertido
        }
        //Mirar al avion para seguirlo
        this.transform.LookAt(plane.transform);

    }

    private void LockCamera(float input)
    {
        if (input == 1) //El jugador presiona la tecla de cambiar la camara
        {
            if (!lockCamera)
                offset = defaultOffset; //Si la camara se bloquea, se vuelve a ver el avion desde un lado y no se mueve con el raton.
            lockCamera = !lockCamera;
        }
    }
}