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
        Mgr.GameEx.SaveNewRecord(_name.text);
        Mgr.UIEx.Close();
    }
}
