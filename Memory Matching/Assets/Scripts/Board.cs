using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using TMPro;

public class Board : MonoBehaviour
{
    [Header("Level Configs")]
    [SerializeField] protected List<LevelConfig> all_level_configs;

    [Header("Curent Level")]
    [SerializeField] protected int curent_level;

    [Header("Board Size")]
    [SerializeField] protected int row;
    [SerializeField] protected int col;

    [Header("Board Coordinate")]
    [SerializeField] protected float start_x = 0;
    [SerializeField] protected float start_y = 0;
    [SerializeField] protected float offset_x = 6f;
    [SerializeField] protected float offset_y = 6f;

    [Header("Card Prefab")]
    [SerializeField] protected Transform card_prefab;

    [Header("All Card Configs")]
    [SerializeField] protected List<Card> card_configs;

    [Header("Card Id In Configs")]
    [SerializeField] protected List<int> list_card_ids;

    [Header("Card Used In Game")]
    [SerializeField] protected List<Transform> list_card_choosen;

    public bool isExcuteCard
    {
        get; private set;
    }

    public List<Transform> List_Card_Choosen
    {
        get { return list_card_choosen; }
        set { list_card_choosen = value;}
    }

    public void Start()
    {
        LoadComponent();
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

        float cord_x = start_x;
        float cord_y = start_y;


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
                Transform obj = Instantiate(card_prefab);
                obj.gameObject.GetComponent<CardController>().CardID = randomList[0];
                // Thay đổi sprite của GameObject con
                Transform childTransform = obj.transform.GetChild(0); // Lấy GameObject con đầu tiên
                SpriteRenderer spriteRenderer = childTransform.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = card_configs[randomList[0]].card_image;
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on child GameObject.");
                }
                randomList.RemoveAt(0);
                obj.gameObject.SetActive(true);
                obj.position = new Vector2(cord_x, cord_y);
                cord_x += offset_x;
            }
            cord_x = start_x; // Reset lại vị trí x sau mỗi hàng
            cord_y += offset_y;
        }
    }

    public void CheckMatchingCard()
    {
        if(list_card_choosen.Count() < 2)
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
                    //card.gameObject.GetComponent<CardController>().CardMatched();
                }
            }
            else
            {
                foreach (Transform card in list_card_choosen)
                {
                    StartCoroutine(ExecuteCard(card, 1));
                    //card.gameObject.GetComponent<CardController>().LoadComponent();
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
}