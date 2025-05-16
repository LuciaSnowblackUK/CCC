using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static GM_Global;

public class RPG : Card
{
    //public Variables
    public override string Name { get; set; } = "RPG";
    public override int ID { get; set; } = 0014;
    public override List<string> Tag { get; set; } = new List<string> { "Ammo" };
    public override string CardDiscription { get; set; } =
@"<RPG>

Can not be Played

When Shuffle: 
Deal 100 Explosive damage to target and stun it 100.
Deal 100 Kinetic damage to yourself.

<Tag:Ammo>";

    // Core functions
    public override async Task<bool> WhenDraw()
    {
        return false;
    }

    public override async Task<bool> WhenPlayed()
    {
        //can not be played

        return false;
    }

    public override async Task<bool> WhenShuffle()
    {
        // 玩家选择敌人（通过静态方法等待选择）
        GameObject Target = await TargetingHelper.WaitForTargetWithComponentAsync<Creature>(PlayerState.ChoosingEnemy, GM_Global);

        Creature TargetCreature = Target.GetComponent<Creature>();
        // 如果选择了有效的敌人
        if (TargetCreature != null)
        {
            int TargetInGameID = TargetCreature.InGameID;

            // damage
            GM_Creature.Damage(TargetInGameID, "E", 100);
            GM_Creature.Stun(TargetInGameID, 100);
        }

        GM_Creature.Damage(0, "K", 100);
        return false;
    }

    public override async Task<bool> WhenDiscard()
    {


        return false;
    }


}
