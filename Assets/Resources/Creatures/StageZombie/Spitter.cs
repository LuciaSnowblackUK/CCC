using TMPro;
using UnityEngine;

public class Spitter : Creature
{
    // 全部覆写属性
    public override string Name { get; set; } = "Spitter";
    public override int ID { get; set; } = 003;
    public override int InGameID { get; set; } = 7;
    public override int HP { get; set; } = 200;
    public override bool IsEnemy { get; set; } = true;
    public override int MaxStun { get; set; } = 30;
    public override int Stun { get; set; } = 0;
    public override string ArmorType { get; set; } = "S";
    public override string DefaultArmorType { get; set; } = "S";
    public override int CurrentAction { get; set; } = 0;
    public override int CurrentStat { get; set; } = 0;


    //private important logical variables
    bool IfIdleLastTurn = true;
    int TargetEnemyInGameID;
    Creature TargetEnemy;


    // 事件处理方法（你可以在这里处理事件触发时的逻辑）
    public override void HandleTargetCheck()
    {
        if (TargetEnemy == null)
        {
            CurrentAction = 0;
            CurrentStat = 0;
        }
        // 在这里实现你想要的逻辑
        Debug.Log("Target check triggered in Creature!");
    }

    // 覆写逻辑方法




    public override void Update()
    {



        switch (CurrentAction)
        {
            case 0:
                MyPlan = $"Enemy{this.InGameID}_Spitter:[Do nothing]";
                CurrentStat = 0;
                break;

            case 1:
                MyPlan = $"Enemy{this.InGameID}_Spitter:[Spit Acid]:Deal 10 ion Damage to Player, make you H 1 turn";
                CurrentStat = 1;
                break;

            case 2:
                MyPlan = $"Enemy{this.InGameID}_Spitter:[Regenerative Virus]:(Enemy{this.TargetEnemyInGameID}):Heal 100 it if HP lower than 200";
                CurrentStat = 3;
                break;

            case 3:
                MyPlan = $"Enemy{this.InGameID}_Spitter:Harden Virus]:(Enemy{this.TargetEnemyInGameID}):it become A for 1 turn";
                CurrentStat = 3;
                break;

            case 4:
                MyPlan = $"Enemy{this.InGameID}_Spitter:[Rest]:Do nothing";
                CurrentStat = 0;
                break;

        }
        // Show its stat by UI




    }















    public override bool CheckStun()
    {
        if (this.Stun >= this.MaxStun)
        {
            this.Stun = 0; // Clean Stun
            this.CurrentAction = 0; // Be Idle
            IfIdleLastTurn = true;
            return true;
        }

        return false;
    }


    public override void PickAction()
    {
        // pick the target first Randomly Choose an Enemy, could be itself
        foreach (int InGameID in GM_Creature.CreatureList.Keys)
        {
            if (GM_Creature.CreatureList[InGameID].InGameID == 0)
            {
                continue;//Skip the Player
            }

            if (GM_Creature.CreatureList[InGameID].InGameID != 0)
            {
                while (TargetEnemy = null)
                {
                    if (Random.value < 0.5f)// 有 50% 概率执行这个分支
                    {
                        TargetEnemyInGameID = InGameID;
                        TargetEnemy = GM_Creature.CreatureList[InGameID];
                        break;
                    }
                }

            }
        }

        // Picking Action
        if (!IfIdleLastTurn)
        {
            CurrentAction = 4;
        }


        if (IfIdleLastTurn)
        {
            int[] options = { 1, 2, 3 };
            float[] weights = { 0.3f, 0.3f, 0.3f }; // 
            CurrentAction = WeightedRandom(options, weights);

        }

        Debug.Log(Name + " picks action " + CurrentAction);
    }

    public override void Action0()
    {
        // Do nothing
        // Show UI to tell this enemy will do nothing
        Debug.Log(Name + " does nothing.");
        TargetEnemy = null;
    }

    public override void Action1() //ATK [Spit Acid]
    {
        GM_Creature.Damage(0, "I", 10);
        GM_Creature.Type(0,"H");
        Debug.Log(Name + " performs [Spit Acid]!");
        TargetEnemy = null;
    }

    public override void Action2() //[Regenerative Virus]
    {
        if (TargetEnemy != null)
        {
            if (GM_Creature.UpdateCreature()[TargetEnemyInGameID].HP < 200)
            {
                GM_Creature.Heal(TargetEnemyInGameID, 100);
            }

        }

        TargetEnemy = null;

        Debug.Log(Name + " performs [Regenerative Virus]!");
    }

    public override void Action3()//[Harden Virus]
    {
        if (TargetEnemy != null)
        {
            GM_Creature.Type(TargetEnemyInGameID, "A");
        }

        TargetEnemy = null;

        Debug.Log(Name + " performs [Harden Virus]!");
    }

    public override void Action4()//[Rest]
    {
        //Do nothing
        Debug.Log(Name + " performs [Rest]!");
        TargetEnemy = null;
    }

    public override void Action5()
    {
        Debug.Log(Name + " performs [X]!");
    }
}
