using TMPro;
using UnityEngine;

public class Whiners : Creature
{
    // 全部覆写属性
    public override string Name { get; set; } = "Whiners";
    public override int ID { get; set; } = 001;
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





    public override void Update()
    {

        switch (CurrentAction)
        {
            case 0:
                MyPlan = $"Enemy{this.InGameID}_Whiners:[Do nothing]";
                CurrentStat = 0;
                break;

            case 1:
                MyPlan = $"Enemy{this.InGameID}_Whiners:[Shout and Bite]:Deal 10 Kinetic Damage to Player";
                CurrentStat = 1;
                break;

            case 2:
                MyPlan = $"Enemy{this.InGameID}_Whiners:[Rapid Bite]:Deal 20 Kinetic Damage to Player";
                CurrentStat = 1;
                break;

            case 3:
                MyPlan = $"Enemy{this.InGameID}_Whiners:[Hold You]:Add 2 [Surrounded] card to Hand!";
                CurrentStat = 3;
                break;

            case 4:
                MyPlan = $"Enemy{this.InGameID}_Whiners:[Surround You]:loss 200 HP, and spawn a 200 HP Whiners";
                CurrentStat = 3;
                break;

            case 5:
                MyPlan = $"Enemy{this.InGameID}_Whiners:[Reinforcing]:Heal itself 200 HP";
                CurrentStat = 3;
                break;
        }
        

    }











    public override void PickAction()
    {
        // Picking Action
        if (HP > 200 && GM_Creature.NewInGameID <= 6)
        {
            int[] options = { 1, 4 };
            float[] weights = { 0.7f, 0.3f }; // 权重为 70%, 30%
            CurrentAction = WeightedRandom(options, weights);
        }

        if (HP > 200 && GM_Creature.NewInGameID > 6)
        {
            CurrentAction = 2;
        }

        if (HP <= 200)
        {
            int[] options = { 2, 3, 5};
            float[] weights = { 0.2f, 0.4f, 0.4f}; 
            CurrentAction = WeightedRandom(options, weights);
        }

        Debug.Log(Name + " picks action " + CurrentAction);
    }



    public override void Action0()
    {
        // Do nothing
        // Show UI to tell this enemy will do nothing
        Debug.Log(Name + " does nothing.");
    }

    public override void Action1() //[Shout and Bite]
    {
        GM_Creature.Damage(0,"K",10);
        Debug.Log(Name + " performs [Shout and Bite]!");
    }

    public override void Action2() //[Rapid Bite]
    {
        GM_Creature.Damage(0, "K", 20);
        Debug.Log(Name + " performs [Rapid Bite]!");
    }

    public override void Action3()//[Hold You]
    {
        GM_Card.Add(9991,"BadCard","Hand",false);
        GM_Card.Add(9991,"BadCard","Hand",false);

        Debug.Log(Name + " performs [Hold You]!");
    }

    public override void Action4()//[Surround You]
    {
        HP -= 200;
        GM_Creature.Spawn(200,001,"StageZombie");
        this.CheckHP(); 
        Debug.Log(Name + " performs [Surround You]!");
    }

    public override void Action5() //[Reinforcing]
    {
        HP += 200;
        Debug.Log(Name + " performs [Reinforcing]!");
    }
}
