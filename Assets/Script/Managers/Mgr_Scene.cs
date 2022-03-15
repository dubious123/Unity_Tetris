using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using UnityEngine.SceneManagement;

public class Mgr_Scene 
{
    public void MoveScene(SceneType type)
    {
        int desire = (int)type;
        SceneManager.LoadScene(desire);
        string scene_this = SceneManager.GetActiveScene().name;
        string scene_desire = SceneManager.GetSceneByBuildIndex(desire).name;
        Debug.Log($"Loading Scene : {scene_this} -> {scene_desire}");
    }
}
