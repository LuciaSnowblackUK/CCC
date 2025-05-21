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

    // Split and parse to int
    List<int> deck = idString.Split(',').Select(id => int.Parse(id)).ToList();

    // Set the loaded deck in DeckLoader
    DeckLoader.SetDeck(deck); // Make sure DeckLoader has a SetDeck method

    return deck;
}
}
