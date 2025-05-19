using TMPro;
using UnityEngine;

public class CurrentDeckCount : MonoBehaviour
{

    public TMP_Text CurrentDeckCountTEXT;  // 你想改变的文本
    public DeckLoader DeckLoader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DeckLoader == null)
            DeckLoader = GameObject.Find("DeckLoader").GetComponent<DeckLoader>();
        if (CurrentDeckCountTEXT == null)
            CurrentDeckCountTEXT = GameObject.Find("CurrentDeckCount").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDeckCountTEXT.text = $"{DeckLoader.deckCount}/12";
    }
}
