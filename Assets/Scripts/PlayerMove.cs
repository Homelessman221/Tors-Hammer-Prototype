using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    private PlayerInput input;
    private Playerrecall playerrecall;
    private PlayerStates playerStates;
    private Rigidbody2D rb;

    private float playerScale = 0.07f;
    private float moveSpeed = 10;
    private float jumpStrength = 18;

    public bool isFacingRight = true;

    private float MaxDistanceForGroundCheck = 1;
    [SerializeField]private LayerMask isGroundLayer;

    private bool isGrounded;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        playerrecall = GetComponent<Playerrecall>();
        playerStates = GetComponent<PlayerStates>();
    }

    private void Update()
    {
        if(playerStates.PlayerState == 0)
        {
            Jump();
            IsGrounded();
        }
        
    }

    private void FixedUpdate()
    {
        if(playerStates.PlayerState == 0)
        {

        Move();
        }
    }

    private void Move()
    {
        Vector3 moveVector = new Vector3(input.MoveVector.x, 0f, input.MoveVector.y) * moveSpeed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

        if(input.MoveVector.x > 0f)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        }
        if (input.MoveVector.x < 0f)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-playerScale, playerScale, playerScale);
        }
    }
    private void Jump()
    {
        if (isGrounded && input.JumpValue)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
    }

    private void IsGrounded()
    {
        Vector3 direction1 = Vector3.down;
        Vector3 origin1 = transform.position + new Vector3(0, 0, 0);
        isGrounded = Physics2D.Raycast(origin1, direction1, MaxDistanceForGroundCheck, isGroundLayer);
        Debug.DrawRay(origin1, direction1 * MaxDistanceForGroundCheck, Color.red);
    }
}
