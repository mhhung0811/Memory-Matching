using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private Board board;

    [Header("Counter")]
    [SerializeField] private TextMeshProUGUI moveCounterText;
    private int moveCounter;
    [SerializeField] private TextMeshProUGUI timeCounterText;
    private int timeCounter;
    [SerializeField] private TextMeshProUGUI starCounterText;
    private int starCounter;

    private LevelConfig levelInfo;

    [Header("UI control")]
    [SerializeField] private Button replayButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private CanvasRenderer pausePanel;
    [SerializeField] private Button continueButton;

    private float timer;
    public bool isPaused
    {
        get; private set;
    }

    public static InGameManager Instance;
    private void Awake()
    {
        if (Instance == null)
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
        if (board != null) levelInfo = GameManager.Instance.CurrentLevelConfig();
        moveCounter = levelInfo.number_of_moves;
        timeCounter = (int)levelInfo.time_limit;
        starCounter = 3;
        timer = 0;
        moveCounterText.text = moveCounter.ToString();
        TimeCountDown();
        starCounterText.text = "3";

        replayButton.onClick.AddListener(Replay);
        pauseButton.onClick.AddListener(Pause);
        pausePanel.gameObject.SetActive(false);
        continueButton.onClick.AddListener(Continue);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            TimeCountDown();
        }
    }

    public void MoveCount()
    {
        if (levelInfo.number_of_moves > 0)
        {
            moveCounter--;
            if (moveCounter == levelInfo.number_of_moves_2) StarCount(2);
            else if (moveCounter == levelInfo.number_of_moves_1) StarCount(1);
            else if (moveCounter == levelInfo.number_of_moves_0) StarCount(0);
        }
        else
        {
            moveCounter++;
        }
        moveCounterText.text = moveCounter.ToString();
    }

    public void TimeCountDown()
    {
        timeCounter--;
        string unit;
        if (timeCounter % 60 > 9)
        {
            unit = (timeCounter % 60).ToString();
        }
        else
        {
            unit = "0" + (timeCounter % 60).ToString();
        }
        timeCounterText.text = $"{timeCounter / 60}:{unit}";
        if (timeCounter == (int)levelInfo.time_2) StarCount(2);
        else if (timeCounter == (int)levelInfo.time_1) StarCount(1);
        else if (timeCounter == (int)levelInfo.time_0) StarCount(0);
    }

    public void StarCount(int star)
    {
        Debug.Log("count star");
        if (starCounter >= star)
        {
            starCounter = star;
            starCounterText.text = starCounter.ToString();
        }
    }

    public void Replay()
    {
        if (isPaused) return;
        moveCounter = 0;
        timeCounter = 0;
        starCounter = 3;
        timer = 0;
        moveCounterText.text = "0";
        timeCounterText.text = "0:00";
        starCounterText.text = "3";

        board.Replay();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
    }

    public void Continue()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }
}