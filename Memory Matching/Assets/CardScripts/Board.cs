using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    [SerializeField] private int row = 4;
    [SerializeField] private int col = 4;

    [SerializeField] private Transform card_prefab;

    [SerializeField] private float start_x = 0;
    [SerializeField] private float start_y = 0;
    [SerializeField] private float offset_x = 6f;
    [SerializeField] private float offset_y = 6f;

    [SerializeField] private List<Card> card_configs;
    [SerializeField] private List<int> list_card_ids;

    private void Start()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        // Lấy config của tất cả card;
        this.card_configs = GameManager.Instance.GetCardConfigs().card_configs;

        if (card_configs == null )
        {
            return;
        }

        float cord_x = start_x;
        float cord_y = start_y;

        
        foreach(Card card in card_configs)
        {
            list_card_ids.Add(card.card_id);
        }
        
        if(list_card_ids == null )
        {
            return;
        }

        // Tạo một đối tượng Random
        System.Random random = new System.Random();
        List<int> randomList = new List<int>();

        for (int i = 0; i < 8 && list_card_ids.Count > 0; i++)
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
    // Hàm xáo trộn danh sách
    void ShuffleCard(List<int> list)
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

}
