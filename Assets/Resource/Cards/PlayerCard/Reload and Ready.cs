using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Reload_and_Ready : Card
{
    //public Variables
    public override string Name { get; set; } = "Reload and Ready";
    public override int ID { get; set; } = 0031;
    public override List<string> Tag { get; set; } = new List<string> { "Action" };

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
        //Player gain 2 Draws
        GM_Global.Draws += 2;
        //Shuffle your discard pile into your deck
        await GM_Card.Shuffle(false,true);
        //Draw 2 cards
        await GM_Card.Draw(2, true);

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
        //Heal yourself for 50 and change your Armor type to Armored this turn
        GM_Creature.Heal(0, 50);
        GM_Creature.Type(0, "A");

        return false;
    }


}
