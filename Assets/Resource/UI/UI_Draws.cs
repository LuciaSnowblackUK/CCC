using UnityEngine;
using TMPro;  // 引入 TextMeshPro 命名空间

public class UI_Draws : MonoBehaviour
{
    // Important Refers
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;

    public TMP_Text displayText; // 引用显示文本的 UI 组件

    void Start()
    {
        // 查找并获取相关组件
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");
    }

    void Update()
    {
        // 确保 GM_Global 和 displayText 都有效
        if (GM_Global != null && displayText != null)
        {
            // 格式化字符串为 "Draws / MaxDraws"
            displayText.text = $"{GM_Global.Draws}/{GM_Global.MaxDraws}";
        }
    }
}
