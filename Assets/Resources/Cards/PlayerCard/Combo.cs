using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Combo : Card
{
    //public Variables
    public override string Name { get; set; } = "Combo";
    public override int ID { get; set; } = 0042;
    public override List<string> Tag { get; set; } = new List<string> { "Melee" };

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




        return false;
    }

    public override async Task<bool> WhenDiscard()
    {

        Destroy(this.gameObject);
        return false;
    }


}
