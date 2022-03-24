using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Mgr_Input 
{
    PlayerInput p;
    public PlayerInput _playerInput 
    {
        get
        {  
            return p != null ?
                p : p = Mgr.ResourceEx.Instantiate("@PlayerInput").GetComponent<PlayerInput>();
        }
    }
    public void ConnectAction(string actionName, Action<CallbackContext> action)
    {
        InputAction inputA = _playerInput.actions.FindAction(actionName);
        if (inputA == null)
        {
            Debug.LogError("Input Action not Found");
            return;
        }
        inputA.performed += action;
    }
    public void ControlGameInput(bool control)
    {
        if (control)
        {
            _playerInput.ActivateInput();          
            return;
        }
        _playerInput.DeactivateInput();
    }
}
