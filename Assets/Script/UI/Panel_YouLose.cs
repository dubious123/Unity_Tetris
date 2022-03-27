using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_YouLose : MonoBehaviour
{
    public void Restart()
    {
        Mgr.GameEx.RestartGame();
    }
    public void Quit()
    {
        Mgr.GameEx.QuitToMainMenu();
    }
}
