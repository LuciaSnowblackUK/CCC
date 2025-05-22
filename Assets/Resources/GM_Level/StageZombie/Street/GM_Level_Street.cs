using System.Threading;
using UnityEngine;

public class GM_Level_Street : GM_Level
{
    public override int LevelID { get; set; } = 001;
    public override int LevelProgress { get; set; } = 0; //at which sub level of this level

    // The Important Variables --------------------------------------------------------------------------
    public int Timer_Reinforce_Easy = 0;   
    public int Timer_Reinforce_Middle = 0;
    public int Timer_Reinforce_Hard = 0;

    // Awake, if needed



    // The Events -----------------------------------------------------------------------------------------
    public override void SettleEvents()
    {
        // the prepare turn
        if (LevelProgress == 0)
        {
            LevelProgress = 1;
        }

        // the Easy Waves 3 Waves
        if (LevelProgress >=1 && LevelProgress < 4)
        {
            Timer_Reinforce_Easy --;

            if (CheckEliminateAll())
            {
                int choice = Random.Range(0, 4); // 返回 0, 1, 2, 或 3 中的一个
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Easy = 4;
                        EasyWave1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Easy = 4;
                        EasyWave2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Easy = 4;
                        EasyWave3();
                        Debug.Log("选中了第三个选项");
                        break;
                    case 3:
                        Timer_Reinforce_Easy = 4;
                        EasyWave4();
                        Debug.Log("选中了第四个选项");
                        break;
                }

            }

            if (Timer_Reinforce_Easy == 0)
            {
                int choice = Random.Range(0, 4); // 返回 0, 1, 2, 
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Easy = 4;
                        EasyReinforce1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Easy = 4;
                        EasyReinforce2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Easy = 4;
                        EasyReinforce3();
                        Debug.Log("选中了第三个选项");
                        break;
                }
            }

        }

        //the Middle Waves 5 Waves
        if (LevelProgress >=4 && LevelProgress < 9)
        {
            Timer_Reinforce_Easy--;
            Timer_Reinforce_Middle--;

            if (CheckEliminateAll())
            {
                int choice = Random.Range(0, 4); // 返回 0, 1, 2, 或 3 中的一个
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleWave1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleWave2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleWave3();
                        Debug.Log("选中了第三个选项");
                        break;
                    case 3:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleWave4();
                        Debug.Log("选中了第四个选项");
                        break;
                }

            }

            if (Timer_Reinforce_Easy == 0)
            {
                int choice = Random.Range(0, 3); // 返回 0, 1, 2, 
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Easy = 4;
                        EasyReinforce1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Easy = 4;
                        EasyReinforce2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Easy = 4;
                        EasyReinforce3();
                        Debug.Log("选中了第三个选项");
                        break;
                }
            }

            if (Timer_Reinforce_Middle == 0)
            {
                int choice = Random.Range(0, 4); // 返回 0, 1, 2, 或 3 中的一个
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleReinforce1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleReinforce2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleReinforce3();
                        Debug.Log("选中了第三个选项");
                        break;
                    case 3:
                        Timer_Reinforce_Easy = 4;
                        Timer_Reinforce_Middle = 7;
                        MiddleReinforce4();
                        Debug.Log("选中了第四个选项");
                        break;
                }
            }

        }

        //the Hard Waves 5 Waves
        if (LevelProgress >= 9 && LevelProgress < 15)
        {
            Timer_Reinforce_Hard--;
            Timer_Reinforce_Middle--;

            if (CheckEliminateAll())
            {
                int choice = Random.Range(0, 4); // 返回 0, 1, 2, 或 3 中的一个
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Hard = 7;
                        Timer_Reinforce_Middle = 4;
                        HardWave1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Hard = 7;
                        Timer_Reinforce_Middle = 4;
                        HardWave2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Hard = 7;
                        Timer_Reinforce_Middle = 4;
                        HardWave3();
                        Debug.Log("选中了第三个选项");
                        break;
                    case 3:
                        Timer_Reinforce_Hard =7;
                        Timer_Reinforce_Middle = 4;
                        HardWave4();
                        Debug.Log("选中了第四个选项");
                        break;
                }

            }

            if (Timer_Reinforce_Middle == 0)
            {
                int choice = Random.Range(0, 4); // 返回 0, 1, 2, 或 3 中的一个
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Middle = 4;
                        MiddleReinforce1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Middle = 4;
                        MiddleReinforce2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Middle = 4;
                        MiddleReinforce3();
                        Debug.Log("选中了第三个选项");
                        break;
                    case 3:
                        Timer_Reinforce_Middle = 4;
                        MiddleReinforce4();
                        Debug.Log("选中了第四个选项");
                        break;
                }
            }

            if (Timer_Reinforce_Hard == 0)
            {
                int choice = Random.Range(0, 4); // 返回 0, 1, 2, 
                LevelProgress++;
                switch (choice)
                {
                    case 0:
                        Timer_Reinforce_Middle = 4;
                        Timer_Reinforce_Hard = 7;
                        HardReinforce1();
                        Debug.Log("选中了第一个选项");
                        break;
                    case 1:
                        Timer_Reinforce_Middle = 4;
                        Timer_Reinforce_Hard = 7;
                        HardReinforce2();
                        Debug.Log("选中了第二个选项");
                        break;
                    case 2:
                        Timer_Reinforce_Middle = 4;
                        Timer_Reinforce_Hard = 7;
                        HardReinforce3();
                        Debug.Log("选中了第三个选项");
                        break;
                    case 3:
                        Timer_Reinforce_Middle = 4;
                        Timer_Reinforce_Hard = 7;
                        HardReinforce4();
                        Debug.Log("选中了第三个选项");
                        break;
                }
            }


        }

        //The End You Win
        if (LevelProgress >= 15)
        {
            // you win, exit the scene
        }
    }


    // Wave Spawners --------------------------------------------------------------------------------------

    //Easy Waves---------------------------
    public void EasyWave1()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(400,001,"StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(400, 001, "StageZombie");
        }

    }

    public void EasyWave2()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(400, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }
    }

    public void EasyWave3()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(400, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }

    public void EasyWave4()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(300, 006, "StageZombie");
        }

    }

    //Middle Wasves------------------------
    public void MiddleWave1()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(800, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 004, "StageZombie");
        }
    }

    public void MiddleWave2()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(800, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }

    public void MiddleWave3()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(800, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }

    public void MiddleWave4()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(800, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(500, 006, "StageZombie");
        }
    }

    //Hard Waves--------------------------
    public void HardWave1()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(1000, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 004, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 005, "StageZombie");
        }
    }

    public void HardWave2()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(1000, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(1000, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }

    public void HardWave3()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(1000, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(500, 006, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }

    public void HardWave4()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(500, 006, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }
    }

    // Reinforce Spawners ---------------------------------------------------------------------------------

    //Easy Reinforce---------------------------
    public void EasyReinforce1()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(400, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(400, 001, "StageZombie");
        }
    }

    public void EasyReinforce2()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }

    }

    public void EasyReinforce3()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }

    }

    //Middle Reinforce------------------------
    public void MiddleReinforce1()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(600, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(600, 001, "StageZombie");
        }
    }

    public void MiddleReinforce2()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(600, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }
    }

    public void MiddleReinforce3()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 005, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }

    public void MiddleReinforce4()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 004, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }
    //Hard Reinforce--------------------------
    public void HardReinforce1()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(600, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(600, 001, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 005, "StageZombie");
        }
    }

    public void HardReinforce2()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(100, 002, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }

    public void HardReinforce3()
    {

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 004, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 005, "StageZombie");
        }
    }

    public void HardReinforce4()
    {
        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(500, 006, "StageZombie");
        }

        if (GM_Creature.NewInGameID < 6)
        {
            GM_Creature.Spawn(200, 003, "StageZombie");
        }
    }
}
