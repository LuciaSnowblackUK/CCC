using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static GM_Global;

public class Run_and_Gun : Card
{
    //public Variables
    public override string Name { get; set; } = "Run and Gun";
    public override int ID { get; set; } = 0033;
    public override List<string> Tag { get; set; } = new List<string> { "Action" };
    public override string CardDiscription { get; set; } =
@"<Run and Gun>

Discard up to 3 cards, 
for each card discard this way, heal yourself 10 HP,
for each [Combo] discard this way, add a [Bullet] to your hand
for each [Bullet] discard this way, add a [Combo] to your hand

<Tag:Action>";

    // Core functions
    public override async Task<bool> WhenDraw()
    {
        return false;
    }

    public override async Task<bool> WhenPlayed()
    {
        GM_Global.CurrentPlayerState = PlayerState.Idle;
        //Not longer a hand card when played
        GM_Card.ReturnHandCard().Remove(this.gameObject);
        transform.SetParent(null); transform.position = new Vector3(100f, 100f, transform.position.z);
        //---------------------------------------------------------------------------------------------------------------
        //Targeting and Discard those card


        GameObject Card1 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool K11 = false;
        bool K12 = false;
        if (Card1 != null)
        {
            var cardComp = Card1.GetComponent<Card>();
            if (cardComp != null)
            {
                K11 = cardComp.ID == 10;
                K12 = cardComp.ID == 42;
            }
        }
        bool C1 = Card1 != null ? await GM_Card.Discard(Card1, true) : false;

        GameObject Card2 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool K21 = false;
        bool K22 = false;
        if (Card2 != null)
        {
            var cardComp = Card2.GetComponent<Card>();
            if (cardComp != null)
            {
                K21 = cardComp.ID == 10;
                K22 = cardComp.ID == 42;
            }
        }
        bool C2 = Card2 != null ? await GM_Card.Discard(Card2, true) : false;

        GameObject Card3 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool K31 = false;
        bool K32 = false;
        if (Card3 != null)
        {
            var cardComp = Card3.GetComponent<Card>();
            if (cardComp != null)
            {
                K31 = cardComp.ID == 10;
                K32 = cardComp.ID == 42;
            }
        }
        bool C3 = Card3 != null ? await GM_Card.Discard(Card3, true) : false;

        //Heal according to card discarded
        int Cardcount = (C1 ? 1 : 0) + (C2 ? 1 : 0) + (C3 ? 1 : 0);
        GM_Creature.Heal(0, Cardcount * 10);

        // deal with cards
        if (C1)
        {
            if (K11)
            {
                await GM_Card.Add(0042, "PlayerCard", "Hand", false);
            }

            if (K12)
            {
                await GM_Card.Add(0010, "PlayerCard", "Hand", false);
            }

        }

        if (C2)
        {
            if (K21)
            {
                await GM_Card.Add(0042, "PlayerCard", "Hand", false);
            }

            if (K22)
            {
                await GM_Card.Add(0010, "PlayerCard", "Hand", false);
            }
        }

        if (C3)
        {
            if (K31)
            {
                await GM_Card.Add(0042, "PlayerCard", "Hand", false);
            }

            if (K32)
            {
                await GM_Card.Add(0010, "PlayerCard", "Hand", false);
            }
        }



        //---------------------------------------------------------------------------------------------------------------
        //Using Draws to Draw
        while (GM_Card.ReturnHandCard().Count < GM_Card.HandSize && GM_Global.Draws > 0)
        {
            GM_Global.Draws--;
            await GM_Card.Draw(1, true);
        }

        // into DiscardPile after play
        await GM_Card.Discard(this.gameObject, false);
        GM_Global.CurrentPlayerState = PlayerState.PlayingCard;


        return false;
    }

    public override async Task<bool> WhenShuffle()
    {


        return false;
    }

    public override async Task<bool> WhenDiscard()
    {

        return false;
    }



}
