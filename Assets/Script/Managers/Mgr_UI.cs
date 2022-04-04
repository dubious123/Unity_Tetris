using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mgr_UI 
{
    UI_Helper UI { get { if (_ui == null) _ui = GameObject.Find("UI_Helper").GetComponent<UI_Helper>(); return _ui; } }
    UI_Helper _ui;
    public void Init()
    {
    }
    public void Close()
    {
        UI.Close();
    }
    public void Pause()
    {
        UI.Pause();
    }

    internal void UpdateLevel(int level)
    {
        UI.UpdateLevel(level);
    }

    internal void UpdateScore(int score)
    {
        UI.UpdateScore(score);
    }

    internal void UpdateHighScore(int highScore)
    {
        UI.UpdateHighScore(highScore);
    }

    internal void NewScore(int score)
    {
        UI.NewScore(score);
    }

    internal void YouLose()
    {
        UI.YouLose();
    }
}
