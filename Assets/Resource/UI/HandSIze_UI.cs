using UnityEngine;
using TMPro; // 确保引入 TextMeshPro 命名空间

[RequireComponent(typeof(TMP_Text))]
public class TempTextUpdater : MonoBehaviour
{
    //Important Refers
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;
    private TMP_Text tmpText;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        // 获取 TMP_Text 组件
        tmpText = GetComponent<TMP_Text>();


        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");

    }


    private void Update()
    {
        tmpText.text = "Hand: " + $"{GM_Card.ReturnHandCard().Count}/{GM_Card.HandSize}";

    }
}
