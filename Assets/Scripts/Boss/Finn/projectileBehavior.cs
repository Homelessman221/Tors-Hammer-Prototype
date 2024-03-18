using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehavior : MonoBehaviour
{
    [SerializeField]private IntVariable casterFacingRight;
    private int isFacingRight;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    private float lifeTimer;

    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        isFacingRight = casterFacingRight.Value;
        lifeTimer = lifeTime;
        if (isFacingRight == 0)
        {
            rb.velocity = Vector2.left * moveSpeed;
        }
        if (isFacingRight == 1)
        {
            rb.velocity = Vector2.right * moveSpeed;
        }
    }

    private void Update()
    {
        lifeTimer = Time.deltaTime;
        if(lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
