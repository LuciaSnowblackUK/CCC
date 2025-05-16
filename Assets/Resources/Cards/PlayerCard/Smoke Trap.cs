using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Smoke_Trap: Card
{
    //public Variables
    public override string Name { get; set; } = "Smoke_Trap";
    public override int ID { get; set; } = 0012;
    public override List<string> Tag { get; set; } = new List<string> { "Ammo" };
    public override string CardDiscription { get; set; } =
@"<Smoke Trap>

Can not be Played

When Shuffle: 
Cancel all enemy movement

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
        foreach (int InGameID in GM_Creature.UpdateCreature().Keys)
        {

            GM_Creature.UpdateCreature()[InGameID].CurrentStat = 0;
            GM_Creature.UpdateCreature()[InGameID].CurrentAction = 0;


        }

        return false;
    }

    public override async Task<bool> WhenDiscard()
    {


        return false;
    }


}
