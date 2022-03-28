using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Helper : MonoBehaviour
{
    [SerializeField] public Transform Game_Parent;
    [SerializeField] public GameObject PanelYouLose;
    [SerializeField] public TextMeshProUGUI Level;
    [SerializeField] public TextMeshProUGUI HighScore;
    [SerializeField] public TextMeshProUGUI Score;
    public void ToggleYouLose()
    {
        Mgr.ResourceEx.Instantiate(PanelYouLose, Game_Parent);
    }
    public void UpdateLevel(int level)
    {
        Level.text = $"Level : {level}";
    }
    public void UpdateHighScore(int score)
    {
        HighScore.text = $"HighScore:  {score}";

    }
    public void UpdateScore(int score)
    {
        Score.text = $"Score : {score}";

    }
}
