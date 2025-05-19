using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;
using static UnityEngine.GraphicsBuffer;

public class Finish_Blow : Card
{
    //public Variables
    public override string Name { get; set; } = "Finish Blow";
    public override int ID { get; set; } = 0044;
    public override List<string> Tag { get; set; } = new List<string> { "Melee" };

    public override string CardDiscription { get; set; } =
@"<Finish Blow>

Deal 100 ion damage and 100 stun to target enemy

Discard all your hand

if this card kill an enemy, you get 1 Max Draws if your Max Draw is lower than 6, then, for each [Combo] discarded by this Finish Blow, you heal 5 HP


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

        //Deal 100 ion damage and 1000 stun to target enemy


        // 玩家选择敌人（通过静态方法等待选择）
        bool Kill = false;
        GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);
        if (Target != null)
        {
            Creature TargetCreature = Target.GetComponent<Creature>();
            // 如果选择了有效的敌人
            if (TargetCreature != null)
            {
                int TargetInGameID = TargetCreature.InGameID;

                // 对目标造成100的i伤害并使其Stun100
                Kill = GM_Creature.Damage(TargetInGameID, "I", 100);
                GM_Creature.Stun(TargetInGameID, 100);
            }
        }




        // 先复制一份手牌列表，避免在遍历中修改原列表
        List<GameObject> currentHand = new List<GameObject>(GM_Card.ReturnHandCard());

        int ComboDiscarded = 0;

        foreach (GameObject Card in currentHand)
        {
            if (Card.GetComponent<Card>().ID == 0042)
            {
                ComboDiscarded++;
            }
            await GM_Card.Discard(Card, true);
 
        }

        //if this card kill an enemy, you get 1 Max Draws if your Max Draw is lower than 6, and Draw 3 Cards, then, for each [Combo] discarded by this Finish Blow, you heal 20HP


        if (Kill)
        {
            if (GM_Global.MaxDraws < 6)
            {
                GM_Global.MaxDraws++;
            }

            GM_Creature.Heal(0, 5 * ComboDiscarded);

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