using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapButton : MonoBehaviour
{
    [Header("Level of Button")]
    [SerializeField] private int _level;

    [Header("Button")]
    [SerializeField] private Image _button;

    [Header("List Image Star of Button")]
    [SerializeField] private List<Image> _star;

    [Header("Unplayed Button Normal Level Sprite")]
    [SerializeField] private Sprite _unplayed_button;

    [Header("Unplayed Button Elite Level Sprite")]
    [SerializeField] private Sprite _unplayed_button_elite;

    [Header("Current Level Button Sprite")]
    [SerializeField] private Sprite _blue_button;

    [Header("Curent Elite Level Button Sprite")]
    [SerializeField] private Sprite _red_button;

    [Header("Yellow Star")]
    [SerializeField] private Sprite _yellow_star;

    [Header("Pink Level Text")]
    [SerializeField] private TextMeshProUGUI _pink_level_text;

    [Header("Blue Level Text")]
    [SerializeField] private TextMeshProUGUI _blue_level_text;

    [Header("Shining Animation")]
    [SerializeField] private Image _shining_animation;

    private int player_level = 1;

    private int num_star = 0;

    enum LevelType
    {
        Normal,
        Elite
    }
    [SerializeField] private LevelType _level_type;

    

    private void Start()
    {
        this.LoadComponent();
    }
    public void ClickButton()
    {
        if(player_level < _level)
        {
            return;
        }
        Debug.Log("Use Level " + _level);
        
        LevelSceneUIManager.Instance.OpenStartLevelPanel(_level, 120, 30, num_star , 30);
    }
    public void LoadComponent()
    {

        // Đoạn lệnh tiếp theo cần lấy level của người chơi là set trạng thái cho các button
        player_level = SaveManager.NextLevel();
        num_star = SaveManager.LoadStar(_level);

        _shining_animation.gameObject.SetActive(false);
        _blue_level_text.text = _pink_level_text.text =  $"{_level}";

        if (player_level > _level)
        {
            if (_level_type == LevelType.Elite)
            {
                _button.sprite = _red_button;
            }
            else
            {
                _button.sprite = _blue_button;
            }
            _blue_level_text.gameObject.SetActive(true);
            _pink_level_text.gameObject.SetActive(false);
            for (int i = 0; i < num_star; i++)
            {
                _star[i].sprite = _yellow_star;
            }


        }
        else if (player_level < _level)
        {
            if (_level_type == LevelType.Elite)
            {
                _button.sprite = _unplayed_button_elite;
            }
            else
            {
                _button.sprite = _unplayed_button;
            }

            _blue_level_text.gameObject.SetActive(false);
            _pink_level_text.gameObject.SetActive(true);
            foreach (Image star in _star)
            {
                star.gameObject.SetActive(false);
            }

        }
        else
        {
            if (_level_type == LevelType.Elite)
            {
                _button.sprite = _red_button;
            }
            else
            {
                _button.sprite = _blue_button;
            }
            _blue_level_text.gameObject.SetActive(true);
            _pink_level_text.gameObject.SetActive(false);
            _shining_animation.gameObject.SetActive(true);
            foreach (Image star in _star)
            {
                star.gameObject.SetActive(true);
            }
        }
    }
}
