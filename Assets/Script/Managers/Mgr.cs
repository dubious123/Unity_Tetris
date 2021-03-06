using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mgr : MonoBehaviour
{
    static Mgr _instance;
    static Mgr Instance { get { Init(); return _instance; } }

    Mgr_Scene _scene = new Mgr_Scene();
    Mgr_Resource _resource = new Mgr_Resource();
    Mgr_Pool _pool = new Mgr_Pool();
    Mgr_Game _game = new Mgr_Game();
    Mgr_Input _input = new Mgr_Input();
    Mgr_Data _data = new Mgr_Data();
    Mgr_UI _ui = new Mgr_UI();
    public static Mgr_Scene SceneEx { get { return Instance._scene; } }
    public static Mgr_Resource ResourceEx { get { return Instance._resource; } }
    public static Mgr_Pool PoolEx { get { return Instance._pool; } }
    public static Mgr_Game GameEx { get { return Instance._game; } }
    public static Mgr_Input InputEx { get { return Instance._input; } }
    public static Mgr_Data DataEx { get { return Instance._data; } }
    public static Mgr_UI UIEx { get { return Instance._ui; } }

    static public void Init()
    {
        if(_instance == null)
        {
            try
            {
                GameObject.Find("@Managers");
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
            GameObject go = new GameObject("@Managers");
            _instance = go.AddComponent<Mgr>();
            DontDestroyOnLoad(go);
            _instance._input.Init();
            _instance._ui.Init();
        }
    }
}
