using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class QuickDraw : Card
{
    //public Variables
    public override string Name { get; set; } = "QuickDraw";
    public override int ID { get; set; } = 0032;
    public override List<string> Tag { get; set; } = new List<string> { "Action" };

    public override string CardDiscription { get; set; } =
@"<QuickDraw>

Draw a <Shooting> tag card that is closest to the top of your deck, repeat the same for a <Ammo> tag card

When Discard:
Shuffle your Discard Pile.
then, you fill up your Draws.

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
        //Draw a <Shooting> tag card that is closest to the top of your deck, repeat the same for a <Ammo> tag card

        await GM_Card.DrawCardWithTag("Shooting", "Deck", true);
        await GM_Card.DrawCardWithTag("Ammo", "Deck", true);




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
        await GM_Card.Shuffle(false, true);
        GM_Global.Draws = GM_Global.MaxDraws;

        return false;
    }


}
