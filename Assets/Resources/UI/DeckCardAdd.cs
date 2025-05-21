using TMPro;
using UnityEngine;
using static GM_Global;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

[RequireComponent(typeof(Button))]
public class DeckCardAdd : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    GameObject Card;
    Card ThisCard;
    int CardID;
    // 获取子物体
    Transform CardCount;

    // 获取孙物体
    Transform CardCountNumber;

    public TMP_Text Detail_Text;  // 你想改变的文本
    public TMP_Text CardNumber;  // 你想改变的文本


    void Start()
    {

        if (Detail_Text == null)
            Detail_Text = GameObject.Find("Detail_Text").GetComponent<TMP_Text>();

        Card = transform.parent.gameObject;
        ThisCard = Card.GetComponent<Card>();
        CardID = ThisCard.ID;
        CardCount = transform.GetChild(0);
        CardCountNumber = transform.GetChild(0).GetChild(0);
        CardNumber = CardCountNumber.GetComponent<TMP_Text>();
    }

    private void Awake()
    {
        // 获取按钮组件并添加点击事件监听
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // 更新字典中的卡牌数量并获取新值
        string countStr = DeckLoader.Instance.ToggleCardCount(CardID);

        // 显示到文本上
        CardNumber.text = countStr;

        // 控制显示/隐藏
        switch (countStr)
        {
            case "0":
                CardCount.gameObject.SetActive(false);
                break;

            case "1":
            case "2":
                CardCount.gameObject.SetActive(true);
                break;
        }

        // 其他操作可以在此添加…
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Detail_Text.text = ThisCard.CardDiscription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Detail_Text.text = DeckLoader.Instance.GetDeckListAsText(); //改成显示现有卡组列表

    }

    public void RefreshCardCount()
    {
        string countStr = DeckLoader.Instance.GetCardCountAsString(CardID); // You need a method like this
        CardNumber.text = countStr;
    
        switch (countStr)
        {
            case "0":
                CardCount.gameObject.SetActive(false);
                break;
            case "1":
            case "2":
                CardCount.gameObject.SetActive(true);
                break;
        }
    }
}
