using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Bullet : Card
{
    //public Variables
    public override string Name { get; set; } = "Bullet";
    public override int ID { get; set; } = 0010;
    public override List<string> Tag { get; set; } = new List<string> { "Ammo" };

    // Core functions
    public override async Task<bool> WhenDraw()
    {
        return false;
    }

    public override async Task<bool> WhenPlayed()
    {
        GM_Global.CurrentPlayerState = PlayerState.Idle;

        // Not longer a hand card when played
        GM_Card.ReturnHandCard().Remove(this.gameObject);
        transform.SetParent(null); transform.position = new Vector3(100f, 100f, transform.position.z);

        //---------------------------------------------------------------------------------------------------------------
        GameObject Card1 = null;

        // To Choose a Card1 and Discard it
        Card1 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCardOptional, GM_Global);
        bool C1 = await GM_Card.Discard(Card1, true);

        //---------------------------------------------------------------------------------------------------------------
        // Using Draws to Draw
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


        // 玩家选择敌人（通过静态方法等待选择）// 使用异步方式等待目标选择
        GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);

        if (Target != null)
        {
            Creature TargetCreature = Target.GetComponent<Creature>();

            // 如果选择了有效的敌人
            if (TargetCreature != null)
            {
                int TargetInGameID = TargetCreature.InGameID;

                // 对目标造成20的K伤害并使其Stun10
                GM_Creature.Damage(TargetInGameID, "K", 20); // deal 20 damage to target
                GM_Creature.Stun(TargetInGameID, 10); // stun the target for 10 turns
            }
        }
        else
        {
            Debug.Log("没有选择有效目标。");
        }

        Destroy(this.gameObject);
        GM_Global.CurrentPlayerState = PlayerState.PlayingCard;
        return false;
    }



}
