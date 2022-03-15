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

    public static Mgr_Scene SceneEx { get { return Instance._scene; } }
    public static Mgr_Resource ResourceEx { get { return Instance._resource; } }
    public static Mgr_Pool PoolEx { get { return Instance._pool; } }

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
        }
    }
}
