using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GM_Global;

public class TestDiscard : Card
{
    //public Variables
    public override int ID { get; set; } = 9992;
    public override List<string> Tag { get; set; } = new List<string>();

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


        //the effect of this card: randomly discard a card
        int index = UnityEngine.Random.Range(0, GM_Card.Hand.Cards.Count);
        GameObject TargetCard = GM_Card.Hand.Cards[index];
        await GM_Card.Discard(TargetCard, true);

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
