using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static Define;
using static UnityEngine.InputSystem.InputAction;
using Random = UnityEngine.Random;

public class GameHelper : MonoBehaviour
{
    [SerializeField] public Tilemap Map;
    [SerializeField] public TileBase[] _baseBlocks;
    [SerializeField] public float _currentSpeed = 0.5f;
    float _deltaTime;
    Tetromino _currentTetro;
    public Tetromino CurrentTetro { get { return _currentTetro; } }
    TetrominoGenerator _generator = new TetrominoGenerator();

    public bool IsGameRunning { get; set; } = false;
    private void Update()
    {
        if (!IsGameRunning)
            return;
        _deltaTime += Time.deltaTime;
        if (_deltaTime > _currentSpeed)
        {
            _deltaTime = 0f;
            if (_currentTetro == null)
                CreateTetro();
            if (_currentTetro.CanDown(Map))
                Down();
            else
            {
                //Check Game Condition
                Mgr.GameEx.CheckGameCondition();
                if (!IsGameRunning)
                    return;
                _currentTetro = null;
                CreateTetro();
            }
        }
    }
    public void Set(TileBase tile, params Vector3Int[] arr)
    {
        foreach (var pos in arr)
            Map.SetTile(pos, tile);
    }
    public Tetromino CreateTetro()
    {
        if (_currentTetro != null)
        {
            Debug.LogError("Trying to create new Tetro but there is already active tetro");
            return null;
        }
        Tetromino tetro = _generator.GetTetro((TetrominoType)Random.Range(0,6));
        tetro.MyTile = _baseBlocks[Random.Range(0, 9)];
        tetro.Pos.x = 5;
        foreach (Vector3Int childPos in tetro.GetAllBlockPos())
        {
            Map.SetTile(childPos, tetro.MyTile);
        }
        _currentTetro = tetro;
        return _currentTetro;
    }
    public void Fall(CallbackContext c)
    {
        Down(_currentTetro.CanFall(Map));
    }
    public void Down(CallbackContext c)
    {
        if (_currentTetro.CanDown(Map))
            Down();
    }
    public void Down(int i = 1)
    {
        Set(null ,_currentTetro.GetAllBlockPos());
        _currentTetro.Pos.y -= i;
        Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
    }
    public void MoveLeft(CallbackContext c)
    {
        if (_currentTetro.CanLeft(Map))
        {
            Set(null, _currentTetro.GetAllBlockPos());
            _currentTetro.Pos.x --;
            Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
        }
    }
    public void MoveRight(CallbackContext c)
    {
        if (_currentTetro.CanRight(Map))
        {
            Set(null, _currentTetro.GetAllBlockPos());
            _currentTetro.Pos.x++;
            Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
        }
    }
    public void RotateRight(CallbackContext c)
    {
        if (_currentTetro.CanRotateR(Map))
        {
            Set(null, _currentTetro.GetAllBlockPos());
            for (int i = 0; i < 4; i++)
            {
                float t = -_currentTetro.ChildVector[i].x;
                _currentTetro.ChildVector[i].x = _currentTetro.ChildVector[i].y;
                _currentTetro.ChildVector[i].y = t;
            }
            Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
        }
    }
    public void RotateLeft(CallbackContext c)
    {
        if (_currentTetro.CanRotateL(Map))
        {
            Set(null, _currentTetro.GetAllBlockPos());
            for(int i = 0; i < 4; i++)
            {
                float t = _currentTetro.ChildVector[i].x;
                _currentTetro.ChildVector[i].x = -_currentTetro.ChildVector[i].y;
                _currentTetro.ChildVector[i].y = t;
            }
            Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
        }
    }
    public void DeleteLine(int y)
    {
        Vector3Int[] line = new Vector3Int[12];
        for (int x = 0; x < 12; x++)
        {
            line[x] = new Vector3Int(x, y, 0);
        }
        Set(null, line);
        for(int i = y; i < 24; i++)
        {
            SwapLine(ref line);
        }
    }
    void SwapLine(ref Vector3Int[] lowLine)
    {
        Vector3Int[] upLine = new Vector3Int[12];
        for (int i = 0; i<12; i++) 
        {
            upLine[i] = lowLine[i];
            upLine[i].y++;
            SwapTile(upLine[i], lowLine[i]);
        }
        lowLine = upLine;
    }
    void SwapTile(Vector3Int p1, Vector3Int p2)
    {
        var temp = Map.GetTile(p1);
        Set(Map.GetTile(p2), p1);
        Set(temp, p2);
    }
}
