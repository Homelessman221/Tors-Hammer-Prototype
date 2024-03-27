using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirections : MonoBehaviour
{
    [SerializeField] private Vector2Variable moveVector;
    [SerializeField] private IntVariable isFacingRight;
    [SerializeField] private IntVariable isFacingUp;
    [SerializeField] private float moveTreshold;
    private void Update()
    {
        if(moveVector.Value.x < 0)
        {
            isFacingRight.Value = 0;
        }
        else if(moveVector.Value.x > 0)
        {
            isFacingRight.Value = 1;
        }

        if (moveVector.Value.y > moveTreshold)
        {
            isFacingUp.Value = 1;
        }
        else if (moveVector.Value.y < -moveTreshold)
        {
            isFacingUp.Value= 0;
        }
    }
}
