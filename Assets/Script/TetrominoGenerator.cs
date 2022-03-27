using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class TetrominoGenerator 
{
    public Tetromino GetTetro(TetrominoType type)
    {
        Vector3[] ChildPos = new Vector3[4];
        switch (type)
        {
            case TetrominoType.I:
                ChildPos.SetValue(new Vector3(0.5f,0.5f,0), 0);
                ChildPos.SetValue(new Vector3(0.5f,1.5f, 0), 1);
                ChildPos.SetValue(new Vector3(0.5f,-0.5f, 0), 2);
                ChildPos.SetValue(new Vector3(0.5f, -1.5f, 0), 3);
                break;                      
            case TetrominoType.O:
                ChildPos.SetValue(new Vector3(-0.5f, 0.5f, 0), 0);
                ChildPos.SetValue(new Vector3(0.5f, 0.5f, 0), 1);
                ChildPos.SetValue(new Vector3(-0.5f, -0.5f, 0), 2);
                ChildPos.SetValue(new Vector3(0.5f, -0.5f, 0), 3);
                break;                      
            case TetrominoType.T:
                ChildPos.SetValue(new Vector3(-1.0f, 0, 0), 0);
                ChildPos.SetValue(new Vector3(1.0f, 0, 0), 1);
                ChildPos.SetValue(new Vector3(0, -1.0f, 0), 2);
                ChildPos.SetValue(new Vector3(0, 0, 0), 3);
                break;                      
            case TetrominoType.J:
                ChildPos.SetValue(new Vector3(-0.5f, 0.5f, 0), 0);
                ChildPos.SetValue(new Vector3(0.5f, 0.5f, 0), 1);
                ChildPos.SetValue(new Vector3(1.5f, -0.5f, 0), 2);
                ChildPos.SetValue(new Vector3(1.5f, 0.5f, 0), 3);
                break;                      
            case TetrominoType.L:
                ChildPos.SetValue(new Vector3(0.5f, 0.5f, 0), 0);
                ChildPos.SetValue(new Vector3(1.5f, 0.5f, 0), 1);
                ChildPos.SetValue(new Vector3(-0.5f, 0.5f, 0), 2);
                ChildPos.SetValue(new Vector3(-0.5f, -0.5f, 0), 3);
                break;                      
            case TetrominoType.S:
                ChildPos.SetValue(new Vector3(-0.5f, 0.5f, 0), 0);
                ChildPos.SetValue(new Vector3(-0.5f, 1.5f, 0), 1);
                ChildPos.SetValue(new Vector3(0.5f, 0.5f, 0), 2);
                ChildPos.SetValue(new Vector3(0.5f, -0.5f, 0), 3);
                break;                      
            case TetrominoType.Z:
                ChildPos.SetValue(new Vector3(0.5f, 0.5f, 0), 0);
                ChildPos.SetValue(new Vector3(0.5f, 1.5f, 0), 1);
                ChildPos.SetValue(new Vector3(-0.5f, 0.5f, 0), 2);
                ChildPos.SetValue(new Vector3(-0.5f, -0.5f, 0), 3);
                break;
        }
        return new Tetromino(ChildPos);
    }
}
