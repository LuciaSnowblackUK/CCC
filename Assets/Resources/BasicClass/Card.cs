using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    //public Variables
    public virtual string Name { get; set; } = "AAA";
    public virtual int ID { get; set; } = 1;
    public virtual List<string> Tag { get; set; } = new List<string>();

    //Important Refers
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;
    public TMP_Text Hint_Text;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        Hint_Text = GameObject.Find(nameof(Hint_Text)).GetComponent<TMP_Text>();


        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");

    }


    public virtual string CardDiscription { get; set; } = "-------------";






    // Core functions
    // 异步的 WhenDraw 方法
    public virtual async Task<bool> WhenDraw()
    {
        // 你可以在这里执行任何异步操作，例如延迟等
        await Task.Delay(500);  // 假设模拟等待500毫秒

        // 逻辑处理完成后返回
        return false;
    }

    // 异步的 WhenPlayed 方法
    public virtual async Task<bool> WhenPlayed()
    {
        // 你可以在这里执行任何异步操作，例如延迟等
        await Task.Delay(500);  // 假设模拟等待500毫秒

        // 逻辑处理完成后返回
        return false;
    }

    // 异步的 WhenShuffle 方法
    public virtual async Task<bool> WhenShuffle()
    {
        // 你可以在这里执行任何异步操作，例如延迟等
        await Task.Delay(500);  // 假设模拟等待500毫秒

        // 逻辑处理完成后返回
        return false;
    }

    // 异步的 WhenDiscard 方法
    public virtual async Task<bool> WhenDiscard()
    {
        // 你可以在这里执行任何异步操作，例如延迟等
        await Task.Delay(1000);  // 例如，模拟等待1秒

        // 逻辑处理完成后返回
        return false;
    }


}
