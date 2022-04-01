using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_NewScore : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _score;
    [SerializeField] public TextMeshProUGUI _name;

    public void Init(int score)
    {
        _score.text = $"{score}";
    }
    public void SaveNewScore()
    {
        Debug.Log($"name : {_name.text}, score : {_score.text}");
        Mgr.DataEx.Save(_name.text, int.Parse(_score.text));
    }

}
