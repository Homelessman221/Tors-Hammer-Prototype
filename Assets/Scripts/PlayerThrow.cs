using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerThrow : MonoBehaviour
{
    private PlayerInput input;
    private PlayerMove move;
    private Rigidbody2D axerb;
    private Animator animator;
    private Animator axeAnimator;
    private PlayerStates playerState;

    private BoxCollider2D collider;

    private float maxGravity = 2f;
    [SerializeField] private float currentGravity;
    private float gravityAcceleration = 7;

    private float axeSpeed = 40;
    [SerializeField]private float currentAxeRecallSpeed = 0;
    private float maxAxeRecallSpeed = 10000;
    private float recallAcceleration = 90;
    private float axeRotateSpeed = 100;

    private bool axeIsFacingRight = true;
    private float axeHopStrenght = 5;
    private float axeMaxDistanceForGroundCheck = 0.5f;

    private bool axePositionHasReset;
    [SerializeField] private GameObject axe;
    [SerializeField] private bool axeGrounded;
    private bool isPlayer;

    public bool playerHasAxe;
    public int axeStates;

    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Collider2D hurtBox;
    [SerializeField] private Collider2D hitBox;
    [SerializeField] private IntVariable playerStates;
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
        axeAnimator = axe.GetComponent<Animator>();
        playerState = GetComponent<PlayerStates>();

        axerb = axe.GetComponent<Rigidbody2D>();
        collider = axe.GetComponent<BoxCollider2D>();
        axerb.gravityScale = currentGravity;
        playerHasAxe = true;
    }

    private void Update()
    {
        PlayerHasAxe();
        AxeGroundCeck();
    }

    private void FixedUpdate()
    {
        AxeStates();
    }

    private void PlayerHasAxe()
    {
        switch(playerHasAxe)
        {
            case true:
                //playerState.PlayerState = 0;
                animator.Play("PlayerIdleAxe");
                axe.SetActive(false);
                axePositionHasReset = false;
                /*
                if(input.ThrowValue || input.RecallSelfValue)
                {
                    playerHasAxe = false;
                }
                */
                break;
            case false:
                animator.Play("PlayerIdle");
                axe.SetActive(true);
                if(!axePositionHasReset)
                {
                    axe.transform.position = transform.position;
                    axePositionHasReset = true;
                    axerb.velocity = new Vector2(axerb.velocity.x, axeHopStrenght);
                    axeStates = 0;
                    axerb.constraints = RigidbodyConstraints2D.None;
                    currentGravity = 0;
                    collider.isTrigger = true;
                    if (move.isFacingRight.Value == 1)
                    {
                        axeIsFacingRight = true;
                    }
                    if(move.isFacingRight.Value == 0)
                    {
                        axeIsFacingRight = false;
                    }
                }
                if (input.ThrowValue)
                {
                    axeStates = 2;
                    axerb.constraints = RigidbodyConstraints2D.None;
                    axerb.velocity = Vector3.zero;
                }
                if (input.RecallSelfValue)
                {
                    axeStates = 3;
                    axerb.constraints = RigidbodyConstraints2D.FreezeAll;
                    axerb.velocity = Vector3.zero;
                    hitBox.enabled = true;
                    hurtBox.enabled = false;
                }
                break;
        }

    }

    private void AxeStates()
    {
        if(!playerHasAxe)
        {
            switch (axeStates)
            {
                case 0:
                    //playerState.PlayerState = 0;
                    //Axe is thrown and moves in the direction the player was facing when they threw

                    axerb.gravityScale = currentGravity;
                    
                    axe.transform.Rotate(Time.deltaTime * 0, 0, axeRotateSpeed, Space.Self);
                    axeAnimator.Play("AxeLightning");
                    if (axeIsFacingRight)
                    {
                        axerb.velocity = new Vector2(axeSpeed, axerb.velocity.y);
                    }
                    if (!axeIsFacingRight)
                    {
                        axerb.velocity = new Vector2(-axeSpeed, axerb.velocity.y);
                    }

                    if (currentGravity < maxGravity)
                    {
                        currentGravity += gravityAcceleration * Time.deltaTime;
                    }
                    if (axeGrounded)
                    {
                        axerb.velocity = Vector3.zero;
                        axeStates = 1;
                    }
                    break;
                case 1:
                    //The Axe has hit a surface and is no longer moving
                    //playerState.PlayerState = 0;
                    axeAnimator.Play("AxeNormal");
                    collider.isTrigger = false;
                    axerb.gravityScale = maxGravity;
                    axerb.constraints = RigidbodyConstraints2D.FreezeAll;
                    if(isPlayer)
                    {
                        axeGrounded = false;
                        axeStates = 0;
                        playerHasAxe = true;
                        currentAxeRecallSpeed = 0;
                    }
                    //axerb.velocity = Vector3.zero;
                    break;
                case 2:
                    //The axe is being recalled
                    //playerState.PlayerState = 0;
                    if (axeGrounded)
                    {

                    axerb.velocity = new Vector2(axerb.velocity.x, axeHopStrenght);
                    }
                    axeAnimator.Play("AxeLightning");
                    collider.isTrigger = true;
                    axerb.gravityScale = 0;
                    recallSpeedIncrease();
                    var step = currentAxeRecallSpeed * Time.deltaTime;
                    axe.transform.position = Vector3.MoveTowards(axe.transform.position, transform.position, step);
                    axe.transform.Rotate(Time.deltaTime * 0, 0, axeRotateSpeed, Space.Self);
                    //The Axe is flying towards the player, playerhaspickedup wil be set to true if the player collides with it
                    if (input.RecallSelfValue)
                    {
                        axeStates = 3;
                        axerb.constraints = RigidbodyConstraints2D.FreezeAll;
                        axerb.velocity = Vector3.zero;
                        hitBox.enabled = true;
                        hurtBox.enabled = false;
                    }
                    break;
                case 3:
                    //When player recalls self to axe. The axe will stop moving while spinning, when the player touches the axe
                    // it will be picked up
                    playerStates.Value = 1;
                    axerb.constraints = RigidbodyConstraints2D.FreezeAll;
                    axe.transform.Rotate(Time.deltaTime * 0, 0, axeRotateSpeed, Space.Self);
                    collider.isTrigger = true;
                    axeAnimator.Play("AxeLightning");
                    if (input.ThrowValue)
                    {
                        axeStates = 2;
                        axerb.constraints = RigidbodyConstraints2D.None;
                        axerb.velocity = Vector3.zero;
                    }
                    break;
                    
            }
        }
        
    }

    public void ThrowInput()
    {
        print("throw triggered");
        if(axeStates == 3)
        {
            axeStates = 2;
            axerb.constraints = RigidbodyConstraints2D.None;
            axerb.velocity = Vector3.zero;
        }
        else{
            if (playerHasAxe)
            {
                playerHasAxe = false;
            }
            else
            {
                axeStates = 2;
                axerb.constraints = RigidbodyConstraints2D.None;
                axerb.velocity = Vector3.zero;
            }
        }
        
    }

    public void SelfRecallInput()
    {
        if(axeStates == 2)
        {
            axeStates = 3;
            axerb.constraints = RigidbodyConstraints2D.FreezeAll;
            axerb.velocity = Vector3.zero;
            hitBox.enabled = true;
            hurtBox.enabled = false;
        }
        else
        {
            if (playerHasAxe)
            {
                playerHasAxe = false;
            }
            else
            {
                axeStates = 3;
                axerb.constraints = RigidbodyConstraints2D.FreezeAll;
                axerb.velocity = Vector3.zero;
                hitBox.enabled = true;
                hurtBox.enabled = false;
            }
        }
    }
        

    private void recallSpeedIncrease()
    {
        if(currentAxeRecallSpeed < maxAxeRecallSpeed)
        {

        currentAxeRecallSpeed += recallAcceleration * Time.deltaTime;
        }
    }
    private void AxeGroundCeck()
    {
        Vector3 direction1 = Vector3.down;
        Vector3 origin1 = axe.transform.position + new Vector3(0, 0, 0);
        bool isGrounded1 = Physics2D.Raycast(origin1, direction1, axeMaxDistanceForGroundCheck, isGroundLayer);
        Debug.DrawRay(origin1, direction1 * axeMaxDistanceForGroundCheck, Color.red);

        Vector3 direction2 = Vector3.up;
        Vector3 origin2 = axe.transform.position + new Vector3(0, 0, 0);
        bool isGrounded2 = Physics2D.Raycast(origin2, direction2, axeMaxDistanceForGroundCheck, isGroundLayer);
        Debug.DrawRay(origin2, direction2 * axeMaxDistanceForGroundCheck, Color.red);

        Vector3 direction3 = Vector3.left;
        Vector3 origin3 = axe.transform.position + new Vector3(0f, 0, 0);
        bool isGrounded3 = Physics2D.Raycast(origin3, direction3, axeMaxDistanceForGroundCheck, isGroundLayer);
        Debug.DrawRay(origin3, direction3 * axeMaxDistanceForGroundCheck, Color.red);

        Vector3 direction4 = Vector3.right;
        Vector3 origin4 = axe.transform.position + new Vector3(0, 0, 0);
        bool isGrounded4 = Physics2D.Raycast(origin4, direction4, axeMaxDistanceForGroundCheck, isGroundLayer);
        Debug.DrawRay(origin4, direction4 * axeMaxDistanceForGroundCheck, Color.red);


        //Noe veldig veldig kul kode
        axeGrounded = isGrounded1 || isGrounded2 || isGrounded3 || isGrounded4;

        bool isPlayer1 = Physics2D.Raycast(origin1, direction1, axeMaxDistanceForGroundCheck, playerLayer);
        bool isPlayer2 = Physics2D.Raycast(origin2, direction2, axeMaxDistanceForGroundCheck, playerLayer);
        bool isPlayer3 = Physics2D.Raycast(origin3, direction3, axeMaxDistanceForGroundCheck, playerLayer);
        bool isPlayer4 = Physics2D.Raycast(origin4, direction4, axeMaxDistanceForGroundCheck, playerLayer);

        isPlayer = isPlayer1 || isPlayer2 || isPlayer3 || isPlayer4;
        if(isPlayer)
        {
            //print("player is!");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(axeStates == 2 || axeStates == 3 && other.gameObject.CompareTag("Axe"))
        {
            axeGrounded = false;
            axeStates = 0;
            playerHasAxe = true;
            currentAxeRecallSpeed = 0;
            hitBox.enabled = false;
            hurtBox.enabled = true;
            playerStates.Value = 0;
            if (axeStates == 3)
            {
                playerStates.Value = 0;
            }
        }
    }


}
