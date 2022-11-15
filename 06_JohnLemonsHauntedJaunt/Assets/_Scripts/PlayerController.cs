using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    [SerializeField, Range(30f, 180)]
    private float turnSpeed = 30;
    private Quaternion rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate() //----- Fixed Update! Porque es fisicas!
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(0f, horizontal); //Sensibilidad del Input, queremos ver si es parecido a 0.
        bool hasVerticalInput = !Mathf.Approximately(0f, vertical);
        //Movimiento esta en la animacion

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        if (isWalking)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else
            _audioSource.Stop();


        _animator.SetBool("IsWalking", isWalking);

        //Suavizar el giro (Ojo! Time.FixedDeltaTime)
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f); //Cuando se sabe el destino, Lerp interpola y Slerp interpola en esfera
        rotation = Quaternion.LookRotation(desiredForward); //Direccion 3d a rotation a la que quiero mirar
    }

    private void OnAnimatorMove()
    {
        //Apply Root Motion = Handle by Script
        //S = S0 + v*t
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
