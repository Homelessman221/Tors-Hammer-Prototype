using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private IntVariable PlayerStates;
    [SerializeField] private IntVariable isFacingRight;
    [SerializeField] private IntVariable isFacingUp;

    [SerializeField] private float punchLungeSpeed;

    private bool canPunch = true;
    [SerializeField] private float punchTime;
    [SerializeField] private float timeBeforeControllBack;
    [SerializeField] private float punchCooldown;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private IntVariable isGrounded;

    [SerializeField] private GameObject punchBox;

    [SerializeField] private Transform upBoxPos;
    [SerializeField] private Transform downBoxpos;
    [SerializeField] private Transform leftBoxPos;
    [SerializeField] private Transform rightBoxPos;

    [SerializeField] private float pogoStrength;
    [SerializeField] private float hitEnemyPush;

    private bool hasHit = false;
    private int currentPunchDirection;
     public void AttackInput()
    {
        

        if (PlayerStates.Value == 0 && canPunch)
        {
            PlayerStates.Value = 2;
            canPunch = false;
            StartCoroutine(PunchEnd());
            StartCoroutine(Cooldown());
            StartCoroutine(ControllBack());
            //print("punch");
            DirectionThings();
            punchBox.SetActive(true);
            hasHit = false;
        }
    }
    private IEnumerator PunchEnd()
    {
        yield return new WaitForSeconds(punchTime);
        punchBox.SetActive(false);
        if(isGrounded.Value == 1 && !hasHit)
        {
        rb.velocity = Vector2.zero;
        }
        //print("punchEnd");
        
    }
    private IEnumerator ControllBack()
    {
        yield return new WaitForSeconds(timeBeforeControllBack);

        PlayerStates.Value = 0;

    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(punchCooldown);
        canPunch = true;
        //print("punch cool down over");
    }

    private void DirectionThings()
    {
        if (isGrounded.Value == 1)
        {
            if (isFacingUp.Value == 1)
            {
                print("punch up");
                rb.velocity = Vector2.zero;
                punchBox.transform.position = upBoxPos.transform.position;
                currentPunchDirection = 3;
            }
            if (isFacingUp.Value == 0 || isFacingUp.Value == -1)
            {
                if (isFacingRight.Value == 1)
                {
                    print("punch right");
                    rb.velocity = Vector2.right * punchLungeSpeed;
                    punchBox.transform.position = rightBoxPos.transform.position;
                    currentPunchDirection = 0;
                }
                if (isFacingRight.Value == 0)
                {
                    print("punch left");
                    rb.velocity = Vector2.left * punchLungeSpeed;
                    punchBox.transform.position = rightBoxPos.transform.position;
                    currentPunchDirection = 1;
                }
            }
        }
        else
        {
            if (isFacingUp.Value == 1)
            {
                print("air punch up");
                punchBox.transform.position = upBoxPos.transform.position;
                currentPunchDirection = 3;
            }
            if (isFacingUp.Value == -1)
            {
                print("air punch down");
                punchBox.transform.position = downBoxpos.transform.position;
                currentPunchDirection = 4;

            }
            if (isFacingUp.Value == 0)
            {
                if (isFacingRight.Value == 1)
                {
                    print("air punch right");
                    punchBox.transform.position = rightBoxPos.transform.position;
                    currentPunchDirection = 0;
                }
                if (isFacingRight.Value == 0)
                {
                    print("air punch left");
                    punchBox.transform.position = rightBoxPos.transform.position;
                    currentPunchDirection=1;
                }
            }
        }
       

    }

    public void PunchHit()
    {
        hasHit = true;
        
        if(isGrounded.Value == 0)
        {
            if (currentPunchDirection == 3)
            {
                rb.velocity = new Vector2(rb.velocity.x, -pogoStrength);
            }
            if (currentPunchDirection == 4)
            {
                rb.velocity = new Vector2(rb.velocity.x,pogoStrength);
            }
            if (currentPunchDirection == 0)
            {
                rb.velocity = Vector2.left * hitEnemyPush;
            }
            if (currentPunchDirection == 1)
            {
                rb.velocity = Vector2.left * -hitEnemyPush;
            }
        }
        else
        {
            if (currentPunchDirection == 0)
            {
                rb.velocity = Vector2.left * hitEnemyPush;
            }
            if (currentPunchDirection == 1)
            {
                rb.velocity = Vector2.left * -hitEnemyPush;
            }
        }
    }
}
