using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour//This is an Parent objects to put DiscardPile
{
    //Pubulic Variables
    public int Numbers; //How many cards in the DiscardPile
    public List<GameObject> Cards = new List<GameObject>(); // The list of cards in DiscardPile


    // Method: Update the Cards in DiscardPile
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
        // Step 1: 先移除所有已经被销毁的卡
        Cards.RemoveAll(card => card == null || card.Equals(null));

        // Step 2: 按照剩下的 Cards 顺序调整 Hierarchy
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] != null && !Cards[i].Equals(null)) // 安全检查
            {
                Cards[i].transform.SetSiblingIndex(i);
            }
        }

        // Step 3: 重建 Cards 列表（保持和 Hierarchy 同步）
        Cards.Clear();
        foreach (Transform child in transform)
        {
            Cards.Add(child.gameObject);
        }
    }

}
