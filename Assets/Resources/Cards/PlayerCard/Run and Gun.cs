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
        bool C1 = await GM_Card.Discard(Card1, true);
        GameObject Card2 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C2 = await GM_Card.Discard(Card2, true);
        GameObject Card3 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C3 = await GM_Card.Discard(Card3, true);

        //Heal according to card discarded
        int Cardcount = (C1 ? 1 : 0) + (C2 ? 1 : 0) + (C3 ? 1 : 0);
        GM_Creature.Heal(0, Cardcount * 10);

        // deal with cards
        if (C1)
        {
            if (Card1.GetComponent<Card>().ID == 0010)
            {
                await GM_Card.Add(0042, "PlayerCard", "Hand", false);
            }

            if (Card1.GetComponent<Card>().ID == 0042)
            {
                await GM_Card.Add(0010, "PlayerCard", "Hand", false);
            }

        }

        if (C2)
        {
            if (Card2.GetComponent<Card>().ID == 0010)
            {
                await GM_Card.Add(0042, "PlayerCard", "Hand", false);
            }

            if (Card2.GetComponent<Card>().ID == 0042)
            {
                await GM_Card.Add(0010, "PlayerCard", "Hand", false);
            }
        }

        if (C3)
        {
            if (Card3.GetComponent<Card>().ID == 0010)
            {
                await GM_Card.Add(0042, "PlayerCard", "Hand", false);
            }

            if (Card3.GetComponent<Card>().ID == 0042)
            {
                await GM_Card.Add(0010, "PlayerCard", "Hand", false);
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
