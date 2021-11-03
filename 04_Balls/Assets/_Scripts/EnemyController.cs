using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float moveForce;
    private Vector3 direction;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Se normaliza para que la fuerza tenga magnitud 1 (Up, forward... etc ya estan normalizados)
        //Si no, cuando mas lejos este, mayor sera el vector y con mas fuerza se movera
        direction = (player.transform.position - this.transform.position).normalized; //Destino - Origen
        _rigidbody.AddForce(direction * moveForce, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("KillZone"))
            Destroy(this.gameObject);
    }
}
