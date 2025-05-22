using UnityEditor;
using UnityEngine;
using Unity.Mathematics;
using TMPro;
using System.Threading.Tasks;

public class Creature : MonoBehaviour
{
    // The Public Variables of a Creature
    public virtual string Name { get; set; } = "SampleMonster"; // The name of this creature
    public virtual int ID { get; set; } = 1001; // The ID of this creature
    public virtual int InGameID { get; set; } = 7; // In-game ID for tracking, since we only have 1 player and max 6 enemy so [0,1,2,3,4,5,6], default 7 means a prefab
    public virtual int HP { get; set; } = 1000; // HP
    public virtual bool IsEnemy { get; set; } = true; // If false it is player side, if true it is enemy
    public virtual int MaxStun { get; set; } = 10; // MaxStun
    public virtual int Stun { get; set; } = 0; // Begin in 0, Stun
    public virtual string ArmorType { get; set; } = "S"; // The ArmorType of the Creature [H:Horde, S:Specialist, B:Behemoth, A:Heavy Armored]
    public virtual string DefaultArmorType { get; set; } = "S"; // Default ArmorType
    public virtual int CurrentAction { get; set; } = 0; // It represents what the Creature plans to do this turn
    public virtual int CurrentStat { get; set; } = 0; // [0:Idle, 1:Attack, 2:Defend, 3:Casting]



    //Important Refers
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;
    public Animator Animator;

    //---------------------
    public string MyPlan;

    void Awake()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();
        Animator = GetComponent<Animator>();

        //订阅广播
        if (GM_Creature != null)
        {
            // 订阅 GM_Creature 类中的事件
            GM_Creature.OnTargetCheck += HandleTargetCheck;
        }

        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");
    }


    public virtual void Update()
    {
        

    }


    // 事件处理方法（你可以在这里处理事件触发时的逻辑）
    public virtual void HandleTargetCheck(int dyingID)
    {
        // 在这里实现你想要的逻辑
        Debug.Log("Target check triggered in Creature!");
    }

    // 在对象销毁时取消订阅事件，避免内存泄漏
    void OnDestroy()
    {
        if (GM_Creature != null)
        {
            GM_Creature.OnTargetCheck -= HandleTargetCheck;

        }
    }

    // 使用 Unity.Mathematics.Random 进行加权随机选择
    // 加权随机方法，直接调用即可
    public static Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint)System.DateTime.Now.Ticks);

    public static int WeightedRandom(int[] options, float[] weights)
    {
        float totalWeight = 0f;
        foreach (float w in weights) totalWeight += w;

        float randomValue = random.NextFloat(0f, totalWeight);

        float cumulativeWeight = 0f;
        for (int i = 0; i < options.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
                return options[i];
        }

        return options[options.Length - 1];
    }


    public virtual bool CheckStun()
    {
            if (this.Stun >= this.MaxStun)
            {
                this.Stun = 0; // 或者你有更合适的处理方式
                this.CurrentAction = 0; // 假设 Action 是一个 List、Queue 或 Dictionary
                return true;
            }

            return false;
    }

    public virtual async Task<bool> CheckHP()
    {
        if (this == null)return false;
        if (this.HP <= 0)
        {
            if (Animator != null)
            {
                Animator.SetTrigger("Die");
            }

            await Task.Delay(500);
            // 摧毁当前的生物对象
            if (this != null && this.gameObject != null) Destroy(this.gameObject); // 这个会销毁当前物体以及它所有的组件

            return true;
        }

        return false;
    }

    public virtual void ResetArmorType()
    {
        this.ArmorType = this.DefaultArmorType;
    }

    public virtual void DoAction(int theAction)
    {
        switch (theAction)
        {
            case 0:
                Action0();
                break;

            case 1:
                Action1();
                break;

            case 2:
                Action2();
                break;

            case 3: 
                Action3();
                break;

            case 4:
                Action4();
                break;

            case 5:
                Action5();
                break;
        }

        if (Animator != null)
        {
            Animator.SetTrigger("Attack");
        }
    }

    // To pick an action ------------------------------------------------------------------------ 6 action slots, from 0 to 5
        public virtual void PickAction()
        {
            // 默认的行动选择逻辑
            Debug.Log("Picking action for " + Name);
        }

        // Action Slot 0
        public virtual void Action0() //Action Zero is always left for do nothing
        {
            // 默认的 Action0 逻辑 
            Debug.Log(Name + " performs Action 0.");
        }

        // Action Slot 1
        public virtual void Action1()
        {
            // 默认的 Action1 逻辑
            Debug.Log(Name + " performs Action 1.");
        }

        // Action Slot 2
        public virtual void Action2()
        {
            // 默认的 Action2 逻辑
            Debug.Log(Name + " performs Action 2.");
        }

        // Action Slot 3
        public virtual void Action3()
        {
            // 默认的 Action3 逻辑
            Debug.Log(Name + " performs Action 3.");
        }

        // Action Slot 4
        public virtual void Action4()
        {
            // 默认的 Action4 逻辑
            Debug.Log(Name + " performs Action 4.");
        }

        // Action Slot 5
        public virtual void Action5()
        {
            // 默认的 Action5 逻辑
            Debug.Log(Name + " performs Action 5.");
        }
    //
}
