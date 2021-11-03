using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject focalPoint;
    private Rigidbody _rigidbody;
    [SerializeField] private float moveForce = 10f;
    
    private float verticalInput;

    private bool hasPowerUp;
    private float powerUpDuration = 7;
    [SerializeField] private float powerUpForce = 45f;
    public List<GameObject> powerUpIndicators;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //focalPoint = GameObject.Find("FocalPoint"); //Si fuese Privado
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        //Vector3.Forward seria para alante en el sistema de referencia de Unity, solo se moveria en una direccion.
        //Habria que mvoerse en torno al punto focal.
        //_rigidbody.AddForce(Vector3.forward * moveForce * verticalInput); 
        _rigidbody.AddForce(focalPoint.transform.forward * moveForce * verticalInput);

        foreach (var indicator in powerUpIndicators)
            indicator.transform.position = this.transform.position + 0.5f * Vector3.down;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            //Arrancamos corutina
            StartCoroutine(PowerUpCountdown());
        }
        else if (other.CompareTag("KillZone"))
        {
            SceneManager.UnloadSceneAsync("Prototype 4");
            SceneManager.LoadScene("Prototype 4");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerUp)
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayDirection = collision.gameObject.transform.position - this.transform.position; //No se normaliza porque la direccion siempre es la misma (colision)
                enemyRigidbody.AddForce(awayDirection * powerUpForce, ForceMode.Impulse);
            }
    }

    //Co-Rutina. IEnumerator = Se gobierna ella sola.
    IEnumerator PowerUpCountdown()
    {
        powerUpIndicators[0].gameObject.SetActive(true);
        
        var timeToWait = powerUpDuration / powerUpIndicators.Count;

        foreach (var indicator in powerUpIndicators)
        {
            indicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeToWait); //Espera unos segundos y luego sigue.
            indicator.gameObject.SetActive(false);
        }
        hasPowerUp = false;
        
        //yield return --> Acaba la CoRutina.
    }
}