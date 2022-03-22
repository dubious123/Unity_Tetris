using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino
{
    Vector3Int[] _blockPosArr = new Vector3Int[4];
    public Vector3Int Pos = new Vector3Int(0, 23, 0);
    public Vector3Int[] ChildVector = new Vector3Int[3];
    public bool IsFalling = false;
    public Tetromino(Vector3Int[] childPos)
    {
        childPos.CopyTo(ChildVector, 0);
    }
    public Vector3Int[] GetAllBlockPos()
    {
        for(int i = 1; i < 4; i++)
        {
            _blockPosArr[i] = Pos + ChildVector[i-1];
        }
        _blockPosArr[0] = Pos;
        return _blockPosArr;
    }

}
