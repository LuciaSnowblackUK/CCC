using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour //This is an Parent objects to put HandCards
{
    //Pubulic Variables
    public int Numbers; //How many cards in Hand
    public List<GameObject> Cards = new List<GameObject>(); // The list of cards in Hand



    // Method: Update the Cards in Deck
    public List<GameObject> UpdateCards()
    {
        Cards.Clear();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Cards.Add(this.transform.GetChild(i).gameObject);
        }

        return Cards;
    }

    //Method:ResetOrder
    public void ResetOrder()
    {
        // Step 1: 按照 Cards 列表的顺序更新 Hierarchy 顺序
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.SetSiblingIndex(i);
        }

        // Step 2: 根据 Hierarchy 顺序更新 Cards 列表
        Cards.Clear();
        foreach (Transform child in transform)
        {
            Cards.Add(child.gameObject);
        }
    }

    private void Update()
    {
        UpdateCards();
        ArrangeCardsLinear();
    }

    // Method: Linearly arrange cards between two positions
    public void ArrangeCardsLinear()
    {
        //UpdateCards(); // 确保 Cards 列表是最新的

        int count = Cards.Count;
        if (count == 0) return;

        // 最大排列范围的两端位置
        Vector2 startPos = new Vector2(6.1f, 0.45f);
        Vector2 endPos = new Vector2(-6.1f, 0.45f);

        // 计算最大间距
        float maxSpacing = (startPos.x - endPos.x) / (10 - 1); // 假设最大数量为 10
        float spacing = maxSpacing;

        // 计算偏移量，使卡牌居中
        float offset = 0f;
        if (count < 10)  // 只有卡牌数小于最大数时才需要居中
        {
            // 总间距 - 实际间距 = 偏移量
            float totalSpacing = (count - 1) * spacing;  // 实际的间距总和
            offset = (startPos.x - endPos.x - totalSpacing) / 2; // 偏移量就是剩余空间的一半
        }

        // 进行位置计算和排列
        for (int i = 0; i < count; i++)
        {
            // 计算卡牌的 X 坐标，使用最大间距并考虑偏移
            float xPos = startPos.x - i * spacing - offset;
            Vector2 pos = new Vector2(xPos, startPos.y);

            Cards[i].transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        }
    }





}
