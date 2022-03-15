using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Scene_Main : MonoBehaviour
{
    public void StartGame()
    {
        Mgr.SceneEx.MoveScene(SceneType.Game);
    }
}
