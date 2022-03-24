using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

public class GameHelper : MonoBehaviour
{
    [SerializeField] public Tilemap _tilemap;
    [SerializeField] public TileBase _baseBlock;
    float _deltaTime;
    [SerializeField] public float _currentSpeed = 0.5f;
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
            if (_currentTetro.CanDown(_tilemap))
                Down();
            else
            {
                //Check Game Condition
                _currentTetro = null;
                CreateTetro();
            }
        }
    }
    public Tetromino CreateTetro()
    {
        if (_currentTetro != null)
        {
            Debug.LogError("Trying to create new Tetro but there is already active tetro");
            return null;
        }
        Tetromino tetro = _generator.GetTetro(Define.TetrominoType.I);
        tetro.Pos.x = Random.Range(0, 12);
        foreach (Vector2Int childPos in tetro.ChildVector)
        {
            _tilemap.SetTile(tetro.Pos + (Vector3Int)childPos, _baseBlock);
        }
        _tilemap.SetTile(tetro.Pos, _baseBlock);
        _currentTetro = tetro;
        return _currentTetro;
    }
    public void Fall(CallbackContext c)
    {
        Debug.Log("Fall");
    }
    public void Down(CallbackContext c)
    {
        if (_currentTetro.CanDown(_tilemap))
            Down();
        Debug.Log("Down");

    }
    public void Down()
    {
        foreach (var pos in _currentTetro.GetAllBlockPos())
            _tilemap.SetTile(pos, null);
        _currentTetro.Pos.y--;
        foreach (var pos in _currentTetro.GetAllBlockPos())
            _tilemap.SetTile(pos, _baseBlock);

    }
    public void MoveLeft(CallbackContext c)
    {
        Debug.Log("MoveLeft");

    }
    public void MoveRight(CallbackContext c)
    {
        Debug.Log("MoveRight");

    }
    public void RotateRight(CallbackContext c)
    {
        Debug.Log("RotateRight");

    }
    public void RotateLeft(CallbackContext c)
    {
        Debug.Log("RotateLeft");

    }
}
