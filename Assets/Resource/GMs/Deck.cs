using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Deck : MonoBehaviour //This is an Parent objects to put Deck
{
    //Pubulic Variables
    public int Numbers; //How many cards in the Deck
    public List<GameObject> Cards = new List<GameObject>(); // The list of cards in Deck


    // Method: Update the Cards in Deck
    public List<GameObject> UpdateCards()
    {
        Cards.Clear();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Cards.Add(this.transform.GetChild(i).gameObject);
        }

        return Cards;
    }


    //Method:ResetOrder
    public void ResetOrder()
    {
        // Step 1: 按照 Cards 列表的顺序更新 Hierarchy 顺序
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.SetSiblingIndex(i);
        }

        // Step 2: 根据 Hierarchy 顺序更新 Cards 列表
        Cards.Clear();
        foreach (Transform child in transform)
        {
            Cards.Add(child.gameObject);
        }
    }





}
