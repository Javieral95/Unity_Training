using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;

    //private BoxCollider backgroundCollider;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        //backgroundCollider = GetComponent<BoxCollider>();
        repeatWidth = (GetComponent<BoxCollider>().size.x)/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos.x - transform.position.x >= repeatWidth)
            this.transform.position = startPos;
    }
}
