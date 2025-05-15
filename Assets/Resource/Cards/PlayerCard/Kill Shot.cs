using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class Kill_Shot : Card
{
    //public Variables
    public override string Name { get; set; } = "Kill Shot";
    public override int ID { get; set; } = 0020;
    public override List<string> Tag { get; set; } = new List<string> { "Shooting" };

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



        //if

        if (C1 || C2)
        {
            if (Card1.GetComponent<Card>().Tag.Contains("Ammo") || Card2.GetComponent<Card>().Tag.Contains("Ammo"))
            {
                // 玩家选择敌人（通过静态方法等待选择）
                GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);

                Creature TargetCreature = Target.GetComponent<Creature>();
                // 如果选择了有效的敌人
                if (TargetCreature != null)
                {
                    int TargetInGameID = TargetCreature.InGameID;

                    // 100 K damage 
                    GM_Creature.Damage(TargetInGameID, "K", 100);
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
