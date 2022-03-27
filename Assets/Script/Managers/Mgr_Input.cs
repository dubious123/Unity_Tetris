using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Mgr_Input 
{
    PlayerInput p;
    Dictionary<string, Action<CallbackContext>> handlers;
    public PlayerInput _playerInput 
    {
        get
        {  
            return p != null ?
                p : p = Mgr.ResourceEx.Instantiate("@PlayerInput").GetComponent<PlayerInput>();
        }
    }
    public void ClearAction()
    {
        if (handlers != null)
        {
            foreach (var a in _playerInput.actions.actionMaps[0])
            {
                a.performed -= handlers[a.name];
            }
        }
        handlers = new Dictionary<string, Action<CallbackContext>>();
    }
    public void ConnectAction(string actionName, Action<CallbackContext> action)
    {
        InputAction inputA = _playerInput.actions.FindAction(actionName);
        if (inputA == null)
        {
            Debug.LogError("Input Action not Found");
            return;
        }
        handlers.Add(actionName, action);

        inputA.performed += handlers[actionName];
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
