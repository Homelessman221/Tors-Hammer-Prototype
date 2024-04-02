using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerStates playerStates;
    [SerializeField] private IntVariable isFacingRight;
    [SerializeField] private IntVariable isFacingUp;

    [SerializeField] private float punchLungeSpeed;

    private bool canPunch = true;
    [SerializeField] private float punchTime;
    [SerializeField] private float punchCooldown;
     public void AttackInput()
    {
        

        if (playerStates.PlayerState == 0 && canPunch)
        {
            playerStates.PlayerState = 2;
            canPunch = false;
            StartCoroutine(PunchEnd());
            StartCoroutine(Cooldown());
            print("punch");
        }
    }
    private IEnumerator PunchEnd()
    {
        yield return new WaitForSeconds(punchTime);
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(punchCooldown);
        canPunch = true;
        print("punch cool down over");
    }
}
