using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Helper : MonoBehaviour
{
    [SerializeField] public Transform Game_Parent;
    [SerializeField] public GameObject PanelYouLose;
    [SerializeField] public GameObject PanelNewScore;
    [SerializeField] public GameObject PanelPause;
    [SerializeField] public TextMeshProUGUI Level;
    [SerializeField] public TextMeshProUGUI HighScore;
    [SerializeField] public TextMeshProUGUI Score;
    Stack<GameObject> uiStack = new Stack<GameObject>();
    public void YouLose()
    {
        uiStack.Push(Mgr.ResourceEx.Instantiate(PanelYouLose, Game_Parent));
    }
    public void NewScore(int score)
    {
        var go = Mgr.ResourceEx.Instantiate(PanelNewScore, Game_Parent);
        go.GetComponent<Panel_NewScore>().Init(score);
        uiStack.Push(go);
    }
    public void Pause()
    {
        uiStack.Push(Mgr.ResourceEx.Instantiate(PanelPause, Game_Parent));
    }
    public void Close(GameObject go)
    {
        if (uiStack.Peek() == go)
            Close();
    }
    public void Close()
    {
        Mgr.ResourceEx.Destroy(uiStack.Pop());  
    }
    public void CloseAll()
    {
        while(uiStack.Count > 0)
            Mgr.ResourceEx.Destroy(uiStack.Pop());
    }
    public void UpdateLevel(int level)
    {
        Level.text = $"Level : {level}";
    }
    public void UpdateHighScore(int score)
    {
        HighScore.text = $"HighScore:  {score}  {Mgr.GameEx.SaveGame.Record[0].Name}";

    }
    public void UpdateScore(int score)
    {
        Score.text = $"Score : {score}";
    }

}
