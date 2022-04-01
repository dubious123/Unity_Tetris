using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Linq;
using static Define;
using static UnityEngine.InputSystem.InputAction;
using Random = UnityEngine.Random;

public class GameHelper : MonoBehaviour
{
    [SerializeField] public Tilemap Map;
    [SerializeField] public Tilemap Map_Keep;
    [SerializeField] public Tilemap Map_Guide;
    [SerializeField] public TileBase[] _baseBlocks;
    [SerializeField] public float speedOffset = 0.4f;
    public float CurrentSpeed { get { return speedOffset - Mgr.GameEx.CurrentLevel * 0.01f; } }
    
    [SerializeField] public Transform Keep_Camera;
    float _deltaTime;
    Tetromino _currentTetro;
    Tetromino _keep;
    public Tetromino CurrentTetro { get { return _currentTetro; } }
    TetrominoGenerator _generator = new TetrominoGenerator();
    bool _keepNotUsed = true;
    public bool IsGameRunning { get; set; } = false;
    private void Update()
    {
        if (!IsGameRunning)
            return;
        _deltaTime += Time.deltaTime;
        if (_deltaTime > CurrentSpeed)
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
            }
        }
    }



    public void Set(TileBase tile, params Vector3Int[] arr)
    {
        foreach (var pos in arr)
            Map.SetTile(pos, tile);
        UpdateGuide();
    }
    public void Set_Keep(Tetromino keep)
    {
        if (keep == null)
            return;
        Map_Keep.ClearAllTiles();
        keep.Pos = Vector3Int.zero;
        var posList = keep.GetAllBlockPos();
        var xList = new List<int>();
        var yList = new List<int>();

        foreach (var pos in posList)
        {
            if (!xList.Contains(pos.x))
                xList.Add(pos.x);
            if (!yList.Contains(pos.y))
                yList.Add(pos.y);
            Map_Keep.SetTile(pos, keep.MyTile);
        }
        var xOffset = (int)xList.Average();
        var yOffset = (int)yList.Average();
        int yCount = yList.Count;
        int xCount = xList.Count;
        Keep_Camera.position = 
            xCount % 2 == 0? 
            yCount % 2 == 0?
            new Vector3(0, 0, -10) :
            new Vector3(0, 0.5f + yOffset, -10) :
            yCount % 2 == 0 ?
            new Vector3(0.5f + xOffset, 0, -10) :
            new Vector3(0.5f + xOffset, 0.5f + yOffset, -10);
    }
    public void Set_Guide(TileBase tile, params Vector3Int[] arr)
    {
        foreach (var pos in arr)
            Map_Guide.SetTile(pos, tile);
    }
    public Tetromino CreateTetro(Tetromino keep = null)
    {
        _keepNotUsed = true;
        Tetromino tetro;
        if (_currentTetro != null)
        {
            Debug.LogError("Trying to create new Tetro but there is already active tetro");
            return null;
        }
        if(keep == null)
        {
            tetro = _generator.GetTetro((TetrominoType)Random.Range(0, 7));
            tetro.MyTile = _baseBlocks[Random.Range(0, 9)];
        }
        else
        {
            tetro = _generator.GetTetro(keep.TetroType);
            tetro.MyTile = keep.MyTile;
        }
        tetro.Pos.x = 5;
        foreach (Vector3Int childPos in tetro.GetAllBlockPos())
        {
            Map.SetTile(childPos, tetro.MyTile);
        }
        _currentTetro = tetro;
        return _currentTetro;
    }
    void UpdateGuide()
    {
        if (_currentTetro == null)
            return;
        int yDown = _currentTetro.CanFall(Map);
        Map_Guide.ClearAllTiles();
        foreach (var pos in _currentTetro.GetAllBlockPos()) 
        {
            var t = pos;
            t.y -= yDown;
            Set_Guide(_currentTetro.MyTile, t);
        }
    }
    public void Fall(CallbackContext c)
    {
        if (_currentTetro == null)
            return;
        Mgr.InputEx.ControlGameInput(false);
        int yDown = _currentTetro.CanFall(Map);
        Down(yDown);
        Mgr.GameEx.UpdateScore(yDown * Mgr.GameEx.CurrentLevel);
        Mgr.InputEx.ControlGameInput(true);
    }
    public void Down(CallbackContext c)
    {
        if (_currentTetro.CanDown(Map))
            Down();
    }
    public void Down(int i = 1)
    {
        if (_currentTetro == null)
            return;
        Set(null ,_currentTetro.GetAllBlockPos());
        _currentTetro.Pos.y -= i;
        Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
    }
    public void MoveLeft(CallbackContext c)
    {
        if (_currentTetro == null)
            return;
        if (_currentTetro.CanLeft(Map))
        {
            Set(null, _currentTetro.GetAllBlockPos());
            _currentTetro.Pos.x --;
            Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
        }
    }
    public void MoveRight(CallbackContext c)
    {
        if (_currentTetro == null)
            return;
        if (_currentTetro.CanRight(Map))
        {
            Set(null, _currentTetro.GetAllBlockPos());
            _currentTetro.Pos.x++;
            Set(_currentTetro.MyTile, _currentTetro.GetAllBlockPos());
        }
    }
    public void RotateRight(CallbackContext c)
    {
        if (_currentTetro == null)
            return;
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
        if (_currentTetro == null)
            return;
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
    public void Keep(CallbackContext c)
    {
        if (_currentTetro == null)
            return;
        if (_keepNotUsed)
        {
            var temp = _keep;
            _keep = _currentTetro;
            Set(null, _currentTetro.GetAllBlockPos());
            Set_Keep(_keep);
            _currentTetro = null;
            _currentTetro = CreateTetro(temp);
            _keepNotUsed = false;
        }

    }
    public void Pause()
    {

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
