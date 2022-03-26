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
        //Vector3Int[] newPos = ((Vector3Int[])ChildVector.Clone()).ForEach(
        //    (Vector3Int v) =>
        //    {
        //        int t = v.x;
        //        v.x = v.y;
        //        v.y = -v.x;
        //        return v;
        //    });
        //foreach (var pos in currentPosArr)
        //{
        //    var temp = pos;
        //    temp.x++;
        //    if (currentPosArr.Contains(temp))
        //        continue;
        //    if (temp.x > 11 || tilemap.HasTile(temp))
        //        return false;
        //}
        return true;
    }
    public bool CanRoateL(Tilemap tilemap)
    {
        bool result = true;
        return result;
    }

}
