using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;
using static UnityEngine.GraphicsBuffer;

public class Fire_Axe : Card
{
    //public Variables
    public override string Name { get; set; } = "Fire Axe";
    public override int ID { get; set; } = 0041;
    public override List<string> Tag { get; set; } = new List<string> { "Melee" };

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
        //the effect of this card: Discard a Card from your hand,Deal 20 Explosive damage 20 stun to target enemy,add 2[Combo] card into your hand

        //Targeting and Discard those card
        bool C1 = false;
        GameObject Card1 = null;

        if (GM_Card.ReturnHandCard().Count != 0)
        {
            Card1 = await TargetingHelper.WaitForTargetWithComponentAsync<Card>(PlayerState.ChoosingCard, GM_Global);
            C1 = await GM_Card.Discard(Card1, true);
        }


        // 玩家选择敌人（通过静态方法等待选择）
        GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);

        Creature TargetCreature = Target.GetComponent<Creature>();
        // 如果选择了有效的敌人
        if (TargetCreature != null)
        {
            int TargetInGameID = TargetCreature.InGameID;

            // Deal 20 Explosive damage 20 stun to target
            GM_Creature.Damage(TargetInGameID, "E", 20);
            GM_Creature.Stun(TargetInGameID, 20);
        }


        // Add 2 Combo cards
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


        return false;
    }


}

