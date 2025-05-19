using TMPro;
using UnityEngine;
using static GM_Global;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class ButtonGoCombat : MonoBehaviour
{
    public string LevelName = "TestCombat";
    DeckLoader DeckLoader;

    private void Awake()
    {
        // 获取按钮组件并添加点击事件监听
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        DeckLoader = GameObject.Find("DeckLoader").GetComponent<DeckLoader>();
    }
    public void OnButtonClick()
    {
        if (DeckLoader.Instance.deckCount == 12)
        {
            DeckLoader.SaveDeckToPlayerPrefs();
            SceneManager.LoadScene(LevelName);
        }
    }
}
