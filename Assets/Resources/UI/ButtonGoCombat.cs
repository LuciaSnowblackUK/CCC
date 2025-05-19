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

    private void Awake()
    {
        // 获取按钮组件并添加点击事件监听
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick()
    {
        if (DeckLoader.Instance.deckCount == 12)
        {
            SceneManager.LoadScene(LevelName); // need 12 cards in deck
        }

     }
}
