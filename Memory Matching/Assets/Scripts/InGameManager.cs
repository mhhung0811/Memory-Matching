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
        moveCounter = 0;
        timeCounter = 0;
        starCounter = 3;
        timer = 0;
        moveCounterText.text = "0";
        timeCounterText.text = "0:00";
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
            AddTimeCount();
        }
    }

    public void AddMoveCount()
    {
        moveCounter++;
        moveCounterText.text = moveCounter.ToString();
        if (moveCounter > 15) RemoveStarCount();
    }

    public void AddTimeCount()
    {
        timeCounter++;
        
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

        if (timeCounter == 60) RemoveStarCount();
        if (timeCounter == 120) RemoveStarCount();
    }

    public void RemoveStarCount()
    {
        if (starCounter > 0)
        {
            starCounter--;
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