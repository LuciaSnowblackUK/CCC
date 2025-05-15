using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Grenade : Card
{
    //public Variables
    public override string Name { get; set; } = "Grenade";
    public override int ID { get; set; } = 0011;
    public override List<string> Tag { get; set; } = new List<string> { "Ammo" };

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
        bool C1 = await GM_Card.Discard(Card1, true); transform.position = new Vector3(100f, 100f, transform.position.z);



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
        foreach (int InGameID in GM_Creature.UpdateCreature().Keys)
        {
            if (GM_Creature.UpdateCreature()[InGameID].InGameID != 0) //InGameID = 0 means player, this is a AOE effect for enemies
            {
                GM_Creature.Damage(InGameID, "E", 100);
                GM_Creature.Stun(InGameID, 20);
            }

        }

        return false;
    }

    public override async Task<bool> WhenDiscard()
    {



        return false;
    }


}
