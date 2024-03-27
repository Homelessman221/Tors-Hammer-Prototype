using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    [SerializeField] Vector2Variable moveInput;
    [SerializeField] private float moveTreshold;
    [SerializeField] private IntVariable isFacingRight;
    public void OnAttack()
    {
        if(moveInput.Value.y > moveTreshold)
        {
            //slå opp retning
        }
        else if(moveInput.Value.y < -moveTreshold)
        {
            //slå ned
        }
        else
        {
            //slå med retning
        }
    }
}
