using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FSlide : MonoBehaviour
{
    private int attackState;
    [SerializeField] private IntVariable bossState;

    [SerializeField] private float attackWindUpTime;
    private float attackWindUpTimer;

    [SerializeField] private float slideSpeed;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private IntVariable isFacingRight;

    [SerializeField] private float maxDistanceForWallCheck;
    private bool isWall;

    [SerializeField] LayerMask wall;
    private void OnEnable()
    {
        attackState = 1;
        attackWindUpTimer = attackWindUpTime;
        isWall = false;
    }

    private void OnDisable()
    {
        attackState = 0;
        
    }
    private void Update()
    {
        switch (attackState)
        {
            case 1:
                attackWindUpTimer -= Time.deltaTime;

                if(attackWindUpTimer <= 0)
                {
                    attackState = 2;
                }
                break;
            case 2:
                checkForWall();
                if(isFacingRight.Value == 0)
                {
                    RB.velocity = Vector2.left * slideSpeed * Time.deltaTime;
                }
                if (isFacingRight.Value == 1)
                {
                    RB.velocity = Vector2.right * slideSpeed * Time.deltaTime;
                }
                if (isWall)
                {
                    bossState.Value = 3;
                }
                break;
        }
    }

    private void checkForWall()
    {
        if(isFacingRight.Value == 0)
        {
            Vector3 direction1 = Vector3.left;
            Vector3 origin1 = transform.position + new Vector3(0, 0, 0);
            isWall = Physics2D.Raycast(origin1, direction1, maxDistanceForWallCheck, wall);
            Debug.DrawRay(origin1, direction1 * maxDistanceForWallCheck, Color.red);
        }
        if (isFacingRight.Value == 1)
        {
            Vector3 direction1 = Vector3.right;
            Vector3 origin1 = transform.position + new Vector3(0, 0, 0);
            isWall = Physics2D.Raycast(origin1, direction1, maxDistanceForWallCheck, wall);
            Debug.DrawRay(origin1, direction1 * maxDistanceForWallCheck, Color.red);
        }
    }
}
