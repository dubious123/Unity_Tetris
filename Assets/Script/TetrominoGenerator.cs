using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class TetrominoGenerator 
{
    public Tetromino GetTetro(TetrominoType type)
    {
        Vector3Int[] ChildPos = new Vector3Int[3];
        switch (type)
        {
            case TetrominoType.I:
                ChildPos.SetValue(new Vector3Int(0,2,0), 0);
                ChildPos.SetValue(new Vector3Int(0,1, 0), 1);
                ChildPos.SetValue(new Vector3Int(0,-1, 0), 2);
                break;                      
            case TetrominoType.O:           
                ChildPos.SetValue(new Vector3Int(-1, 0, 0), 0);
                ChildPos.SetValue(new Vector3Int(-1, -1, 0), 1);
                ChildPos.SetValue(new Vector3Int(0, -1, 0), 2);
                break;                      
            case TetrominoType.T:           
                ChildPos.SetValue(new Vector3Int(-1, 0, 0), 0);
                ChildPos.SetValue(new Vector3Int(0, -1, 0), 1);
                ChildPos.SetValue(new Vector3Int(1, 0, 0), 2);
                break;                      
            case TetrominoType.J:           
                ChildPos.SetValue(new Vector3Int(-1, 0, 0), 0);
                ChildPos.SetValue(new Vector3Int(1, 0, 0), 1);
                ChildPos.SetValue(new Vector3Int(1, -1, 0), 2);
                break;                      
            case TetrominoType.L:           
                ChildPos.SetValue(new Vector3Int(-1, 0, 0), 0);
                ChildPos.SetValue(new Vector3Int(-1, -1, 0), 1);
                ChildPos.SetValue(new Vector3Int(1, 0, 0), 2);
                break;                      
            case TetrominoType.S:           
                ChildPos.SetValue(new Vector3Int(-1, 0, 0), 0);
                ChildPos.SetValue(new Vector3Int(-1, 1, 0), 1);
                ChildPos.SetValue(new Vector3Int(0, -1, 0), 2);
                break;                      
            case TetrominoType.Z:           
                ChildPos.SetValue(new Vector3Int(0, 1, 0), 0);
                ChildPos.SetValue(new Vector3Int(-1, 0, 0), 1);
                ChildPos.SetValue(new Vector3Int(-1, -1, 0), 2);
                break;
        }
        return new Tetromino(ChildPos);
    }
}
