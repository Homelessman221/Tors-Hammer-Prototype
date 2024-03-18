using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class FProjectile : MonoBehaviour
{
    private int attackState;
    [SerializeField] private IntVariable bossState;
   [SerializeField] private IntVariable isFacingRight;

    [SerializeField] private float attackWindUpTime;
    private float attackWindUpTimer;

    [SerializeField] private float attackCooldownTime;
    private float attackCooldownTimer;

    [SerializeField] private GameObject Projectile;

    [SerializeField] private Transform projectilePoint;
    private void OnEnable()
    {
        attackState = 1;
        attackWindUpTimer = attackWindUpTime;
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

                if (attackWindUpTimer <= 0)
                {
                    attackState = 2;
                }
                break;
            case 2:
                Instantiate(Projectile,projectilePoint.position, transform.rotation);
                attackState = 3;
                attackCooldownTimer = attackCooldownTime;
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
}
