using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mgr_Game 
{
    Board _board { get { return GameObject.FindGameObjectWithTag("Map").GetComponent<Board>(); } }
    public void StartGame()
    {
        //_board.CreateBlock
        _board.CreateTetro();
    }
    public void ResumeGame()
    {

    }
    public void StopGame()
    {

    }
}
