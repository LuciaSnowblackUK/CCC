using System.Collections.Generic;
using System.Linq; // ⬅️ 别忘了添加这个命名空间
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    public static DeckLoader Instance { get; private set; }

    public Dictionary<int,int> DeckList = new Dictionary<int, int>();

    public int deckCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ✅ 切场景时不销毁
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
            // 卡牌当前不在字典里，尝试增加1张
            if (deckCount < 12)
            {
                DeckList[cardID] = 1; // 从0到1
                deckCount = DeckList.Values.Sum();
                return "1";
            }
            else
            {
                // 达到上限，不能增加
                deckCount = DeckList.Values.Sum();
                return "0"; // 不变，或你可以返回特殊标记表示无法增加
            }
        }
        else if (DeckList[cardID] == 1)
        {
            // 当前是1张，尝试加到2张
            if (deckCount < 12)
            {

                DeckList[cardID] = 2; // 从1到2
                deckCount = DeckList.Values.Sum();
                return "2";
            }
            else
            {
                DeckList.Remove(cardID); // ✅ 从字典中彻底移除
                deckCount = DeckList.Values.Sum();
                // 达到上限，减少1张
                return "0"; // 
            }
        }
        else
        {

                // 当前是2张，减少为0（移除）
                DeckList.Remove(cardID); // 从2到0
                deckCount = DeckList.Values.Sum();
                return "0";
        }
    }

    public void SaveDeckToPlayerPrefs()
    {
        // Create a list to hold all card IDs, including duplicates for multiples
        List<int> cardIDs = new List<int>();
        foreach (var kvp in DeckList)
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
    }

    public string GetCardCountAsString(int cardID)
    {
        int count = DeckList.ContainsKey(cardID) ? DeckList[cardID] : 0;
        return count.ToString();
    }


}
