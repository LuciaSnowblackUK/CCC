using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Surrounded : Card
{
    //public Variables
    public override string Name { get; set; } = "Surrounded";
    public override int ID { get; set; } = 9991;
    public override List<string> Tag { get; set; } = new List<string> { "Bad" };

    public override string CardDiscription { get; set; } =
@"<Surrounded>

Can not be Played

When Discarded:
destroy this card

<Tag:Bad>";

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
