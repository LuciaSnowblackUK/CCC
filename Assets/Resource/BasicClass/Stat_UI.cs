using System;
using TMPro;
using UnityEngine;

public class Stat_UI : MonoBehaviour
{


    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;

    public Creature Creature;
    public int CurrentStat;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        Creature = GetComponentInParent<Creature>();


        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");

    }


    private void Update()
    {
        ShowStat();
    }



    public void ShowStat()
    {
        CurrentStat = Creature.CurrentStat;
        switch (CurrentStat)
        {
            case 0: // Idle
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(child.name == "Idle");
                }
                break;

            case 1: // Attack
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(child.name == "Attack");
                }
                break;

            case 2: // Defend
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(child.name == "Defend");
                }
                break;

            case 3: // Cast
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(child.name == "Cast");
                }
                break;
        }
    }




}
