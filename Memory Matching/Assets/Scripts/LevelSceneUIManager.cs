using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneUIManager : MonoBehaviour
{
    public static LevelSceneUIManager Instance;

    private StartGamePopUp _start_game_pop_up;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one LevelSceneUIManager");
        }
        else
        {
            Instance = this;
        }

        if (_start_game_pop_up != null)
        {
            return;
        }
        _start_game_pop_up = GetComponent<StartGamePopUp>();
    }
    public void BackButton()
    {
        AudioManager.Instance.PlayFX(1);
        SceneManager.LoadSceneAsync("StartScene");
    }
    public void OpenStartLevelPanel(int level, int time_limit, int step_limit, int star, int best_time)
    {
        AudioManager.Instance.PlayFX(1);
        _start_game_pop_up.SetHeadlineText(level);
        _start_game_pop_up.SetTimeLimitText(time_limit);
        _start_game_pop_up.SetTimeLimitText(step_limit);
        _start_game_pop_up.SetStar(star);
        _start_game_pop_up.SetBestTime(best_time);
        _start_game_pop_up.SetBoolPopup(true);
    }
    public void CloseStartLevelPanel()
    {
        AudioManager.Instance.PlayFX(1);
        _start_game_pop_up.SetBoolPopup(false);
    }
}
