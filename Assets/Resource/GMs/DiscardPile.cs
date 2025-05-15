using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiscardPile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//This is an Parent objects to put DiscardPile
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




    public Vector2 targetPosition = new Vector2(-150, -150);

    void FixedUpdate()
    {
        foreach (Transform child in transform)
        {
            Vector3 newPos = new Vector3(targetPosition.x, targetPosition.y, child.position.z);
            child.position = newPos;
        }
    }


    private Button button;
    public TMP_Text Detail_Text;  // 你想改变的文本


    private void Awake()
    {
        // 获取按钮组件并添加点击事件监听
        Button button = GetComponent<Button>();
        if (Detail_Text == null)
            Detail_Text = GameObject.Find("Detail_Text").GetComponent<TMP_Text>();
    }


    string GetCleanedChildNames()
    {
        string names = "";

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // 删除名字中的 "Prefab"
            string cleanedName = child.name.Replace("Card", "").Replace("(Clone)", "");

            names += cleanedName + "\n";
        }

        return names;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Detail_Text.text = GetCleanedChildNames();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Detail_Text.text = " ---";
    }

}
