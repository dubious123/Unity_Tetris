using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] public Tilemap _tilemap;
    [SerializeField] public TileBase _baseBlock;
    float _deltaTime;
    float _currentSpeed = 0.5f;
    Tetromino _currentTetro;
    TetrominoGenerator _generator = new TetrominoGenerator();
    private void Update()
    {
        _deltaTime += Time.deltaTime;
        if(_deltaTime > _currentSpeed)
        {
            _deltaTime = 0f;
            if (_currentTetro == null)
                return;

            Fall();
        }
    }
    public void CreateTetro()
    {
        if(_currentTetro != null) 
        {
            Debug.LogError("Trying to create new Tetro but there is already active tetro");
            return;
        }
        Tetromino tetro = _generator.GetTetro(Define.TetrominoType.I);
        tetro.Pos.x = Random.Range(0,12);
        foreach (Vector2Int childPos in tetro.ChildVector)
        {
            _tilemap.SetTile(tetro.Pos + (Vector3Int)childPos, _baseBlock);
        }
        _tilemap.SetTile(tetro.Pos, _baseBlock);
        _currentTetro = tetro;
    }
    public void Fall()
    {
        bool canMove = true;
        if (_currentTetro.Pos.y <= 2)
        {
            foreach (var pos in _currentTetro.GetAllBlockPos())
            {
                canMove &= pos.y > 0;
            }
            if (!canMove)
            {
                DeselectTetro();
                return;
            }
        }
        foreach (var pos in _currentTetro.GetAllBlockPos())
            _tilemap.SetTile(pos, null);
        _currentTetro.Pos.y--;
        foreach (var pos in _currentTetro.GetAllBlockPos())
            _tilemap.SetTile(pos, _baseBlock);
    }
    public void DeselectTetro()
    {
        _currentTetro = null;
    }
}
