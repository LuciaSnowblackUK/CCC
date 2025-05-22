using TMPro;
using UnityEngine;
using static GM_Global;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonGoCombat : MonoBehaviour
{
    public string LevelName = "TestCombat";
    DeckLoader DeckLoader;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        DeckLoader = GameObject.Find("DeckLoader").GetComponent<DeckLoader>();
    }

    public void OnButtonClick()
    {
        if (DeckLoader.Instance.deckCount == 12)
        {
            DeckLoader.ShuffleDeck();                   // Shuffle first
            DeckLoader.SaveDeckToPlayerPrefs();         // Then save the shuffled version
            StartCoroutine(LoadSceneWithDelay());       // Load scene after a short delay
        }
    }

    private System.Collections.IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForEndOfFrame();           // Let Unity update data
        SceneManager.LoadScene(LevelName);
    }
}
