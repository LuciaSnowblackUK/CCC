using TMPro;
using UnityEngine;
using static GM_Global;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LastPage : MonoBehaviour
{
    public PageMonitor PageMonitor;

    private void Awake()
    {
        // 获取按钮组件并添加点击事件监听
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        if (PageMonitor.lastPage())
        {
            PageMonitor.GoToPage(PageMonitor.CurrentPage);
        }

    }
}
