using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HelloWorldButton : MonoBehaviour
{
    public GameObject CARD;

    private void Awake()
    {
        // 获取按钮组件并添加点击事件监听
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // 输出 "你好，世界！" 和父物体的名字
        Debug.Log("你好，世界！" + " 父物体名称: " + CARD.name);
    }
}

