using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

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
    public bool CanDown(Tilemap tilemap)
    {
        bool result = true;
        Vector3Int[] currentPosArr = GetAllBlockPos();
        Vector3Int temp;
        foreach (var pos in currentPosArr)
        {
            temp = pos;
            temp.y--;
            if (currentPosArr.Contains(temp))
                continue;
            result &= !tilemap.HasTile(temp);
            result &= temp.y >= 0;
            if (!result)
                return result;
        }
        return result;
    }
    public bool CanFall(Tilemap tilemap)
    {
        bool result = true;
        return result;
    }
    public bool CanLeft(Tilemap tilemap)
    {
        bool result = true;
        return result;
    }
    public bool CanRight(Tilemap tilemap)
    {
        bool result = true;
        return result;
    }
    public bool CanRotateR(Tilemap tilemap)
    {
        bool result = true;
        return result;
    }
    public bool CanRoateL(Tilemap tilemap)
    {
        bool result = true;
        return result;
    }

}
