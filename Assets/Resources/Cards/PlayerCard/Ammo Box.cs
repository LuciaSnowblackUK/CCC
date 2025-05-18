using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Ammo_Box : Card
{
    //public Variables
    public override string Name { get; set; } = "Ammo Box";
    public override int ID { get; set; } = 0030;
    public override List<string> Tag { get; set; } = new List<string> { "Action" };
    public override string CardDiscription { get; set; } =
@"<AmmoBox>

Discard up to 5 cards from your hand,
then add 5 [Bullet] to the top of your deck,
then add  5 [Bullet] to the bottom of your Discard Pile

<Tag:Action>";




    // Core functions
    public override async Task<bool> WhenDraw()
    {
        GM_Global.CurrentPlayerState = PlayerState.Idle;


        //Not longer a hand card when played
        GM_Card.ReturnHandCard().Remove(this.gameObject);
        transform.SetParent(null); transform.position = new Vector3(100f, 100f, transform.position.z);


        //---------------------------------------------------------------------------------------------------------------

        //Targeting and Discard those card
        GameObject Card1 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C1 = await GM_Card.Discard(Card1, true);
        GameObject Card2 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C2 = await GM_Card.Discard(Card2, true);
        GameObject Card3 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C3 = await GM_Card.Discard(Card3, true);
        GameObject Card4 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C4 = await GM_Card.Discard(Card4, true);
        GameObject Card5 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C5 = await GM_Card.Discard(Card5, true);




        //add [Bullet]
        await GM_Card.Add(0010, "PlayerCard", "Deck", true);
        await GM_Card.Add(0010, "PlayerCard", "Deck", true);
        await GM_Card.Add(0010, "PlayerCard", "Deck", true);
        await GM_Card.Add(0010, "PlayerCard", "Deck", true);
        await GM_Card.Add(0010, "PlayerCard", "Deck", true); //(int CardID, string Pack, string Where, bool Direction)

        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", false);
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", false);
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", false);
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", false);
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", false);

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

    public override async Task<bool> WhenPlayed()
    {


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
