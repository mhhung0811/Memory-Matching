using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [Header("Level Configs")]
    [SerializeField] protected List<LevelConfig> all_level_configs;

    [Header("Curent Level")]
    [SerializeField] protected int curent_level;

    [Header("Board Size")]
    [SerializeField] protected int row;
    [SerializeField] protected int col;

    [Header("Card Prefab")]
    [SerializeField] protected RectTransform card_prefab;

    [Header("All Card Configs")]
    [SerializeField] protected List<Card> card_configs;

    [Header("Card Id In Configs")]
    [SerializeField] protected List<int> list_card_ids;

    [Header("Card Used In Game")]
    [SerializeField] protected List<Transform> list_card_choosen;

    [Header("Board Coordinate")]
    [SerializeField] protected float start_x = 0;
    [SerializeField] protected float start_y = 0;
    [SerializeField] protected float offset_x = 1.2f; // Mặc định khoảng cách giữa các thẻ
    [SerializeField] protected float offset_y = 1.2f; // Mặc định khoảng cách giữa các thẻ

    public bool isExcuteCard { get; private set; }

    public List<Transform> List_Card_Choosen
    {
        get { return list_card_choosen; }
        set { list_card_choosen = value; }
    }

    public Image uiImage;

    [SerializeField] private GridLayoutGroup _grid_layout_group;

    public void Start()
    {
        LoadComponent();
        ConfigGridLayoutGroup();
        InitBoard();
    }

    public void LoadComponent()
    {
        isExcuteCard = false;

        all_level_configs = GameManager.Instance.GetLevelConfigs().all_level_configs;
        curent_level = GameManager.Instance.GetCurrentStage();

        if (all_level_configs != null)
        {
            foreach (LevelConfig config in all_level_configs)
            {
                if (config.level == curent_level)
                {
                    Debug.Log(config.row + "/" + config.col);
                    this.row = config.row;
                    this.col = config.col;
                }
            }
        }
        Debug.Log(row + "/" + col);
    }

    // Hàm xáo trộn danh sách
    public void ShuffleCard(List<int> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public void InitBoard()
    {
        // Lấy config của tất cả card;
        this.card_configs = GameManager.Instance.GetCardConfigs().card_configs;

        if (card_configs == null)
        {
            return;
        }

        foreach (Card card in card_configs)
        {
            list_card_ids.Add(card.card_id);
        }

        if (list_card_ids == null)
        {
            return;
        }

        int num_pair_card = (row * col) / 2;

        // Tạo một đối tượng Random
        System.Random random = new System.Random();
        List<int> randomList = new List<int>();

        for (int i = 0; i < num_pair_card && list_card_ids.Count > 0; i++)
        {
            int randomIndex = random.Next(list_card_ids.Count);
            randomList.Add(list_card_ids[randomIndex]);
            randomList.Add(list_card_ids[randomIndex]);
            list_card_ids.RemoveAt(randomIndex);
        }

        this.ShuffleCard(randomList);


        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                RectTransform obj = Instantiate(card_prefab);
                obj.transform.SetParent(_grid_layout_group.gameObject.transform, false);
                obj.gameObject.GetComponent<CardController>().CardID = randomList[0];

                // Lấy Transform của GameObject con
                Transform childTransform = obj.transform.GetChild(0);

                // Lấy Image từ GameObject con
                Image imageComponent = childTransform.GetComponent<Image>();

                // Kiểm tra nếu Image component tồn tại
                if (imageComponent != null)
                {
                    // Thay đổi sprite của GameObject con
                    imageComponent.sprite = card_configs[randomList[0]].card_image;
                }
                else
                {
                    Debug.LogError("Image component không được tìm thấy trên GameObject con.");
                }
                randomList.RemoveAt(0);
            }
        }
    }

    public void CheckMatchingCard()
    {
        if (list_card_choosen.Count() < 2)
        {
            return;
        }
        if (list_card_choosen.Count() == 2)
        {
            int card_1 = list_card_choosen[0].gameObject.GetComponent<CardController>().CardID;
            int card_2 = list_card_choosen[1].gameObject.GetComponent<CardController>().CardID;
            isExcuteCard = true;
            if (card_1 == card_2)
            {
                foreach (Transform card in list_card_choosen)
                {
                    StartCoroutine(ExecuteCard(card, 0));
                }
            }
            else
            {
                foreach (Transform card in list_card_choosen)
                {
                    StartCoroutine(ExecuteCard(card, 1));
                }
            }
            list_card_choosen.Clear();

            InGameManager.Instance.MoveCount();
        }
    }

    IEnumerator ExecuteCard(Transform obj, int code)
    {
        yield return new WaitForSeconds(1f);
        if (code == 0)
        {
            Destroy(obj.gameObject);
          // Sửa lỗi đoạn này 
            //obj.gameObject.GetComponent<CardController>().CardMatched();
            InGameManager.Instance.DeleteCard();

        }
        else if (code == 1)
        {
            CardController cardController = obj.GetComponent<CardController>();
            cardController.StartFlipDown();
        }
        isExcuteCard = false;
    }

    public void Replay()
    {
        GameObject[] list_card = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject obj in list_card)
        {
            if (obj) Destroy(obj.gameObject);
        }
        InitBoard();
    }


    public Vector2 GetGridLayoutSize()
    {
        RectTransform rectTransform = uiImage.GetComponent<RectTransform>();
        
        if (uiImage != null)
        {
            Vector2 imageSize = rectTransform.rect.size;
            Debug.Log("Kích thước của UI Image: " + imageSize);
        }
        else
        {
            Debug.LogError("UI Image chưa được gán!");
        }
        return rectTransform.rect.size;
    }
    public void ConfigGridLayoutGroup()
    {
        float devide = Mathf.Max(row, col);
        Vector2 grid_size = GetGridLayoutSize();
        
        float cell_size = grid_size.x / (devide + (devide + 2) * 1/3);
        float space = cell_size * 1 / 3;

        _grid_layout_group.cellSize = new Vector2(cell_size, cell_size);
        _grid_layout_group.spacing = new Vector2(space, space);
        _grid_layout_group.constraintCount = col;
    }

    

}
