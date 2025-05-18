using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    public Dictionary<int,int> DeckList = new Dictionary<int, int>();

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


    public void ToggleCardCount(int cardID)
    {
        if (!DeckList.ContainsKey(cardID))
        {
            DeckList[cardID] = 1; // 从0到1
        }
        else if (DeckList[cardID] == 1)
        {
            DeckList[cardID] = 2; // 从1到2
        }
        else
        {
            DeckList.Remove(cardID); // 从2到0
        }
    }



}
