using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private static GameInput _instance;

    public static GameInput Instance { get => _instance; }

    private PlayerInputActions playerInputActions;


    public event EventHandler OnPlayer1Jump;
    public event EventHandler OnPlayer2Jump;


    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player1.Enable();
        playerInputActions.Player2.Enable();

        playerInputActions.Player1.Jump.performed += JumpPerformedPlayer1;
        playerInputActions.Player2.Jump.performed += JumpPerformedPlayer2;
    }


    private void JumpPerformedPlayer1(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayer1Jump?.Invoke(this, EventArgs.Empty);
    }
    
    private void JumpPerformedPlayer2(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayer2Jump?.Invoke(this, EventArgs.Empty);
    }

    public float GetMoveDirPlayer1()
    {
        return playerInputActions.Player1.Move.ReadValue<float>();
    }

    public float GetMoveDirPlayer2()
    {
        return playerInputActions.Player2.Move.ReadValue<float>();
    }
}