using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Mgr_Input 
{
    PlayerInput p;
    Dictionary<string, Stack<Action<CallbackContext>>> handlers = new Dictionary<string, Stack<Action<CallbackContext>>>
    {
        { "Down",new Stack<Action<CallbackContext>>()},
        { "Left",new Stack<Action<CallbackContext>>()},
        { "Right",new Stack<Action<CallbackContext>>()},
        { "Fall",new Stack<Action<CallbackContext>>()},
        { "RotateL",new Stack<Action<CallbackContext>>()},
        { "RotateR",new Stack<Action<CallbackContext>>()},
        { "Keep",new Stack<Action<CallbackContext>>()},
        { "Esc",new Stack<Action<CallbackContext>>()}
    };
    public PlayerInput _playerInput 
    {
        get
        {  
            return p != null ?
                p : p = Mgr.ResourceEx.Instantiate("@PlayerInput").GetComponent<PlayerInput>();
        }
    }
    public void Init()
    {
        foreach (var t in handlers.Keys.ToArray())
        {
            _playerInput.actions[t].performed += ctx => handlers[t].Peek().Invoke(ctx); 
        }
    }
    public void ClearAllActions()
    {
        foreach (var s in handlers.Keys.ToList())
            handlers[s].Clear();
    }
    public void ClearAction(string name)
    {
        handlers[name].Clear();
    }
    public void PushAction(string actionName, Action<CallbackContext> action, bool excuteOnce = false)
    {
        InputAction inputA = _playerInput.actions.FindAction(actionName);
        if (inputA == null)
        {
            Debug.LogError("Input Action not Found");
            return;
        }
        if (excuteOnce)
            action += ctx => handlers[actionName].Pop();
        handlers[actionName].Push(action);
    }
    public Action<CallbackContext> PeekAction(string name)
    {
        return handlers[name].Peek();
    }
    public void PopAction(string name)
    {
        handlers[name].Pop();
    }
    public void InvokeAction(string name, CallbackContext ctx)
    {
        handlers[name].Peek().Invoke(ctx);
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
    public void EnableActions(params string[] names)
    {
        foreach (var n in names)
        {
            _playerInput.actions[n].Enable();
        }
    }
    public void DisableActions(params string[] names)
    {
        foreach (var n in names)
        {
            _playerInput.actions[n].Disable();
        }
    }

}
