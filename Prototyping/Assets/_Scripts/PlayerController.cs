using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    //private float moveSpeed = 10f;
    //private float rotateSpeed = 180f;
    [SerializeField]
    private float force = 10f;
    private float horizontalInput, verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovePlayer();
        KeepPlayerInBounds();
    }

    private void MovePlayer()
    {
        //Fisica => AddForce sobre el RigidBody
        //          AddTorque => Rotar.
        // Influye el rozamiento (Drag & Angular Drag) del RigiBody, deberia ser mayor de 0.
        _rigidbody.AddForce(Vector3.forward * force * Time.deltaTime * verticalInput, ForceMode.Force);
        _rigidbody.AddTorque(Vector3.up * force * Time.deltaTime * horizontalInput, ForceMode.Force);
        //No Fisica => Metodo translate sobre el transform del objeto - Mover.
        //          Rotate sobre el transform - Rodar
    }

    private void KeepPlayerInBounds()
    {
        //Check Limits
        if (Mathf.Abs(transform.position.x) >= 10 || Mathf.Abs(transform.position.z) >= 10)
        {
            _rigidbody.velocity = Vector3.zero;
            if (transform.position.x > 10)
                transform.position = new Vector3(10, transform.position.y, transform.position.z);
            else if (transform.position.x < -10)
                transform.position = new Vector3(-10, transform.position.y, transform.position.z);
            else if (transform.position.z > 10)
                transform.position = new Vector3(transform.position.x, transform.position.y, 10);
            else if (transform.position.z < -10)
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
