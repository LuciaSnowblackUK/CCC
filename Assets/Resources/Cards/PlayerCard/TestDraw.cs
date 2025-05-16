using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GM_Global;

public class TestDraw : Card
{
    //public Variables
    public override int ID { get; set; } = 9992;
    public override List<string> Tag { get; set; } = new List<string>();

    public override string CardDiscription { get; set; } =
    @"TextDraw
    Draw a Card
    Tag:";

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



        //the effect of this card
        await GM_Card.Draw(1, true);


        //Using Draws to Draw
        while (GM_Card.ReturnHandCard().Count < GM_Card.HandSize && GM_Global.Draws > 0)
        {
            Debug.Log(GM_Card.ReturnHandCard().Count);

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
        Debug.Log("hi");
        return false;
    }


}
