using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeAnimationController : MonoBehaviour
{
    private Animator _animator;
    private const string MOVE_HANDS = "MoveHands";
    private const string MOVE_X = "Move_X";
    private const string MOVE_Y = "Move_Y";
    private const string IS_MOVING = "isMoving";
    
    private bool isMovingHands = false;
    private float moveX = 0f;
    private float moveY = 0f;
    private bool isMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(MOVE_HANDS, isMovingHands); //Pasa el valor del codigo al animator (alinea codigo y animador)
        _animator.SetFloat(MOVE_X, moveX);
        _animator.SetFloat(MOVE_Y, moveY);
        _animator.SetBool(IS_MOVING, isMoving);
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Mathf.Sqrt(moveX * moveX + moveY * moveY) >= 0.02)             //Umbral de movimiento
        {
            _animator.SetBool(IS_MOVING, true);
            _animator.SetFloat(MOVE_X, moveX);
            _animator.SetFloat(MOVE_Y, moveY);
        }
        else
            _animator.SetBool(IS_MOVING, false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMovingHands = !isMovingHands;
            _animator.SetBool(MOVE_HANDS, isMovingHands);
        }
    }
}
