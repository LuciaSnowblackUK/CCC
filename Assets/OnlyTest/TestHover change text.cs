using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestHoverchangetext : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text TestText;  // 你想改变的文本
    private Button button;  // 按钮组件

    void  Start()
    {
        // 获取按钮组件
        button = GetComponent<Button>();

        // 如果 TestText 为空，可以通过 Inspector 拖进去
        if (TestText == null)
            TestText = GameObject.Find("TestText").GetComponent<TMP_Text>();

        // 确保按钮有设置 OnClick 事件，如果有需要
        button.onClick.AddListener(OnButtonClick);
    }

    // 鼠标进入按钮区域时触发
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("鼠标悬停在我上面啦！");
        if (TestText != null)
            TestText.text = "A";
    }

    // 鼠标离开按钮区域时触发
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标离开我了！");
        if (TestText != null)
            TestText.text = "B";
    }

    // 按钮点击时的处理
    public virtual void OnButtonClick()
    {
        Debug.Log("C");
        if (TestText != null)
            TestText.text = "C";
    }
}
