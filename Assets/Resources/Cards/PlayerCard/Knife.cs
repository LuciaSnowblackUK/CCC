using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Knife : Card
{
    //public Variables
    public override string Name { get; set; } = "Knife";
    public override int ID { get; set; } = 0040;
    public override List<string> Tag { get; set; } = new List<string> { "Melee" };
    public override string CardDiscription { get; set; } =
@"<Knife>

Deal 10 ion damage 10 stun to target enemy,
add a [Combo] card into your hand
Draw a Card

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
        //the effect of this card: Deal 10 ion damage 10 stun to target enemy,add a[Combo] card into your handDraw a Card


        // 玩家选择敌人（通过静态方法等待选择）
        GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);

        Creature TargetCreature = Target.GetComponent<Creature>();
        // 如果选择了有效的敌人
        if (TargetCreature != null)
        {
            int TargetInGameID = TargetCreature.InGameID;

            // Deal 10 ion damage 10 stun to target enemy
            GM_Creature.Damage(TargetInGameID, "I", 10);
            GM_Creature.Stun(TargetInGameID, 10);
        }

        //add 1 [Combo] card into your hand
        await GM_Card.Add(0042, "PlayerCard", "Hand", false);

        // Draw 1
        await GM_Card.Draw(1, true);




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
