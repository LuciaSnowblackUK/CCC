using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie_GunBoss : Creature
{
    // 全部覆写属性
    public override string Name { get; set; } = "Zombie GunBoss";
    public override int ID { get; set; } = 004;
    public override int InGameID { get; set; } = 7;
    public override int HP { get; set; } = 200;
    public override bool IsEnemy { get; set; } = true;
    public override int MaxStun { get; set; } = 20;
    public override int Stun { get; set; } = 0;
    public override string ArmorType { get; set; } = "S";
    public override string DefaultArmorType { get; set; } = "S";
    public override int CurrentAction { get; set; } = 0;
    public override int CurrentStat { get; set; } = 0;


    //private important logical variables
    bool IfFirstTurn = true;
    bool IfHordeExsist = false;
    int TargetEnemyInGameID;
    Creature TargetEnemy;


    // 事件处理方法（你可以在这里处理事件触发时的逻辑）
    public override void HandleTargetCheck(int dyingID)
    {
        if (TargetEnemyInGameID == dyingID)
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
                    MyPlan = $"Enemy{this.InGameID}_Zombie_GunBoss:[Do nothing]";
                    CurrentStat = 0;
                    break;

                case 1:
                    MyPlan = $"Enemy{this.InGameID}_Zombie_GunBoss:[Shoot]:Deal 10 ion Damage to Player";
                    CurrentStat = 1;
                    break;

                case 2:
                    MyPlan = $"Enemy{this.InGameID}_Zombie_GunBoss:[Command: Surround]:(Enemy{this.TargetEnemyInGameID}):it does [Surround]";
                    CurrentStat = 3;
                    break;

                case 3:
                    MyPlan = $"Enemy{this.InGameID}_Zombie_GunBoss:[Command: Hold]:(Enemy{this.TargetEnemyInGameID}):it does [Hold You]";
                    CurrentStat = 3;
                    break;

                case 4:
                    MyPlan = $"Enemy{this.InGameID}_Zombie_GunBoss:[Command: Reinforcing]:(Enemy{this.TargetEnemyInGameID}):it does [Reinforcing]";
                    CurrentStat = 3;
                    break;

            }
        

        // Show its stat by UI

    }





    public override void PickAction()
    {
        // find Horde enemy
        // 先筛选符合条件的敌人ID，存到列表里
        List<int> candidateIDs = new List<int>();

        foreach (int InGameID in GM_Creature.CreatureList.Keys)
        {
            if (GM_Creature.CreatureList[InGameID].InGameID == 0)
                continue; // 跳过玩家自己

            if (GM_Creature.CreatureList[InGameID].ArmorType == "H") // 举例条件：ArmorType是“H”
            {
                candidateIDs.Add(InGameID);
            }
        }

        // 从符合条件的列表中随机选一个作为目标
        int TargetEnemyInGameID = -1;
        Creature TargetEnemy = null;

        if (candidateIDs.Count > 0)
        {
            IfHordeExsist = true;
            int randomIndex = UnityEngine.Random.Range(0, candidateIDs.Count);
            TargetEnemyInGameID = candidateIDs[randomIndex];
            TargetEnemy = GM_Creature.CreatureList[TargetEnemyInGameID];
        }
        else
        {
            IfHordeExsist = false;
        }



        // Picking Action
        if (IfFirstTurn || IfHordeExsist == false)
        {
            CurrentAction = 1;
        }

        if (!IfFirstTurn)
        {
            int[] options = { 2, 3, 4 };
            float[] weights = { 0.4f, 0.4f, 0.2f }; // 
            CurrentAction = WeightedRandom(options, weights);

        }


        // We need an UI to show the enemy action here

        Debug.Log(Name + " picks action " + CurrentAction);
    }

    public override void Action0()
    {
        // Do nothing
        // Show UI to tell this enemy will do nothing
        Debug.Log(Name + " does nothing.");
        TargetEnemy = null;
    }

    public override void Action1() //ATK [Shoot]
    {
        GM_Creature.Damage(0, "I", 10);
        Debug.Log(Name + " performs [Shoot]!");
        IfFirstTurn = false;
        TargetEnemy = null;
    }

    public override void Action2() //Cast [Command: Surround]
    {
        GM_Creature.UpdateCreature()[TargetEnemyInGameID].DoAction(4);
        Debug.Log(Name + " performs [Command: Surround]!");
        TargetEnemy = null;
    }

    public override void Action3()//Cast [Command: Hold]
    {
        GM_Creature.UpdateCreature()[TargetEnemyInGameID].DoAction(3);
        Debug.Log(Name + " performs [Command: Hold]!");
        TargetEnemy = null;
    }

    public override void Action4()//Cast [Command: Reinforcing]:
    {
        GM_Creature.UpdateCreature()[TargetEnemyInGameID].DoAction(5);
        Debug.Log(Name + " performs [Command: Reinforcing]!");
        TargetEnemy = null;
    }

    public override void Action5()
    {
        Debug.Log(Name + " performs [X]!");
    }
}
