using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer card_background;
    [SerializeField] private SpriteRenderer card_icon;

    [SerializeField] private bool _matched;

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
        this.card_background.color = new Color(1f, 1f, 1f, 1f);
        this.card_icon.color = new Color(1f, 1f, 1f, 1f);
        this._matched = false;

    }

    // Xử lí sự kiện cick card sẽ hiển thị card và đổi màu
    public void OnMouseDown()
    {
        card_background.color = new Color(0f, 188f / 255f, 212f / 255f, 1f);
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
