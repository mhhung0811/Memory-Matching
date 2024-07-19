using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _all_level_configs;
    private void Start()
    {
        this.LoadComponent();
    }
    public void Back()
    {
        Debug.Log("Backed");
    }
    public void ChooseLevel(int level)
    {
        GameManager.Instance.SetCurrentStage(level);
        Debug.Log(GameManager.Instance.GetCurrentStage());
        SceneManager.LoadSceneAsync("CardScene");
    }
    private void LoadComponent()
    {
        this._all_level_configs = GameManager.Instance.GetLevelConfigs().all_level_configs;
    }
}
