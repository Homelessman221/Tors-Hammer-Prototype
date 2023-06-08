using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playerrecall : MonoBehaviour
{
    private PlayerThrow playerThrow;
    private PlayerMove playerMove;
    private PlayerStates playerStates;

    [SerializeField] private GameObject axe;

    private float selfRecallSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        playerThrow = GetComponent<PlayerThrow>();
        playerMove = GetComponent<PlayerMove>();
        playerStates = GetComponent<PlayerStates>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (playerThrow.axeStates == 3)
        {
            var step = selfRecallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, axe.transform.position, step);
        }
    }
}
