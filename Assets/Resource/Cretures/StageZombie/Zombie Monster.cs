using TMPro;
using UnityEngine;

public class Zombie_Monster : Creature
{
    // 全部覆写属性
    public override string Name { get; set; } = "Zombie Monster";
    public override int ID { get; set; } = 006;
    public override int InGameID { get; set; } = 7;
    public override int HP { get; set; } = 500;
    public override bool IsEnemy { get; set; } = true;
    public override int MaxStun { get; set; } = 80;
    public override int Stun { get; set; } = 0;
    public override string ArmorType { get; set; } = "B";
    public override string DefaultArmorType { get; set; } = "B";
    public override int CurrentAction { get; set; } = 0;
    public override int CurrentStat { get; set; } = 0;


    //private important logical variables
    int ChargeLevel = 0;


    public override bool CheckStun()
    {
        if (this.Stun >= this.MaxStun)
        {
            this.Stun = 0; // 或者你有更合适的处理方式
            this.CurrentAction = 0; // 假设 Action 是一个 List、Queue 或 Dictionary
            ChargeLevel = 0; //Stun break its Charge
            return true;
        }

        return false;
    }


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
                    MyPlan.text = $"Enemy{this.InGameID}:[Charge1]:heal itself to 100，This enemy selfstun 30";
                    CurrentStat = 3;
                    break;

                case 2:
                    MyPlan.text = $"Enemy{this.InGameID}:[Charge2]:heal itself to 300，This enemy selfstun 30";
                    CurrentStat = 3;
                    break;

                case 3:
                    MyPlan.text = $"Enemy{this.InGameID}:[Virus Spewing]:deals 30 kinetic damage to Player";
                    CurrentStat = 1;
                    break;

                case 4:
                    MyPlan.text = $"Enemy{this.InGameID}:[Summon Horde]:loss 200 HP, and spawn a 200 HP Whiners";
                    CurrentStat = 3;
                    break;

                case 5:
                    MyPlan.text = $"Enemy{this.InGameID}:[Ram]:loss 100 HP and deal 100 kinetic damage to Player.";
                    CurrentStat = 1;
                    break;
            }
        }

        // Show its stat by UI




    }










    public override void PickAction()
    {
        // Picking Action
        if (ChargeLevel == 0)
        {
            CurrentAction = 1;
        }


        if (ChargeLevel == 1 && GM_Creature.NewInGameID <= 6)
        {
            int[] options = { 2, 3, 4 };
            float[] weights = { 0.4f, 0.4f, 0.2f }; 
            CurrentAction = WeightedRandom(options, weights);
        }        
        
        if (ChargeLevel == 1 && GM_Creature.NewInGameID > 6)
        {
            int[] options = { 2, 3 };
            float[] weights = { 0.5f, 0.5f }; 
            CurrentAction = WeightedRandom(options, weights);
        }

        if (ChargeLevel == 2)
        {
            CurrentAction = 5;
        }


        Debug.Log(Name + " picks action " + CurrentAction);
    }


    public override void Action0()
    {
        // Do nothing
        // Show UI to tell this enemy will do nothing
        Debug.Log(Name + " does nothing.");
    }

    public override void Action1() //Cast [Charge1]
    {
        ChargeLevel = 1;
        if (HP < 100)
        {
            HP = 100;
        }
        Stun += 30;
        CheckStun();

        Debug.Log(Name + " performs [Charge1]!");
    }

    public override void Action2() //Cast [Charge2]
    {
        ChargeLevel = 2;
        if (HP < 300)
        {
            HP = 300;
        }
        Stun += 30;
        CheckStun();
        Debug.Log(Name + " performs [Charge2]!");
    }

    public override void Action3()//ATK [Virus Spewing]
    {
        GM_Creature.Damage(0,"K",30);
        Debug.Log(Name + " performs [Command: Hold]!");
    }

    public override void Action4()//Cast [Summon Horde]:
    {
        HP -= 200;
        GM_Creature.Spawn(200, 001, "StageZombie");
        CheckHP();
        Debug.Log(Name + " performs [Summon Horde]!");
    }

    public override void Action5() //ATK [Ram]
    {
        GM_Creature.Damage(0, "K", 100);
        HP -= 100;
        CheckHP();
        Debug.Log(Name + " performs [Ram]!");
    }
}
