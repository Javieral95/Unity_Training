using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField, Range(10, 50)] private float speed = 30f;

    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        //_playerController = GameObject.Find("Nombre del objeto");
        //_playerController = GameObject.FindObjectOfType(typeof(PlayerController)); //Mas costoso
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerController.GameOver)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}