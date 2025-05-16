using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static GM_Global;
using System.Threading.Tasks;

public class Wall_hammer : Card
{
    //public Variables
    public override string Name { get; set; } = "Wall hammer";
    public override int ID { get; set; } = 0043;
    public override List<string> Tag { get; set; } = new List<string> { "Melee" };
    public override string CardDiscription { get; set; } =
@"<Wall hammer>

Discard 2 Card from your hand, for each [Combo] discard this way you draw a card
 
Deal 50 ion damage 100 stun to target enemy,

add 2 [Combo] card into your hand

When discarded:
add 2 [Combo] card into your hand

<Tag:Melee>";

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

        //To Choose Cards and Discard them--------------------------------------------------------------------------------------------------------------------------]
        GameObject Card1 = null;
        bool C1 = false;
        GameObject Card2 = null;
        bool C2 = false;

        if (GM_Card.ReturnHandCard().Count != 0)
        {
            Card1 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCard, GM_Global);
            C1 = await GM_Card.Discard(Card1, true);
            Card2 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCard, GM_Global);
            C2 = await GM_Card.Discard(Card2, true);
        }





        // deal with cards
        if (C1)
        {
            if (Card1.GetComponent<Card>().ID == 0042)
            {
                await GM_Card.Draw(1, true);
            }

        }

        if (C2)
        {
            if (Card2.GetComponent<Card>().ID == 0042)
            {
                await GM_Card.Draw(1, true);
            }

        }


        // 玩家选择敌人（通过静态方法等待选择）
        GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);

        Creature TargetCreature = Target.GetComponent<Creature>();
        // 如果选择了有效的敌人
        if (TargetCreature != null)
        {
            int TargetInGameID = TargetCreature.InGameID;

            // 对目标造成50的伤害并使其Stun100
            GM_Creature.Damage(TargetInGameID, "I", 50);
            GM_Creature.Stun(TargetInGameID, 100);
        }

        //add 2[Combo] card into your hand
        await GM_Card.Add(0042, "PlayerCard", "Hand", false);
        await GM_Card.Add(0042, "PlayerCard", "Hand", false);

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
        //add 2[Combo] card into your hand
        await GM_Card.Add(0042, "PlayerCard", "Hand", false);

        return false;
    }


}

