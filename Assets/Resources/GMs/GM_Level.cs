using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


// this is a template to create a level
public class GM_Level : MonoBehaviour
{
    public virtual int LevelID { get; set; }  = 001;
    public virtual int LevelProgress { get; set; } = 1; //at which sub level of this level


    //Important Refers
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();

        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
    }



    //OnGoing Process Method
    public virtual void SettleEvents()
    {
        
    }




    //Criteria --------------------------------------

    //Eliminate all EEE 
    public virtual bool CheckEliminate(int enemyID)
    {
        foreach (Transform child in GM_Creature.transform)
        {
            Creature target = child.GetComponent<Creature>();  // 从子物体获取 Creature 组件
            if (target != null && target.ID == enemyID)  // 直接检查 ID 是否匹配
            {

                return false;  // 找到目标后直接返回
            }
        }

        return true;  // 如果遍历完所有子物体都没有找到，返回 false
    }

    //Eliminate all enemy
    public virtual bool CheckEliminateAll()
    {
        foreach (Transform child in GM_Creature.transform)
        {
            Creature target = child.GetComponent<Creature>();  // 从子物体获取 Creature 组件
            if (target != null && target.IsEnemy == true)  // 直接检查 ID 是否匹配
            {

                return false;  // 找到目标后直接返回
            }
        }

        return true;  // 如果遍历完所有子物体都没有找到，返回 false
    }


    //N Turn to {Event}
    public virtual bool CheckTurnLeft(int Timer)
    {
        if (Timer <= 0)
        {
            return true;
        }
        return false;
    }

    //Play N of CCC card
    public virtual bool CheckPlayCount(int PlayCount)
    {
        if (PlayCount <= 0)
        {
            return true;
        }
        return false;
    }


}
