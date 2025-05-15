using UnityEngine;

public class Dummy : Creature
{
    // 全部覆写属性
    public override string Name { get; set; } = "Dummy";
    public override int ID { get; set; } = 999;
    public override int InGameID { get; set; } = 7;
    public override int HP { get; set; } = 1000;
    public override bool IsEnemy { get; set; } = true;
    public override int MaxStun { get; set; } = 10;
    public override int Stun { get; set; } = 0;
    public override string ArmorType { get; set; } = "H";
    public override string DefaultArmorType { get; set; } = "H";
    public override int CurrentAction { get; set; } = 0;
    public override int CurrentStat { get; set; } = 0;

    // 覆写逻辑方法
    public override bool CheckStun()
    {
        if (Stun >= MaxStun)
        {
            Stun = 0;
            CurrentAction = 0;
            Debug.Log(Name + " is stunned!");
            return true;
        }
        return false;
    }

    public override bool CheckHP()
    {
        if (HP <= 0)
        {
            Debug.Log(Name + " is dead!");
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    public override void ResetArmorType()
    {
        ArmorType = DefaultArmorType;
        Debug.Log(Name + "'s armor reset.");
    }


    public override void PickAction()
    {
        CurrentAction = Random.Range(0, 6);
        Debug.Log(Name + " picks action " + CurrentAction);
    }

    public override void Action0()
    {
        Debug.Log(Name + " does nothing.");
    }

    public override void Action1()
    {
        Debug.Log(Name + " performs custom Action 1!");
    }

    public override void Action2()
    {
        Debug.Log(Name + " performs custom Action 2!");
    }

    public override void Action3()
    {
        Debug.Log(Name + " performs custom Action 3!");
    }

    public override void Action4()
    {
        Debug.Log(Name + " performs custom Action 4!");
    }

    public override void Action5()
    {
        Debug.Log(Name + " performs custom Action 5!");
    }
}
