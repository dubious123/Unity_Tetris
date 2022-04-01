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
    int _lineCleared = 0;
    bool _newScore = false;
    public int CurrentLevel { get { return _level; } }
    public void ReadyNewGame()
    {
        GHelper = GameObject.Find("GameHelper").GetComponent<GameHelper>();
        _ui = GameObject.Find("UI_Helper").GetComponent<UI_Helper>();
        Mgr.InputEx.ClearAllActions();
        Mgr.InputEx.PushAction("Down", GHelper.Down);
        Mgr.InputEx.PushAction("Left", GHelper.MoveLeft);
        Mgr.InputEx.PushAction("Right", GHelper.MoveRight);
        Mgr.InputEx.PushAction("Fall", GHelper.Fall);
        Mgr.InputEx.PushAction("RotateL", GHelper.RotateLeft);
        Mgr.InputEx.PushAction("RotateR", GHelper.RotateRight);
        Mgr.InputEx.PushAction("Keep", GHelper.Keep);
        Mgr.InputEx.PushAction("Esc", ctx => PauseGame());
        Mgr.InputEx.ControlGameInput(false);

        Mgr.DataEx.ReadSave(ref _highScore);
        UpdateSideUI();
    }
    public void StartGame()
    {
        Mgr.InputEx.ControlGameInput(true);
        GHelper.IsGameRunning = true;
    }
    public void ResumeGame()
    {
        _ui.Close();
        GHelper.IsGameRunning = true;
        Mgr.InputEx.ControlGameInput(true);
    }
    public void PauseGame()
    {
        if (GHelper.IsGameRunning)
            GHelper.IsGameRunning = false;
        
        Mgr.InputEx.PushAction("Esc", ctx => ResumeGame(), true);

        Mgr.InputEx.ControlGameInput(false);
        Mgr.InputEx.EnableActions("Esc");

        _ui.Pause();
    }
    public void RestartGame()
    {
        //_ui.CloseAll();
        Mgr.SceneEx.MoveScene(SceneType.Game);
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
            GameEnd();
        }
        int lineCleared = 0;
        for(int y = 0; y < 24; y++)
        {
            bool isLined = true;
            for (int x = 0; x < 12; x++)
                isLined &= GHelper.Map.HasTile(new Vector3Int(x, y, 0));
            if (isLined)
            {
                GHelper.DeleteLine(y);
                y--;
                lineCleared++;
            }
        }
        _lineCleared += lineCleared;
        UpdateScore((lineCleared - 1).Factorial() * 1000 * _level);
        UpdateLevel();
        UpdateSideUI();
    }
    public void UpdateScore(int deltaScore = -1)
    {
        if (deltaScore == -1)
            return;
        _score += deltaScore;
        if (_score > _highScore)
            UpdateHighScore();
    }
    void UpdateHighScore()
    {
        _highScore = _score;
        _newScore = true;
    }
    void UpdateLevel()
    {
        int t = _lineCleared / 2 + 1;
        _level = t > 29 ? 30: t;
    }
    public void UpdateSideUI()
    {       
        _ui.UpdateLevel(_level);
        _ui.UpdateScore(_score);
        _ui.UpdateHighScore(_highScore);
    }
    void GameEnd()
    {
        if(_newScore)
            _ui.NewScore(_highScore);
        else
            _ui.YouLose();
    }
}
