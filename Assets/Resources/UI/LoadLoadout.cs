using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

public class LoadLoadout : MonoBehaviour
{
    public int LoadoutNumber;
    DeckLoader DeckLoader;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        DeckLoader = GameObject.Find("DeckLoader").GetComponent<DeckLoader>();
    }

    public void OnButtonClick()
    {
        LoadDeckFromPlayerPrefs(LoadoutNumber);
    }

    public List<int> LoadDeckFromPlayerPrefs(int num)
    {
        string idString = PlayerPrefs.GetString($"Loadout{num}", "");
        if (string.IsNullOrEmpty(idString))
            return new List<int>();

        List<int> deck = idString.Split(',').Select(id => int.Parse(id)).ToList();

        DeckLoader.SetDeck(deck);

        // Refresh all DeckCardAdd instances
        DeckCardAdd[] allCards = FindObjectsOfType<DeckCardAdd>();
        foreach (var card in allCards)
        {
            card.RefreshCardCount();
        }

        return deck;
    }
}
