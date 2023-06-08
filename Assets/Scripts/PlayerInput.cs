using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerInputActions inputActions;

    [HideInInspector] public Vector2 MoveVector;
    [HideInInspector] public bool ThrowValue;
    [HideInInspector] public bool JumpValue;
    [HideInInspector] public bool RecallSelfValue;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Update()
    {
        MoveVector = inputActions.Player.Move.ReadValue<Vector2>();

        ThrowValue = inputActions.Player.Throw.triggered;
        JumpValue = inputActions.Player.Jump.triggered;
        RecallSelfValue = inputActions.Player.RecallSelf.triggered;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
