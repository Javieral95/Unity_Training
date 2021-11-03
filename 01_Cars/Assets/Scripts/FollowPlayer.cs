using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("Objeto que seguirá la cámara")]
    private GameObject player; //PLAYER_CAR

    [SerializeField, Tooltip("Offset de la camara, lo que se aleja del objeto.")]
    private Vector3 offset = new Vector3(0, 4, -6);
        
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + offset;
    }
}
