using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 2;
    private GameObject focalPoint;

    public bool hasPowerup;
    public List<GameObject> powerupIndicators;
    public int powerUpDuration = 5;

    private float normalStrength = 15; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    private float impulseStrength = 35; //How hard to hit enemy with impulse power (space key)

    private bool hasImpulse = true;
    private float impulseCooldownTime = 10;
    public GameObject impulseIndicator;
    public ParticleSystem impulseParticle;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed, ForceMode.Force); 

        // Set powerup indicator position to beneath player
        foreach (var indicator in powerupIndicators)
            indicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        impulseIndicator.transform.position = transform.position + new Vector3(0, -0f, 0);

        if (Input.GetKeyDown(KeyCode.Space) && hasImpulse)
            DoImpulse();
    }

    private void DoImpulse()
    {
        impulseParticle.Play();
        playerRb.AddForce(focalPoint.transform.forward * impulseStrength, ForceMode.Impulse); 
        StartCoroutine(ImpulseCooldown());
    }
    
    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        var countOfPowerUpsIndicators = powerupIndicators.Count;
        foreach (var indicator in powerupIndicators)
        {
            indicator.SetActive(true);
            yield return new WaitForSeconds(powerUpDuration / countOfPowerUpsIndicators);
            indicator.SetActive(false);
        }

        hasPowerup = false;

    }
    
    // Coroutine to count down powerup duration
    IEnumerator ImpulseCooldown()
    {
        hasImpulse = false;
        impulseIndicator.SetActive(false);
        yield return new WaitForSeconds(impulseCooldownTime);
        
        impulseIndicator.SetActive(true);
        hasImpulse = true;

    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
