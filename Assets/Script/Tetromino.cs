using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;
using static UnityEngine.InputSystem.InputAction;

public class Tetromino
{
    Vector3Int[] _blockPosArr = new Vector3Int[4];
    Vector3 _vectorOffset = new Vector3(-0.5f, -0.5f, 0);
    public Vector3Int Pos = new Vector3Int(0, 24, 0); 
    public Vector3[] ChildVector = new Vector3[4];
    public bool IsFalling = false;
    public TileBase MyTile;
    public TetrominoType TetroType;
    public Tetromino(Vector3[] childPos, TetrominoType type)
    {
        childPos.CopyTo(ChildVector, 0);
        TetroType = type;
    }
    public Vector3Int[] GetAllBlockPos()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3Int v = Vector3Int.CeilToInt(ChildVector[i] + _vectorOffset);
            _blockPosArr[i] = Pos + v;
        }
        return _blockPosArr;
    }
    //public Vector3Int[] GetAllBlockPos()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        Vector3Int v = Mgr.GameEx.GHelper.Map.WorldToCell(ChildVector[i]);
    //        _blockPosArr[i] = Pos + v;
    //    }
    //    return _blockPosArr;
    //}
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
    public int CanFall(Tilemap tilemap)
    {
        Vector3Int[] currentPosArr = GetAllBlockPos();
        List<Vector3Int> list = new List<Vector3Int>();
        foreach (var pos in currentPosArr)
        {
            var temp = pos;
            temp.y--;
            if (temp.y <= 0)
                return 0;
            if (currentPosArr.Contains(temp))
                continue;
            list.Add(pos);
        }
        for (int i = 1; ;i++)
        {
            foreach (var pos in list)
            {
                var temp = pos;
                temp.y -= i;
                if (tilemap.HasTile(temp) || temp.y <0)
                    return i - 1; 
            }
        }
    }
    public bool CanLeft(Tilemap tilemap)
    {
        Vector3Int[] currentPosArr = GetAllBlockPos();
        foreach (var pos in currentPosArr)
        {
            var temp = pos;
            temp.x--;
            if (currentPosArr.Contains(temp))
                continue;
            if (temp.x < 0 || tilemap.HasTile(temp))
                return false;
        }
        return true;
    }
    public bool CanRight(Tilemap tilemap)
    {
        Vector3Int[] currentPosArr = GetAllBlockPos();
        foreach (var pos in currentPosArr)
        {
            var temp = pos;
            temp.x++;
            if (currentPosArr.Contains(temp))
                continue;
            if (temp.x > 11 || tilemap.HasTile(temp))
                return false;
        }
        return true;
    }
    public bool CanRotateR(Tilemap tilemap)
    {
        Vector3Int[] currentPosArr = GetAllBlockPos();
        for (int i = 0; i<4; i++)
        {
            Vector3 v = ChildVector[i];
            float t = v.x;
            v.x = v.y;
            v.y = -t;
            v += Pos;

            var v2 = Vector3Int.CeilToInt(v + _vectorOffset);
            if (currentPosArr.Contains(v2))
                continue;
            if (!IsInBound(v2) || tilemap.HasTile(v2))
                return false;
        }
        return true;
    }
    public bool CanRotateL(Tilemap tilemap)
    {
        Vector3Int[] currentPosArr = GetAllBlockPos();
        for (int i = 0; i < 4; i++)
        {
            Vector3 v = ChildVector[i];
            float t = v.x;
            v.x = -v.y;
            v.y = t;
            v += Pos;

            var v2 = Vector3Int.CeilToInt(v + _vectorOffset);
            if (currentPosArr.Contains(v2))
                continue;
            if (!IsInBound(v2) || tilemap.HasTile(v2))
                return false;
        }
        return true;
    }
    public bool IsInBound(Vector3Int pos)
    {
        return
            pos.x >= 0 &&
            pos.x <= 11 &&
            pos.y >= 0 ;
    }
}
