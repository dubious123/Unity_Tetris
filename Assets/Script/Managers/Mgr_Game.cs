using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class Mgr_Game 
{
    public GameHelper GHelper;
    UI_Helper _ui;
    int _score = 0;
    int _highScore = 100;
    int _level = 1;
    public void ReadyNewGame()
    {
        GHelper = GameObject.Find("GameHelper").GetComponent<GameHelper>();
        _ui = GameObject.Find("UI_Helper").GetComponent<UI_Helper>();
        Mgr.InputEx.ClearAction();
        Mgr.InputEx.ConnectAction("Down", GHelper.Down);
        Mgr.InputEx.ConnectAction("Left", GHelper.MoveLeft);
        Mgr.InputEx.ConnectAction("Right", GHelper.MoveRight);
        Mgr.InputEx.ConnectAction("Fall", GHelper.Fall);
        Mgr.InputEx.ConnectAction("RotateL", GHelper.RotateLeft);
        Mgr.InputEx.ConnectAction("RotateR", GHelper.RotateRight);
        Mgr.InputEx.ConnectAction("Keep", GHelper.Keep);
        Mgr.InputEx.ControlGameInput(false);

        UpdateUI();
    }
    public void StartGame()
    {
        Mgr.InputEx.ControlGameInput(true);
        GHelper.IsGameRunning = true;
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
        gameCondition &= GHelper.CurrentTetro.GetAllBlockPos().Max(v => v.y) <= 23;
        GHelper.IsGameRunning = gameCondition;
        if (gameCondition == false)
        {
            _ui.ToggleYouLose();         
        }
        for(int y = 0; y < 24; y++)
        {
            bool isLined = true;
            for (int x = 0; x < 12; x++)
                isLined &= GHelper.Map.HasTile(new Vector3Int(x, y, 0));
            if (isLined)
            {
                GHelper.DeleteLine(y);
                y--;
                _score+=1000;
                UpdateUI();
            }
        }
    }
    public void UpdateUI(int level = -1, int score = -1, int highScore = -1)
    {       
        _ui.UpdateLevel(level == -1 ? _level : level);
        _ui.UpdateScore(score == -1 ? _score : score);
        _ui.UpdateHighScore(highScore == -1 ? (_highScore > _score ? _highScore : _highScore = _score)  : highScore);
    }
}
