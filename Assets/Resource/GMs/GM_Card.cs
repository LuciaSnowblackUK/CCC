using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting; // For List and Random
using System.Threading.Tasks;

public class GM_Card : MonoBehaviour
{
    //Pubulic Variables for cards
    public int HandSize = 6;
    public int MaxHand = 10; //can't put more cards when >10
    public Deck Deck;
    public DiscardPile DiscardPile;
    public Hand Hand;


    //Important Refers
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;

    void Start()
    {
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();
        Deck = GameObject.Find(nameof(Deck)).GetComponent<Deck>();
        DiscardPile = GameObject.Find(nameof(DiscardPile)).GetComponent<DiscardPile>();
        Hand = GameObject.Find(nameof(Hand)).GetComponent<Hand>();

        // 可选：报错检查
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");
    }





    //The method to manipulate cards

    //Draw: Move n cards from deck to hand
    public async Task<bool> Draw(int number, bool IfTrigger)
    {

        if (ReturnHandCard().Count >= 10)
        {
            return false;
        }

        for (int i = 0; i < number; i++)
        {
            Deck.UpdateCards();
            DiscardPile.UpdateCards();
            Hand.UpdateCards();

            if (Deck.Cards.Count <= 0)
            {
                await Shuffle(false, true);

            }

            if (Deck.Cards.Count <= 0)
            {
                Debug.Log("Deck is still empty after shuffling.");
                return false;

            }

            GameObject TargetCard = Deck.Cards[0];

            // 移动卡片到手牌
            TargetCard.transform.SetParent(Hand.transform, false);
            Hand.Cards.Add(TargetCard);
            Deck.Cards.RemoveAt(0); // 确保卡片被移除

            // 如果触发条件为真，则执行抽卡时的逻辑
            if (IfTrigger)
            {
                Card card = TargetCard.GetComponent<Card>();
                await card.WhenDraw();
            }

            Debug.Log($"Card drawn: {TargetCard.name}, Deck count after draw: {Deck.Cards.Count}");

            // 重置排序
            Deck.ResetOrder();
            Hand.ResetOrder();
        }

        return true;
    }


    //DrawCardWithTag, the one closest to top
    public async Task<bool> DrawCardWithTag(string tag, string where, bool ifTrigger)
    {
        List<GameObject> sourceList;

        if (ReturnHandCard().Count >= 10)
        {
            return false;
        }

        // 选择来源列表
        if (where == "Deck")
        {
            sourceList = Deck.Cards;
        }
        else if (where == "DiscardPile")
        {
            sourceList = DiscardPile.Cards;
        }
        else
        {
            Debug.LogWarning("未知的卡堆来源: " + where);
            return false;
        }

        foreach (GameObject card in sourceList.ToList()) // 防止遍历中修改
        {
            if (card.GetComponent<Card>().Tag.Contains(tag))
            {
                sourceList.Remove(card);

                // 插入目标堆顶部，如果是从 Discard 来的，需要插到 Deck 顶部
                Deck.Cards.Insert(0, card);

                await Draw(1, ifTrigger); // 从 Deck 抽
                return true;
            }
        }

        return false;
    }

    //Discard: Move a card from Hand to DiscardPile
    public async Task<bool> Discard(GameObject TargetCard, bool IfTrigger)
    {

        //-----------------------
        if (TargetCard == null) // If TargetCard is null, return false early
        {
            return false;
        }

        // Check if TargetCard has the 'Card' component
        Card cardComponent = TargetCard.GetComponent<Card>();
        if (cardComponent == null) // If no Card component, return false
        {
            Debug.Log("No Card component found on the target card!");
            return false;
        }

        if (Hand.Cards.Count == 0) // Check if there are cards in hand to discard
        {
            return false;
        }

        // If a valid TargetCard is provided, proceed with discard
        if (cardComponent != null)
        {
            // Remove the card from Hand
            Hand.Cards.Remove(TargetCard);

            if (IfTrigger)
            {
                Debug.Log("AAAAAAA");

                // 这里如果有涉及到异步的操作，可以将其改为异步
                await cardComponent.WhenDiscard();  // 异步执行 WhenDiscard
            }

            // Move the card to the discard pile
            TargetCard.transform.SetParent(DiscardPile.transform, false); // Move to DiscardPile
            DiscardPile.Cards.Insert(0, TargetCard); // Add the card to DiscardPile's list

            Hand.ResetOrder();
            DiscardPile.ResetOrder();
            Debug.Log("BBBBBB");
            return true;
        }

        return false; // If no valid card to discard, return false
    }


    //Shuffle: Shuffle Cards from the DiscardPile to the Deck
    public async Task<bool> Shuffle(bool IfRandom, bool IfTrigger)
    {
        if (DiscardPile.Cards.Count <= 0)
            return false;

        // 如果要洗牌，就先打乱 DiscardPile.Cards
        if (IfRandom)
        {
            System.Random rng = new System.Random();
            int n = DiscardPile.Cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                GameObject value = DiscardPile.Cards[k];
                DiscardPile.Cards[k] = DiscardPile.Cards[n];
                DiscardPile.Cards[n] = value;
            }

        }

        // 把卡牌加到 Deck 并设置父物体
        Deck.Cards.AddRange(DiscardPile.Cards);

        foreach (GameObject TargetCard in DiscardPile.Cards)
        {
            TargetCard.transform.SetParent(Deck.transform, false);
            if (IfTrigger)
            {
                    Card Card = TargetCard.GetComponent<Card>();
                    await Card.WhenShuffle();

            }
        }


        Deck.ResetOrder();
        Deck.UpdateCards();
        DiscardPile.Cards.Clear();
        return true;
    }

    //Add: Generate a card and put it Where [Deck,Hand,DiscardPile], and direction [Top:true,Bottom:false]
    public async Task<bool> Add(int CardID, string Pack, string Where, bool Direction)
    {
        if (ReturnHandCard().Count >= 10)
        {
            return false;
        }


        // 拼接路径时要注意加上 "/"
        GameObject[] spawnList = Resources.LoadAll<GameObject>("Cards/" + Pack);

        foreach (GameObject prefab in spawnList)
        {
            Card Them = prefab.GetComponent<Card>();
            if (Them != null && Them.ID == CardID)
            {
                switch (Where)
                {
                    case "Deck":
                        if (Direction)
                        {
                            GameObject instance = Instantiate(prefab, Deck.transform);
                            Deck.Cards.Insert(0, instance);
                            Deck.ResetOrder();
                        }
                        else
                        {
                            GameObject instance = Instantiate(prefab, Deck.transform);
                            Deck.Cards.Add(instance);
                            Deck.ResetOrder();
                        }
                            ;
                        break;


                    case "Hand":

                        if (Hand.Cards.Count >= MaxHand) // Can not have more than 10 cards in hand
                        {
                            return false;
                        }


                        if (Direction)
                        {
                            GameObject instance = Instantiate(prefab, Hand.transform);
                            Hand.Cards.Insert(0, instance);
                            Hand.ResetOrder();
                        }
                        else
                        {
                            GameObject instance = Instantiate(prefab, Hand.transform);
                            Hand.Cards.Add(instance);
                            Hand.ResetOrder();
                        }
                            ;
                        break;


                    case "DiscardPile":
                        if (Direction)
                        {
                            GameObject instance = Instantiate(prefab, DiscardPile.transform);
                            DiscardPile.Cards.Insert(0, instance);
                            DiscardPile.ResetOrder();
                        }
                        else
                        {
                            GameObject instance = Instantiate(prefab, DiscardPile.transform);
                            DiscardPile.Cards.Add(instance);
                            DiscardPile.ResetOrder();
                        }
                            ;
                        break;
                }

                return true;
            }
        }

        Debug.LogWarning("Card with ID " + CardID + " not found in Pack: " + Pack);

        return false;

    }

    //Venting: if the Cards in Hand is larger than the HandSize, then discard all cards
    public async Task<bool> Venting()
    {
        // 如果手牌数大于指定大小
        if (Hand.Cards.Count > HandSize)
        {
            Hand.UpdateCards();

            // 临时存储要discard的卡片
            List<GameObject> toDiscard = new List<GameObject>();

            foreach (GameObject Targetcard in Hand.Cards)
            {
                toDiscard.Add(Targetcard);  // 将卡片添加到临时列表
            }

            // 异步执行弃牌，使用 await 等待每次弃牌操作完成
            foreach (GameObject Targetcard in toDiscard)
            {
                await Discard(Targetcard, true);  // 注意这里调用的是 DiscardAsync
            }

            // 清空原列表并更新
            Hand.Cards.Clear();
            Hand.UpdateCards();  // 更新手牌列表

            return true;
        }

        return false;
    }


//The Method to return Player Cards information
public List<GameObject> ReturnDeckCard()
    {
        Deck.UpdateCards();
        return Deck.Cards;
    }

    public List<GameObject> ReturnHandCard() 
    {
        Hand.UpdateCards();
        return Hand.Cards; 
    }

    public List<GameObject> ReturnDiscardPileCard()
    {
        DiscardPile.UpdateCards();
        return DiscardPile.Cards;
    }
}
