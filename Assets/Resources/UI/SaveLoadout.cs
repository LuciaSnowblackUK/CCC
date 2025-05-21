using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

public class SaveLoadout : MonoBehaviour
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
        SaveDeckToPlayerPrefs(LoadoutNumber);
    }

    public void SaveDeckToPlayerPrefs(int num)
    {
        // Create a list to hold all card IDs, including duplicates for multiples
        List<int> cardIDs = new List<int>();
        foreach (var kvp in DeckLoader.DeckList)
        {
            // Add the card ID as many times as its count
            for (int i = 0; i < kvp.Value; i++)
            {
                cardIDs.Add(kvp.Key);
            }
        }
        // Convert to comma-separated string
        string idString = string.Join(",", cardIDs);
        // Save to PlayerPrefs
        PlayerPrefs.SetString($"Loadout{num}", idString);
        PlayerPrefs.Save();
    }
}
