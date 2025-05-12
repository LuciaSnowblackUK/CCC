using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Rapid_Fire : Card
{
    //public Variables
    public override string Name { get; set; } = "Rapid Fire";
    public override int ID { get; set; } = 0021;
    public override List<string> Tag { get; set; } = new List<string> { "Shooting" };

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
        transform.SetParent(null);



        //---------------------------------------------------------------------------------------------------------------
        //Targeting and Discard those card
        GameObject Card1 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C1 = await GM_Card.Discard(Card1, true);
        GameObject Card2 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C2 = await GM_Card.Discard(Card2, true);
        GameObject Card3 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C3 = await GM_Card.Discard(Card3, true);

        //Add 4 [Bullet] to Discard Pile
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", true);  //(int CardID, string Pack, string Where, bool Direction)
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", true);  //(int CardID, string Pack, string Where, bool Direction)
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", true);  //(int CardID, string Pack, string Where, bool Direction)
        await GM_Card.Add(0010, "PlayerCard", "DiscardPile", true);  //(int CardID, string Pack, string Where, bool Direction)

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
