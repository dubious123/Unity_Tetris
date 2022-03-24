using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mgr_Game 
{
     GameHelper _game;
    public void ReadyNewGame()
    {
        _game = GameObject.Find("GameHelper").GetComponent<GameHelper>();
        Mgr.InputEx.ConnectAction("Down", _game.Down);
        Mgr.InputEx.ConnectAction("Left", _game.MoveLeft);
        Mgr.InputEx.ConnectAction("Right", _game.MoveRight);
        Mgr.InputEx.ConnectAction("Fall", _game.Fall);
        Mgr.InputEx.ConnectAction("RotateL", _game.RotateLeft);
        Mgr.InputEx.ConnectAction("RotateR", _game.RotateRight);
        Mgr.InputEx.ControlGameInput(false);
    }
    public void StartGame()
    {
        Mgr.InputEx.ControlGameInput(true);
        _game.IsGameRunning = true;
    }
    public void ResumeGame()
    {

    }
    public void StopGame()
    {

    }
}
