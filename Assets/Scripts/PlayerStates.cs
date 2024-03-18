using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public int PlayerState = 0;
    private PlayerMove move;
    private PlayerInput input;
    private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D collider;

    private float gravity = 1;

    [SerializeField] private GameObjectVariable playerScrubReference;
    private void Start()
    {
        move = GetComponent<PlayerMove>();
        input = GetComponent<PlayerInput>(); rb = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        playerScrubReference.Value = gameObject;
    }
    private void Update()
    {
        
        switch(PlayerState)
            {
            case 0:
                //Default state, player can do any action
                rb.gravityScale = gravity;
                collider.isTrigger = false;
                break;
            case 1:
                //Self Recall, cannot do any other action except recall wich cancels the self recall
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                collider.isTrigger = true;
                break;
            case 2:
                //Punching
                break;
            case 3:
                //Taking Damage
                break;
        }
    }
}
