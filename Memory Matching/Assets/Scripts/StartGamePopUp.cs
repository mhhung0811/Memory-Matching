using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGamePopUp : MonoBehaviour
{
    [SerializeField] private Animator _start_game_popup;

    private readonly int _open = Animator.StringToHash("Open");


    [SerializeField] private TextMeshProUGUI _headline_text;
    [SerializeField] private List<Image> _stars;
    [SerializeField] private TextMeshProUGUI _time_limit_text;
    [SerializeField] private TextMeshProUGUI _step_limit_text;
    [SerializeField] private TextMeshProUGUI _best_time;

    [SerializeField] private Sprite _yellow_star;

    [SerializeField] private int level;
    
    public void SetHeadlineText(int value)
    {
        _headline_text.text = $"Level {value}";
        this.level = value;

    }
    public void SetStar(int value)
    {
        for(int i = 0; i<value;i++)
        {
            _stars[i].sprite = _yellow_star;
        }
    }
    public void SetTimeLimitText(int value) 
    { 
        _time_limit_text.text = $"{value}";
    }

    public void SetStepLimitText(int value)
    {
        _step_limit_text.text = $"{value}";
    }
    public void SetBestTime(int value) 
    { 
        _best_time.text = $"{value}";
    }

    public void SetBoolPopup(bool value)
    {
        _start_game_popup.SetBool(_open, value);
    }

    public void ClickPlay()
    {
        GameManager.Instance.SetCurrentStage(level);
        Debug.Log(GameManager.Instance.GetCurrentStage());
        SceneManager.LoadSceneAsync("CardScene");
    }
}
