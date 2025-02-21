using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : IDisposable
{
    private InputActions _actions;
    
    public float MovementDirection { get; private set; }
    public bool IsJumpHeld { get; private set; }

    public Action OnJump;
    public Action OnPray;
    public Action OnAttack;
    
    public InputReader()
    {
        _actions = new InputActions();
        _actions.Enable();

        _actions.Player.Move.performed += OnMove;
        _actions.Player.Jump.performed += OnJumped;
        _actions.Player.Jump.canceled += OnJumpReleased;
        _actions.Player.Pray.performed += OnPrayed;
        _actions.Player.Attack.performed += OnAttacked;
    }
    
    public void Dispose()
    {
        _actions.Disable();
        
        _actions.Player.Move.performed -= OnMove;
        _actions.Player.Jump.performed -= OnJumped;
        _actions.Player.Jump.canceled -= OnJumpReleased;
        _actions.Player.Pray.performed -= OnPrayed;
        _actions.Player.Attack.performed -= OnAttacked;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        MovementDirection = obj.ReadValue<Vector2>().x;
    }

    private void OnJumped(InputAction.CallbackContext obj)
    {
        IsJumpHeld = true;
        OnJump?.Invoke();
    }
    
    private void OnJumpReleased(InputAction.CallbackContext obj)
    {
        IsJumpHeld = false;
    }

    private void OnPrayed(InputAction.CallbackContext obj)
    {
        OnPray?.Invoke();
    }

    private void OnAttacked(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }
}
