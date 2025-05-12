using TMPro;
using UnityEngine;

public class Stick_Whiner : Creature
{
    // 全部覆写属性
    public override string Name { get; set; } = "Stick Whiner";
    public override int ID { get; set; } = 002;
    public override int InGameID { get; set; } = 7;
    public override int HP { get; set; } = 100;
    public override bool IsEnemy { get; set; } = true;
    public override int MaxStun { get; set; } = 20;
    public override int Stun { get; set; } = 0;
    public override string ArmorType { get; set; } = "S";
    public override string DefaultArmorType { get; set; } = "S";
    public override int CurrentAction { get; set; } = 0;
    public override int CurrentStat { get; set; } = 0;


    //private important logical variables
    bool IfRaisedStick = false;
    bool IfStunLastTurn = false;


    // 覆写逻辑方法






    public override void Update()
    {

        // Write its plan on text
        if (this.InGameID > 0 && this.InGameID < 6)
        {
            MyPlanName = "InGameID" + $"{this.InGameID}" + "Plan";
            MyPlan = GameObject.Find(nameof(MyPlanName)).GetComponent<TMP_Text>();

            switch (CurrentAction)
            {
                case 0:
                    MyPlan.text = $"Enemy{this.InGameID}:[Do nothing]";
                    CurrentStat = 0;
                    break;

                case 1:
                    MyPlan.text = $"Enemy{this.InGameID}:[Stick Hit]:Deal 10 Kinetic Damage to Player";
                    CurrentStat = 1;
                    break;

                case 2:
                    MyPlan.text = $"Enemy{this.InGameID}:[Raise the Stick]";
                    CurrentStat = 1;
                    break;

                case 3:
                    MyPlan.text = $"Enemy{this.InGameID}:[Heavy Smash]:Deal 50 Kinetic Damage to Player!";
                    CurrentStat = 1;
                    break;

                case 4:
                    MyPlan.text = $"Enemy{this.InGameID}:[Defend]:Become Armored for 1 turn";
                    CurrentStat = 2;
                    break;
            }
        }

        // Show its stat by UI




    }














    public override bool CheckStun()
    {
        if (this.Stun >= this.MaxStun)
        {
            this.Stun = 0; // 或者你有更合适的处理方式
            this.CurrentAction = 0; // 假设 Action 是一个 List、Queue 或 Dictionary
            IfStunLastTurn = true;
            return true;
        }

        return false;
    }


    public override void PickAction()
    {
        // Picking Action
        if (!IfStunLastTurn)
        {
            if (!IfRaisedStick)
            {
                int[] options = { 1, 2 };
                float[] weights = { 0.3f, 0.7f }; // 权重为 70%, 30%
                CurrentAction = WeightedRandom(options, weights);
            }

            if (IfRaisedStick)
            {
                CurrentAction = 3;
            }
        }

        if (IfStunLastTurn)
        {
            CurrentAction = 4;
            IfStunLastTurn = false;
        }

        Debug.Log(Name + " picks action " + CurrentAction);
    }

    public override void Action0()
    {
        // Do nothing
        // Show UI to tell this enemy will do nothing
        Debug.Log(Name + " does nothing.");
    }

    public override void Action1() //[Stick Hit]
    {
        GM_Creature.Damage(0, "K", 10);
        Debug.Log(Name + " performs [Stick Hit]!");
    }

    public override void Action2() //[Raise the Stick]
    {
        IfRaisedStick = true;
        Debug.Log(Name + " performs [Raise the Stick]!");
    }

    public override void Action3()//[Heavy Smash]
    {
        GM_Creature.Damage(0, "K", 50);
        IfRaisedStick = false;
        Debug.Log(Name + " performs [Heavy Smash]!");
    }

    public override void Action4()//[Defend]
    {
        ArmorType = "A";
        Debug.Log(Name + " performs [Defend]!");
    }

    public override void Action5()
    {
        Debug.Log(Name + " performs [X]!");
    }
}
