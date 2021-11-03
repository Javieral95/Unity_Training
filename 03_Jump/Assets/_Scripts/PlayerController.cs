using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField, Range(0, 20)] private float jumpForce;
    [SerializeField, Range(0, 1)] private float gravityMultiplier = 1;

    private bool isOnGround = true;
    private bool _gameOver = false;

    public ParticleSystem explosion;
    public ParticleSystem dirtSplatter;

    [SerializeField, Range(0, 1)] private float jumpFXVolume = 1, crashFXVolume = 1;
    public AudioClip jumpSound, crashSound;

    private AudioSource _audioSource;
    //Animator
    private Animator _animator;

    private const string SPEED_F = "Speed_f";

    private float speed_f = 1.0f;

    private const string JUMP_TRIG = "Jump_trig";
    private const string DEATH_B = "Death_b";
    private bool Death_b;
    private const string DEATHTYPE_INT = "DeathType_int";
    private int DeathType_int;
    //
    public bool GameOver
    {
        get => _gameOver; //Es como return _gameOver
        //set => _gameOver = value;
    }
    
    //Seria mas natural que estuviese en GameManager

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //Para comunicar diferentes componentes entre ellas.
        Physics.gravity *= gravityMultiplier; //Cambiar el valor de la gravedad

        _audioSource = GetComponent<AudioSource>();
        
        _animator = GetComponent<Animator>();
        _animator.SetFloat(SPEED_F, speed_f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0, this.transform.position.y, 0);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !_gameOver)
        {
            //F = m * a
            _audioSource.PlayOneShot(jumpSound, jumpFXVolume);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            dirtSplatter.Stop();
            isOnGround = false;
            _animator.SetTrigger(JUMP_TRIG);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !_gameOver)
        {
            isOnGround = true;
            dirtSplatter.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            _animator.SetInteger(DEATHTYPE_INT, Random.Range(1,3));
            _animator.SetBool(DEATH_B, true);
            _audioSource.PlayOneShot(crashSound, crashFXVolume);
            dirtSplatter.Stop();
            explosion.Play();
            _gameOver = true;
            Debug.Log("Game Over :( ");
            
            Invoke("RestartGame", 3.0f); //Llama al metodo despues de 1 segundo.
        }
    }

    private void RestartGame()
    {
        _gameOver = false; 
        SceneManager.UnloadSceneAsync("Prototype 3"); //Tambien habria que resetear variables que se hubiesen cambiado a lo largo del juego
        SceneManager.LoadScene("Prototype 3");
    }
}