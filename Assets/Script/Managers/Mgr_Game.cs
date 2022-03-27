using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class Mgr_Game 
{
    GameHelper _game;
    UI_Helper _ui;
    public void ReadyNewGame()
    {
        _game = GameObject.Find("GameHelper").GetComponent<GameHelper>();
        _ui = GameObject.Find("UI_Helper").GetComponent<UI_Helper>();
        Mgr.InputEx.ClearAction();
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
    public void RestartGame()
    {
        Debug.Log("Restart");
    }
    public void QuitToMainMenu()
    {
        Mgr.SceneEx.MoveScene(SceneType.Main);
    }
    public void CheckGameCondition()
    {
        bool gameCondition = true;
        gameCondition &= _game.CurrentTetro.GetAllBlockPos().Max(v => v.y) <= 23;
        _game.IsGameRunning = gameCondition;
        if (gameCondition == false)
        {
            _ui.ToggleYouLose();         
        }
        for(int y = 0; y < 24; y++)
        {
            bool isLined = true;
            for (int x = 0; x < 12; x++)
                isLined &= _game.Map.HasTile(new Vector3Int(x, y, 0));
            if (isLined)
            {
                _game.DeleteLine(y);
                y--;
            }
        }
    }
}
