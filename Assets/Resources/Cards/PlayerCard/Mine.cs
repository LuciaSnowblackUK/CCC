using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Mine : Card
{
    //public Variables
    public override string Name { get; set; } = "Mine";
    public override int ID { get; set; } = 0013;
    public override List<string> Tag { get; set; } = new List<string> { "Ammo" };

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
        List<Creature> selectedCreatures = new List<Creature>();

        foreach (var kvp in GM_Creature.UpdateCreature())
        {
            int id = kvp.Key;
            Creature creature = kvp.Value;

            if (id >= 1 && id <= 6)
            {
                selectedCreatures.Add(creature);
            }
        }

        // 遍历这些 Creature 对象
        foreach (var creature in selectedCreatures)
        {
            if (creature.CurrentStat != 2)
            {
                creature.HP = 0;
                GM_Creature.Damage(creature.InGameID, "V", 1);
            }
        }


        if (GM_Card.ReturnHandCard().Count < 8 )
        {
            Destroy(this.gameObject);
            GM_Card.ReturnDeckCard();
        }


        return false;
    }

    public override async Task<bool> WhenDiscard()
    {


        return false;
    }


}
