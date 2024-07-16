using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Card Configs")]
    [SerializeField] private Cards card_configs;

    [Header("Level configs")]
    [SerializeField] private LevelConfigs _all_level_configs;

    [Header("Current Stage")]
    [SerializeField] private int current_stage = 1;
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Lấy config của tất cả card
    public Cards GetCardConfigs()
    {
        return card_configs;
    }
    public int GetCurrentStage()
    {
        return current_stage;
    }
    public void SetCurrentStage(int stage) 
    { 
        this.current_stage = stage;
    }

    public LevelConfigs GetLevelConfigs()
    {
        return _all_level_configs;
    }
}
