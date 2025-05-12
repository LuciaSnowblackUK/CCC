using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickHandler : MonoBehaviour, IPointerDownHandler
{
    public GameObject Card;

    // 将 RectTransform 作为类成员保存
    private RectTransform cardRectTransform;
    public GM_Global GM_Global;


    private void Awake()
    {
        // 获取 Card 对象的 RectTransform 组件
        cardRectTransform = Card.GetComponent<RectTransform>();

        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 判断鼠标点击的是左键还是右键
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // 获取当前的 anchoredPosition
            Vector2 currentPosition = cardRectTransform.anchoredPosition;
            GM_Global.Target = Card;
            // 修改 Y 坐标，增加 2
            cardRectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 2);
            Debug.Log("左键点击，Y坐标增加2");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // 获取当前的 anchoredPosition
            Vector2 currentPosition = cardRectTransform.anchoredPosition;

            // 修改 Y 坐标，减少 2
            cardRectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y - 2);
            Debug.Log("右键点击，Y坐标减少2");
        }
    }
}


