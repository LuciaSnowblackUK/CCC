using UnityEngine;

public class Player : Creature
{
    // 全部覆写属性
    public override string Name { get; set; } = "Player";
    public override int ID { get; set; } = 1000;
    public override int InGameID { get; set; } = 0;
    public override int HP { get; set; } = 100;
    public override bool IsEnemy { get; set; } = false;
    public override int MaxStun { get; set; } = 9999;
    public override int Stun { get; set; } = 0;
    public override string ArmorType { get; set; } = "S";
    public override string DefaultArmorType { get; set; } = "S";
    public override int CurrentAction { get; set; } = 0;
    public override int CurrentStat { get; set; } = 0;

    // 覆写逻辑方法

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



}
