using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    public static DeckLoader Instance { get; private set; }

    public Dictionary<int, int> DeckList = new Dictionary<int, int>();
    public List<int> ShuffledDeckList = new List<int>(); // Preserves order
    public int deckCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static string GetNameByID(int id)
    {
        return id switch
        {
            0010 => "Bullet",
            0011 => "Grenade",
            0012 => "Smoke Trap",
            0013 => "Mine",
            0014 => "RPG",
            0020 => "Kill Shot",
            0021 => "Rapid Fire",
            0022 => "Counter Shot",
            0023 => "Explosive Shot",
            0030 => "Ammo Box",
            0031 => "Reload and Ready",
            0032 => "QuickDraw",
            0033 => "Run and Gun",
            0040 => "Knife",
            0041 => "Fire Axe",
            0042 => "Combo",
            0043 => "Wall hammer",
            0044 => "Finish Blow",
            _ => "Unknown"
        };
    }

    public string GetDeckListAsText()
    {
        string result = "";
        var sortedList = DeckList.OrderBy(kvp => GetNameByID(kvp.Key));

        foreach (var kvp in sortedList)
        {
            string cardName = GetNameByID(kvp.Key);
            result += $"{cardName}: {kvp.Value}\n";
        }

        return result;
    }

    public string ToggleCardCount(int cardID)
    {
        deckCount = DeckList.Values.Sum();

        if (!DeckList.ContainsKey(cardID))
        {
            if (deckCount < 12)
            {
                DeckList[cardID] = 1;
                deckCount = DeckList.Values.Sum();
                return "1";
            }
            return "0";
        }
        else if (DeckList[cardID] == 1)
        {
            if (deckCount < 12)
            {
                DeckList[cardID] = 2;
                deckCount = DeckList.Values.Sum();
                return "2";
            }
            else
            {
                DeckList.Remove(cardID);
                deckCount = DeckList.Values.Sum();
                return "0";
            }
        }
        else
        {
            DeckList.Remove(cardID);
            deckCount = DeckList.Values.Sum();
            return "0";
        }
    }

    public void SaveDeckToPlayerPrefs()
    {
        // Save the shuffled deck order
        string idString = string.Join(",", ShuffledDeckList);
        PlayerPrefs.SetString("DeckCardIDs", idString);
        PlayerPrefs.Save();
    }

    public void SetDeck(List<int> cardIDs)
    {
        DeckList.Clear();
        foreach (int id in cardIDs)
        {
            if (DeckList.ContainsKey(id))
                DeckList[id]++;
            else
                DeckList[id] = 1;
        }
        deckCount = DeckList.Values.Sum();

        ShuffledDeckList = new List<int>(cardIDs); // Also preserve order
    }

    public string GetCardCountAsString(int cardID)
    {
        int count = DeckList.ContainsKey(cardID) ? DeckList[cardID] : 0;
        return count.ToString();
    }

    public void ShuffleDeck()
    {
        // Flatten the DeckList into a list
        List<int> flatDeck = new List<int>();
        foreach (var kvp in DeckList)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                flatDeck.Add(kvp.Key);
            }
        }

        // Shuffle it
        System.Random rng = new System.Random();
        int n = flatDeck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (flatDeck[k], flatDeck[n]) = (flatDeck[n], flatDeck[k]);
        }

        // Save shuffled order
        ShuffledDeckList = new List<int>(flatDeck);

        // Rebuild DeckList for consistency (count only)
        DeckList.Clear();
        foreach (int id in flatDeck)
        {
            if (DeckList.ContainsKey(id))
                DeckList[id]++;
            else
                DeckList[id] = 1;
        }

        deckCount = DeckList.Values.Sum();
    }
}
