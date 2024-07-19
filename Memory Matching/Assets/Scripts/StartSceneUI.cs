using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Image _setting_button;
    [SerializeField] private Image _setting_popup;
    [SerializeField] private Toggle _toggle_male;
    [SerializeField] private Toggle _toggle_female;
    [SerializeField] private Slider _slider_sound;
    [SerializeField] private Slider _slider_music;

    [SerializeField] private Animator _setting_popup_animator;

    private readonly int _open = Animator.StringToHash("Open");

    public void PlayGame()
    {
        AudioManager.Instance.PlayFX(1);
        SceneManager.LoadSceneAsync("LevelScene");
    }

    public void OpenSettingPopup() 
    {
        AudioManager.Instance.PlayFX(1);
        Debug.Log("Setting popup");
        SetBoolPopup(true);
        _setting_button.gameObject.SetActive(false);
        /*_setting_popup.gameObject.SetActive(true);*/


    }
    public void CloseSettingPopup()
    {
        AudioManager.Instance.PlayFX(1);
        SetBoolPopup(false);
        _setting_button.gameObject.SetActive(true);
        /*_setting_popup.gameObject.SetActive(false);*/
    }
    public void ChangeGender(int value)
    {
        if(value == 0)
        {
            _toggle_female.isOn = false;
        }
        else if(value == 1) 
        {
            _toggle_male.isOn=false;
        }
    }
    public void ChangeVolumeBGM()
    {
        AudioManager.Instance.audioSourceBGM.volume = _slider_music.value;
    }
    public void ChangeVolumeFX()
    {
        AudioManager.Instance.audioSourceFX.volume = _slider_sound.value;
    }
    
    public void SetBoolPopup(bool value)
    {
        _setting_popup_animator.SetBool(_open, value); 
    }
}

