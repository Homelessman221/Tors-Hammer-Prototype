using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    [HideInInspector] public Vector2 MoveVector;
    [HideInInspector] public bool ThrowValue;
    [HideInInspector] public bool JumpValue;
    [HideInInspector] public bool RecallSelfValue;
    [HideInInspector] public bool AttackValue;
    [HideInInspector] public bool AttackValueReleased;


    private PlayerInputActions _input;

    [SerializeField] Vector2Variable moveInput;

    [SerializeField] GameEvent attackTriggered;
    [SerializeField] GameEvent attackReleased;
    [SerializeField] GameEvent throwTriggered;
    [SerializeField] GameEvent throwReleased;
    [SerializeField] GameEvent selfRecallTriggered;
    [SerializeField] GameEvent selfRecallReleased;
    [SerializeField] GameEvent powerUpTriggered;
    [SerializeField] GameEvent pauseTriggered;
    [SerializeField] GameEvent jumpTriggered;
    [SerializeField] GameEvent jumpReleased;
 

    private bool _gamePaused;

    public bool InteractValue { get; private set; }
    public float LongJump { get; private set; }

    private void Awake() { _input = new PlayerInputActions(); }

    private void Update()
    {
        moveInput.Value = _input.Player.Move.ReadValue<Vector2>();

        //lookInput.Value = _input.Player.Look.ReadValue<Vector2>();

    }

    private void AttackInputPressed(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            attackTriggered.Raise();

        }

    }

    private void AttackReleaseInput(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            attackReleased.Raise();
        }

    }
    private void throwInput(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            throwTriggered.Raise();

        }

    }
    private void throwInputRelease(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            throwReleased.Raise();

        }

    }
    private void selfRecallInput(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            selfRecallTriggered.Raise();
        }

    }

    private void SelfRecallReleased(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            selfRecallReleased.Raise();
        }
    }
    private void PowerUpInput(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            selfRecallReleased.Raise();
        }
    }
    private void PowerUpInputReleased(InputAction.CallbackContext context)
    {
        
        {
            pauseTriggered.Raise();
        }
    }
    private void JumpInput(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            jumpTriggered.Raise();
        }
    }
    private void JumpInputReleased(InputAction.CallbackContext context)
    {
        if (!_gamePaused)
        {
            jumpReleased.Raise();
        }
    }

    private void OnEnable()
    {
        _gamePaused = false;
        _input.Enable();
        _input.Player.Punch.started += AttackInputPressed;
        _input.Player.Punch.canceled += AttackReleaseInput;
        _input.Player.Throw.started += throwInput;
        //_input.Player.Throw.canceled += throwInputRelease;
        //XX_input.Player.Throw.started += throwInput;
        //_input.Player.PauseMenu.started += PauseMenu;
        _input.Player.RecallSelf.started += selfRecallInput;
       // _input.Player.RecallSelf.canceled += SelfRecallReleased;
        _input.Player.PowerUp.started += PowerUpInput;
        _input.Player.PowerUp.canceled += PowerUpInputReleased;
        _input.Player.Jump.started += JumpInput;
        _input.Player.Jump.canceled += JumpInputReleased;
    }
    private void OnDisable() { _input.Disable(); }


    public void GamePaused(bool gamePaused)
    {
        _gamePaused = gamePaused;
    }
}

