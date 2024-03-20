using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterJump : MonoBehaviour
{
    private int attackState;
    private int waterJumpState;
    [SerializeField] private IntVariable bossState;
    [SerializeField] private Collider2D bossCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObjectVariable player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float attackWindUpTime;
    private float attackWindUpTimer;

    [SerializeField] private float attackCooldownTime;
    private float attackCooldownTimer;

    [SerializeField]private float timeBeforeJump;
    private float timeBeforeJumpTimer;
    private void OnEnable()
    {
        attackState = 1;
        waterJumpState = 1;
        attackWindUpTimer = attackWindUpTime;
    }

    private void OnDisable()
    {
        attackState = 0;
        waterJumpState = 0;
    }
    private void Update()
    {
        switch (attackState)
        {
            case 1:
                attackWindUpTimer -= Time.deltaTime;

                if (attackWindUpTimer <= 0)
                {
                    attackState = 2;
                }
                break;
            case 2:
                WaterJumpHappenings();
                
                break;
            case 3:
                attackCooldownTimer -= Time.deltaTime;
                if (attackCooldownTimer <= 0)
                {
                    bossState.Value = 0;
                }
                break;
        }
    }
    private void WaterJumpHappenings()
    {
        switch (waterJumpState)
        {
            case 1:
                bossCollider.isTrigger = true;
                timeBeforeJumpTimer = timeBeforeJump;
                break;
            case 2:
                rb.gravityScale = 0;
                transform.position = new Vector3(transform.position.x, -7,transform.position.z);
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.Value.transform.position.x, -7, 0), moveSpeed);
                if (transform.position.x < player.Value.transform.position.x)
                {
                    rb.AddForce(Vector2.right * moveSpeed);
                }
                if (transform.position.x > player.Value.transform.position.x)
                {
                    rb.AddForce(Vector2.left * moveSpeed);
                }
                timeBeforeJumpTimer -= Time.deltaTime;
                if(timeBeforeJumpTimer <= 0)
                {
                    waterJumpState = 3;
                    timeBeforeJumpTimer = 1;
                }
                break;
            case 3:
                timeBeforeJumpTimer -= Time.deltaTime;
                if(timeBeforeJumpTimer <= 0)
                {
                    rb.velocity = Vector2.up * jumpPower;
                    timeBeforeJumpTimer = 1;
                    waterJumpState = 4;
                }
                break;
            case 4:
                timeBeforeJumpTimer -= Time.deltaTime;
                if(timeBeforeJumpTimer <= 0)
                {
                    attackDone();
                    bossCollider.isTrigger = false;
                }
                break;
        }
    }
    private void attackDone()
    {
        attackState = 3;
        attackCooldownTimer = attackCooldownTime;
        rb.gravityScale = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (waterJumpState == 1 && collision.gameObject.CompareTag("Water"))
        {
            waterJumpState = 2;
        }
    }
}
