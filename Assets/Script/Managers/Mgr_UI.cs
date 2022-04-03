using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mgr_UI 
{
    UI_Helper _ui;
    public void Init()
    {
        _ui = GameObject.Find("UI_Helper").GetComponent<UI_Helper>();
    }
    public void Close()
    {
        _ui.Close();
    }
    public void Pause()
    {
        _ui.Pause();
    }

    internal void UpdateLevel(int level)
    {
        _ui.UpdateLevel(level);
    }

    internal void UpdateScore(int score)
    {
        _ui.UpdateScore(score);
    }

    internal void UpdateHighScore(int highScore)
    {
        _ui.UpdateHighScore(highScore);
    }

    internal void NewScore(int score)
    {
        _ui.NewScore(score);
    }

    internal void YouLose()
    {
        _ui.YouLose();
    }
}
