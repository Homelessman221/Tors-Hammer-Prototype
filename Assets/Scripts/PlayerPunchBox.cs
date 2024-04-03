using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchBox : MonoBehaviour
{
    [SerializeField] private GameEvent punchHit;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        punchHit.Raise();
        print("punch Hit");
    }
}
