using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private GameManager gameManager;

    [SerializeField] private float MinForceValue = 10f;
    [SerializeField] private float MaxForceValue = 14f;

    [SerializeField] private float TorqueForceLimit = 10;

    private float BackgroundBottomLimit, BackgroundLeftLimit, BackgroundRightimit = 0f;
    private float SpawnBottomOffset = 5f;

    [SerializeField, Range(-100, 100)] private int PointsValue = 10;
    [SerializeField, Range(-100, 0)] private int NoHitPoints = -10;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        //Init 
        gameManager =
            FindObjectOfType<GameManager>(); //Busca el primer con el behaviour GameManager, es costoso, por eso usar solo en el Start

        var tmpBackground = GameObject.Find("Background");
        var backgroundPosition = tmpBackground.transform.position;
        var backgroundSize = tmpBackground.GetComponent<BoxCollider>().size;

        BackgroundBottomLimit =
            backgroundPosition.y - (backgroundSize.y / 2); //Realmente es 0, pero asi es mas dinamico.
        BackgroundLeftLimit = (backgroundPosition.x - (backgroundSize.x / 2)) + 1; //+1 y -1 para evitar los limites
        BackgroundRightimit = (backgroundPosition.x + (backgroundSize.x / 2)) - 1;

        //RigidBody and Forces
        _rigidbody = GetComponent<Rigidbody>();
        ApplySpawnForces();
    }


    /// <summary>
    /// Modifica la posicion del objeto y aplica tanto fuerza como torsion.
    /// </summary>
    void ApplySpawnForces()
    {
        this.transform.position = RandomSpawnPosition();

        //IMPULSE
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(),
            ForceMode.Impulse); //Para que gire (Fuerza angular, rotacion)
    }

    /// <summary>
    /// Fuerza aleatoria hacia arriba, utilizando MinForceValue y MaxForceValue
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(MinForceValue, MaxForceValue);
    }

    /// <summary>
    /// Genera un valor aleatorio entre -TorqueForceLimit y TorqueForceLimit
    /// </summary>
    /// <returns></returns>
    private float RandomTorque()
    {
        return Random.Range(-TorqueForceLimit, TorqueForceLimit);
    }

    /// <summary>
    /// Devuelve una posicion aleatoria para spawnear el objeto (X e y, Z=0)
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(BackgroundLeftLimit, BackgroundRightimit),
            BackgroundBottomLimit - SpawnBottomOffset);
    }

    //Mouse Events
    private void OnMouseOver()
    {
        if (gameManager.GameMode != GameMode.click)
            DestroyTarget();
    }

    private void OnMouseDown()
    {
        if (gameManager.GameMode == GameMode.click)
            DestroyTarget();
    }

//
    private void DestroyTarget()
    {
        if (gameManager.gameState == GameState.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            ChangeScore(PointsValue);
        }
    }

    //Collider events
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killzone"))
        {
            Destroy(gameObject);
            if (!this.gameObject.CompareTag("BadTarget") && gameManager.gameState == GameState.inGame)
            {
                if (gameManager.GameMode != GameMode.infinite && gameManager.GameMode != GameMode.time)
                    gameManager.GameOver(); //Igual esto cambia
                else
                    ChangeScore(NoHitPoints);
            }
        }
    }

//
    private void ChangeScore(int points)
    {
        gameManager.UpdateScore(points);
        //if (this.CompareTag("BadTarget"))
        //    gameManager.GameOver(); //Igual esto cambia
    }
}