using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]private IntVariable isGrounded;
    [SerializeField] private float MaxDistanceForGroundCheck;
    [SerializeField] private LayerMask isGroundLayer;
    private void Update()
    {
        Vector3 direction1 = Vector3.down;
        Vector3 origin1 = transform.position + new Vector3(0, 0, 0);
        if(Physics2D.Raycast(origin1, direction1, MaxDistanceForGroundCheck, isGroundLayer))
        {
            isGrounded.Value = 1;
        }
        else
        {
            isGrounded.Value = 0;
        }
            Debug.DrawRay(origin1, direction1 * MaxDistanceForGroundCheck, Color.red);
        
    }
}
