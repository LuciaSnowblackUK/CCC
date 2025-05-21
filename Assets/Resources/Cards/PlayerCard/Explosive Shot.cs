using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Explosive_Shot: Card
{
    //public Variables
    public override string Name { get; set; } = "Explosive Shot";
    public override int ID { get; set; } = 0023;
    public override List<string> Tag { get; set; } = new List<string> { "Shooting" };

    public override string CardDiscription { get; set; } =
@"<Explosive Shot>

Discard up to 3 cards from your hand,Deal 20 explosive damage and 10 stun to target for each <Ammo> tag discarded this way

<Tag:Shooting>";

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
        ;

        //Targeting and Discard those card
        GameObject Card1 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool K1 = false;
        bool C1 = false;
        if (Card1 != null)
        {
            K1 = Card1.GetComponent<Card>().Tag.Contains("Ammo");
            C1 = await GM_Card.Discard(Card1, true);
        }
        GameObject Card2 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool K2 = false;
        bool C2 = false;
        if (Card1 != null)
        {
            K2 = Card2.GetComponent<Card>().Tag.Contains("Ammo");
            C2 = await GM_Card.Discard(Card2, true);
        }
        GameObject Card3 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool K3 = false;
        bool C3 = false;
        if (Card3 != null)
        {
            K3 = Card3.GetComponent<Card>().Tag.Contains("Ammo");
            C3 = await GM_Card.Discard(Card3, true);
        }

        //
        if (C1)
        {
            if (K1)
            {
                // 玩家选择敌人（通过静态方法等待选择）
                GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);
                if (Target != null)
                {
                    Creature TargetCreature = Target.GetComponent<Creature>();
                    // 如果选择了有效的敌人
                    if (TargetCreature != null)
                    {
                        int TargetInGameID = TargetCreature.InGameID;

                        // 20 E damage + 10 stun
                        GM_Creature.Damage(TargetInGameID, "E", 20);
                        GM_Creature.Stun(TargetInGameID, 10);
                    }
                }

            }
        }

        if (C2)
        {
            if (K2)
            {
                // 玩家选择敌人（通过静态方法等待选择）
                GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);
                if (Target != null)
                {
                    Creature TargetCreature = Target.GetComponent<Creature>();
                    // 如果选择了有效的敌人
                    if (TargetCreature != null)
                    {
                        int TargetInGameID = TargetCreature.InGameID;

                        // 20 E damage + 10 stun
                        GM_Creature.Damage(TargetInGameID, "E", 20);
                        GM_Creature.Stun(TargetInGameID, 10);
                    }
                }

            }
        }

        if (C3)
        {
            if (K3)
            {
                // 玩家选择敌人（通过静态方法等待选择）
                GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);
                if (Target != null)
                {
                    Creature TargetCreature = Target.GetComponent<Creature>();
                    // 如果选择了有效的敌人
                    if (TargetCreature != null)
                    {
                        int TargetInGameID = TargetCreature.InGameID;

                        // 20 E damage + 10 stun
                        GM_Creature.Damage(TargetInGameID, "E", 20);
                        GM_Creature.Stun(TargetInGameID, 10);
                    }
                }

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
