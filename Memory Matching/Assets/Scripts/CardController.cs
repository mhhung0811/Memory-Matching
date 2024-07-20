using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private Image card_background;
    [SerializeField] private Image card_icon;    

    [SerializeField] private bool _matched;

    [SerializeField] private int _card_id;

    [SerializeField] private Board board;

    private Animator animator;

    private Color color_normal;
    private Color color_selected;
    private Color color_matched;

    private bool isSelected;
    private void Awake()
    {
        //this.board = GameObject.Find("StageControl").GetComponent<Board>();
    }
    public int CardID
    {
        get { return _card_id; }
        set { _card_id = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.LoadComponent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadComponent()
    {
        color_normal = new Color(1f, 1f, 1f, 1f);
        color_selected = new Color(0f, 188f / 255f, 212f / 255f, 1f);
        color_matched = new Color(0f, 191f / 255f, 165f / 255f, 1f);

        isSelected = false;

        card_background.color = color_normal;
        card_icon.color = color_normal;
        _matched = false;

        board = FindFirstObjectByType<Board>().GetComponent<Board>();
        animator = GetComponent<Animator>();        
    }

    // Xử lí sự kiện cick card sẽ hiển thị card và đổi màu
    public void ClickCard()
    {
        Debug.Log("Click Card");
        if (InGameManager.Instance.isPaused) return;
        if (board.isExcuteCard) return;
        if (isSelected) return;
        StartFlipUp();
        

    }   

    // Chuyển màu card khi matched
    public void CardMatched()
    {
        if (!_matched)
        {
            card_background.color = color_matched;
            _matched = true;
        }
    }

    public void StartFlipDown()
    {
        isSelected = false;
        animator.SetBool("isDown", true);
    }

    public void IsFlipDown()
    {
        card_background.color = color_normal;
        card_icon.color = color_normal;
        _matched = false;
    }

    public void EndFlipDown()
    {
        animator.SetBool("isDown", false);
    }

    public void StartFlipUp()
    {
        isSelected = true;
        animator.SetBool("isUp", true);
    }

    public void IsFlipUp()
    {
        card_background.color = color_selected;
        board.List_Card_Choosen.Add(gameObject.transform);
        board.CheckMatchingCard();
    }

    public void EndFlipUp()
    {
        animator.SetBool("isUp", false);
    }
}
