using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutOfBounds : MonoBehaviour
{
    private float upperLimit = 14f;

    private float lowerLimit = -1f;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        if (position.y > upperLimit)
        {
            transform.position = new Vector3(position.x, upperLimit, position.z);
            //Cancel Force
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
        else if (position.y < lowerLimit)
            transform.position = new Vector3(position.x, lowerLimit, position.z);
    }
}