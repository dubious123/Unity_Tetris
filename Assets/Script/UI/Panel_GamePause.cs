using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_GamePause : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] public Sprite musicOff;
    [SerializeField] public Sprite musicOn;
    [SerializeField] public Image speaker;
    public void Start()
    {
        slider.value = Mgr.GameEx.Setting.Sound;
    }
    public void OnVolumeChange()
    {
        if(slider.value < 0.01f)
        {
            speaker.sprite = musicOff;
            slider.value = 0;
            AudioListener.volume = 0;
        }
        else
        {
            speaker.sprite = musicOn;
            AudioListener.volume = slider.value;
        }
        Mgr.GameEx.UpdateSetting(slider.value);
    }
    public void Restart()
    {
        Mgr.GameEx.RestartGame();
    }
    public void Quit()
    {
        Mgr.GameEx.QuitToMainMenu();
    }
    public void Close()
    {
        Mgr.InputEx.InvokeAction("Esc", new UnityEngine.InputSystem.InputAction.CallbackContext());
    }
}
