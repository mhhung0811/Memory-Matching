using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer card_background;
    [SerializeField] private SpriteRenderer card_icon;

    [SerializeField] private bool _matched;

    [SerializeField] private int _card_id;

    [SerializeField] private Board board;
    private void Awake()
    {
        this.board = GameObject.Find("StageControl").GetComponent<Board>();
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
        card_background.color = new Color(1f, 1f, 1f, 1f);
        card_icon.color = new Color(1f, 1f, 1f, 1f);
        _matched = false;

    }

    // Xử lí sự kiện cick card sẽ hiển thị card và đổi màu
    public void OnMouseDown()
    {
        card_background.color = new Color(0f, 188f / 255f, 212f / 255f, 1f);
        board.List_Card_Choosen.Add(gameObject.transform);
        board.CheckMatchingCard();
    }   

    // Chuyển màu card khi matched
    public void CardMatched()
    {
        if (!_matched)
        {
            card_background.color = new Color(0f, 191f / 255f, 165f / 255f, 1f);
            _matched = true;
        }
    }

}
