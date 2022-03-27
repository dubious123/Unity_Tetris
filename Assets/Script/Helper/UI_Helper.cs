using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Helper : MonoBehaviour
{
    [SerializeField] public Transform Game_Parent;
    [SerializeField] public GameObject PanelYouLose;
    public void ToggleYouLose()
    {
        Mgr.ResourceEx.Instantiate(PanelYouLose, Game_Parent);
    }
}
