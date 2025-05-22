using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using System;
using UnityEditor.Sprites;
using System.Threading.Tasks;
using TMPro;

public class GM_Global : MonoBehaviour
{
    //Some Useful Global Variables
    public int Turn = 0; //the turn counts
    public int MaxDraws = 3; //the maximum of Draws
    public int Draws = 3;   //the Draws remain

    //GM and its refers
    //Important Refers
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Level GM_Level;

    public TMP_Text Hint_Text;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        Hint_Text = GameObject.Find(nameof(Hint_Text)).GetComponent<TMP_Text>();

        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");
    }

    private void Update()
    {
        switch (CurrentPlayerState)
        {
            case PlayerState.Idle:

                Hint_Text.text = ("Please Wait");
                break;

            case PlayerState.PlayingCard:

                Hint_Text.text = ("You may play a card or click endturn");
                break;

            case PlayerState.ChoosingCardOptional:

                Hint_Text.text = ("Please Choose a card, you may click X button to skip");
                break;

            case PlayerState.ChoosingCard:
                Hint_Text.text = ("Please Choose a card, can not skip");

                break;

            case PlayerState.ChoosingEnemy:
                Hint_Text.text = ("Please Choose a Enemy, you may click X button to skip");

                // 高亮敌人，等待点击
                break;

        }

    }

    //End Turn, This method 
    public async Task EndTurn()
    {
        // When EndTurn -------------------------------------------
        this.CurrentPlayerState = PlayerState.Idle;

        // 1, Check Venting 
        await GM_Card.Venting();

        // 2, EndTurn Trigger effects (To be Continue)

        // 3, Settle the buff/debuff effects 1 by 1 (To be Continue)

        // 4, All enemy reset armor type to default type, then enemy perform their actions 1 by 1, from far to close
        GM_Creature.UpdateCreature();   
        GM_Creature.AllResetArmorType(false);
        GM_Creature.AllDoAction();

        // 5, The GM_Level to judge and activate level events 敌人在此处被刷新出
        GM_Level.SettleEvents();
        await Task.Yield(); // 等待下一帧

        this.Turn++;

        // When NewTurn Begin -------------------------------------
        // 1, [Draw] cards from the Deck to fill up the player [Hand]
        while (GM_Card.ReturnHandCard().Count < GM_Card.HandSize)
        {
            await GM_Card.Draw(1, true);
        }

        // 2. Fill up the [Draws] of player, and the player reset armor type to default type.
        GM_Creature.AllResetArmorType(true);
        Draws = MaxDraws;

        // 3. The enemy choose and show their action for this turn 让敌人选择行为
        GM_Creature.UpdateCreature();
        await Task.Yield(); // 等待下一帧
        GM_Creature.AllPickAction();

        // 4. Active 'at the begin of a turn' effects if there is any, 1 by 1 (To be Continue)

        // After the new turn setup, set state back to PlayingCard
        this.CurrentPlayerState = PlayerState.PlayingCard;
    }




    // the player control logic here ----------------------------------------------------------------------------------------------------------------

    // Current player state
    public PlayerState CurrentPlayerState = PlayerState.PlayingCard;

    // the Game state
    public enum PlayerState
    {
        Idle,            // 眼看手不动状态
        PlayingCard,  // 可以拖拽卡牌来打出状态
        ChoosingCardOptional,  // 要求玩家选择一张卡牌状态,可选拒绝
        ChoosingCard,  //要求玩家选择一张卡牌状态,不可以拒绝
        ChoosingEnemy  // 要求玩家选择一个敌人状态
    }


    // The Targeting Box
    public GameObject Target = null;


    // What will happen when the player click something

    // 点击检测方法



}
