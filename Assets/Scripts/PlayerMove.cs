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

    [SerializeField]public IntVariable isFacingRight;

    private float MaxDistanceForGroundCheck = 1;
    [SerializeField]private LayerMask isGroundLayer;

    [SerializeField] private IntVariable isGrounded;
    [SerializeField] private Vector2Variable moveVector;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        playerrecall = GetComponent<Playerrecall>();
        playerStates = GetComponent<PlayerStates>();
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

        rb.velocity = new Vector2(moveVector.Value.x * moveSpeed, rb.velocity.y);

        if(isFacingRight.Value == 1)
        {
            transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        }
        if (isFacingRight.Value == 0)
        {
            transform.localScale = new Vector3(-playerScale, playerScale, playerScale);
        }
    }
    public void Jump()
    {
        if (isGrounded.Value == 1 && playerStates.PlayerState == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
    }
}
