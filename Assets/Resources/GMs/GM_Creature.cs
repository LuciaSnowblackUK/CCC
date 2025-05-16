using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;

public class GM_Creature : MonoBehaviour
{
    //Pubulic Variables
    public int EnemyCount; //How mant enemies in the Scene
    public int NewInGameID = 1; //the new InGameID for newly spawn creature, In-game ID for tracking, since we only have 1 player and max 6 enemy so [0,1,2,3,4,5,6], default 7 means a prefab

    //Private Variables
    private Dictionary<int, Creature> CreatureList = new Dictionary<int, Creature>(); //This is the refer list of the Creatures,<InGameID: Creature >

    //Important Refers
    public GM_Card GM_Card;
    public GM_Global GM_Global;
    public GM_Level GM_Level;


    //SomeImportant Boardcast and its event
    // 1. 定义委托
    public delegate void TargetCheckHandler(); public event TargetCheckHandler OnTargetCheck;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");
    }

    private void Update()
    {
        UpdateCreature();

        foreach (Transform child in transform)
        {
            Creature creature = child.GetComponent<Creature>();
            RectTransform rectTr = child.GetComponent<RectTransform>();

            if (creature == null || rectTr == null) continue;

            switch (creature.InGameID)
            {
                case 0:
                    rectTr.anchoredPosition = new Vector2(-5.5f, 0);
                    break;

                case 1:
                    rectTr.anchoredPosition = new Vector2(-1.5f, -1);
                    break;

                case 2:
                    rectTr.anchoredPosition = new Vector2(1, -1);
                    break;

                case 3:
                    rectTr.anchoredPosition = new Vector2(3.5f, -1);
                    break;

                case 4:
                    rectTr.anchoredPosition = new Vector2(-1.5f, 2);
                    break;

                case 5:
                    rectTr.anchoredPosition = new Vector2(1, 2);
                    break;

                case 6:
                    rectTr.anchoredPosition = new Vector2(3.5f, 2);
                    break;
            }
        }
    }


    //Method: Update the information of all Creatures, the other object use this to get the CreatureList
    public Dictionary<int, Creature> UpdateCreature()
    {
        foreach (Transform child in transform)
        {
            Creature creature = child.GetComponent<Creature>();
            if (creature != null)
            {
                CreatureList[creature.InGameID] = creature; // InGameID:Creature
            }
        }

        return this.CreatureList;
    }

    //Method: Deal Damage to the Creature by InGameID, adjust by the Creature.ArmorType , return true when fully kill target
    public bool Damage(int TargetInGameID,string DamageType,int DamageAmount)
    {
        UpdateCreature();
        Creature TargetCreature = CreatureList[TargetInGameID];
        if (TargetCreature == null) return false;

        switch (DamageType)
        {
            // The Kinetic Damage
            case "K":
                switch (TargetCreature.ArmorType)
                {
                    case "H":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "S":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "B":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "A":
                        TargetCreature.HP -= DamageAmount * 0;
                        break;

                }

                break;

            case "E":
                switch (TargetCreature.ArmorType)
                {
                    case "H":
                        TargetCreature.HP -= DamageAmount * 5;
                        break;

                    case "S":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "B":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "A":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                }
                break;

            case "I":
                switch (TargetCreature.ArmorType)
                {
                    case "H":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "S":
                        TargetCreature.HP -= DamageAmount * 5;
                        break;

                    case "B":
                        TargetCreature.HP -= DamageAmount * 2;
                        break;

                    case "A":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                }
                break;

            case "V":
                switch (TargetCreature.ArmorType)
                {
                    case "H":
                        TargetCreature.HP -= DamageAmount * 10;
                        break;

                    case "S":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "B":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                    case "A":
                        TargetCreature.HP -= DamageAmount * 1;
                        break;

                }
                break;

        }

        bool Kill = TargetCreature.CheckHP();

        UpdateCreature();

        if (Kill)
        {
            this.NewInGameID--;
            //TargetInGameID
            foreach (int InGameID in CreatureList.Keys)
            {
                if (CreatureList[InGameID].InGameID > TargetInGameID)
                {
                    CreatureList[InGameID].InGameID -= 1;
                }
            }

            // 广播“检测目标”事件
            OnTargetCheck?.Invoke();
        }

        UpdateCreature();
        return Kill;

    }

    //Method: Heal to the Creature by InGameID
    public bool Heal(int TargetInGameID,int HealAmount)
    {
        Creature TargetCreature = CreatureList[TargetInGameID];
        if (TargetCreature == null) return false;

        TargetCreature.HP += HealAmount;
        UpdateCreature();   
        return true;
    }

    //Method: Stun to the Creature by InGameID, return true when fully stun target
    public bool Stun(int TargetInGameID,int StunAmount)
    {
        Creature TargetCreature = CreatureList[TargetInGameID];
        if (TargetCreature == null) return false;

        TargetCreature.Stun += StunAmount;

        bool Stun = TargetCreature.CheckStun();
        UpdateCreature();
        return Stun;
    }

    //Method: Change the ArmorType by InGameID
    public bool Type(int TargetInGameID, string ArmorType)
    {
        Creature TargetCreature = CreatureList[TargetInGameID];
        if (TargetCreature == null) return false;

        TargetCreature.ArmorType = ArmorType;
        UpdateCreature() ;  
        return true;
    }

    //Method: To Spawn a Creature by ID, and grants it NewCreatureID, VERY IMPORTANT YOU HAVE TO KNOW WHAT STAGE THE CREATURE BELONGS TO
    public bool Spawn(int HP, int MonsterID, string Stage)
    {
        if (NewInGameID >= 6)
        {
            return false; // cant have more than 6 enemies
        }

        // 拼接路径时要注意加上 "/"
        GameObject[] spawnList = Resources.LoadAll<GameObject>("Creatures/" + Stage);

        foreach (GameObject prefab in spawnList)
        {
            Creature creature = prefab.GetComponent<Creature>();
            if (creature != null && creature.ID == MonsterID)
            {
                GameObject instance = Instantiate(prefab,this.transform);
                Creature newCreature = instance.GetComponent<Creature>();
                newCreature.HP = HP;
                newCreature.InGameID = NewInGameID;
                NewInGameID++;
                UpdateCreature();
                return true;
            }
        }

        Debug.LogWarning("Monster with ID " + MonsterID + " not found in stage: " + Stage);
        return false;
    }

    // Ask all creature To Pick and Show their Action
    public void AllPickAction()
    {
        UpdateCreature();
        for (int i = 6; i > 0; i--)
        {
            if (CreatureList.ContainsKey(i))
            {
                Creature Creature = CreatureList[i];
                Creature.PickAction();                 //the later the enemy comes,the ealier it choose and show its action
            }
        }
    }

    // Ask all creature To Do their Action
    public void AllDoAction()
    {
        UpdateCreature();
        for (int i = 6; i > 0; i--)
        {
            if (CreatureList.ContainsKey(i))
            {
                Creature Creature = CreatureList[i];
                int theAction = Creature.CurrentAction;
                Creature.DoAction(theAction);                 //the later the enemy comes,the ealier it choose and show its action
            }
        }
    }

    // To clean all ArmorType Change effects, IsBegin [true:TurnBegin, false:EndTurn]
    public void AllResetArmorType(bool IsBegin) 
    {
        if (IsBegin)
        {
            foreach (Creature Creature in CreatureList.Values)
            {
                if (!Creature.IsEnemy)
                {
                    Creature.ResetArmorType(); //Clear ArmorType Change effects of player at the begin of turn

                }
            }
            
        }

        else
        {
            foreach (Creature Creature in CreatureList.Values)
            {
                if (Creature.IsEnemy)
                {
                    Creature.ResetArmorType(); //Clear ArmorType Change effects of enemy at the end of turn, before their actions

                }
 
            }
        }
    }
}
