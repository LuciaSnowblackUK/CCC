using TMPro;
using UnityEngine;
using static GM_Global;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skip_Button : MonoBehaviour
{
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;

    private Button button;
    private Image image;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        button = GetComponent<Button>();
        image = GetComponent<Image>();

        // 可选：报错检查
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");

    }

    private void Awake()
    {
        // 获取按钮组件并添加点击事件监听
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        bool shouldBeVisible = GM_Global.CurrentPlayerState == PlayerState.ChoosingEnemy ||
                               GM_Global.CurrentPlayerState == PlayerState.ChoosingCardOptional;

        if (button != null) button.interactable = shouldBeVisible;
        if (image != null) image.enabled = shouldBeVisible;

    }


    public void OnButtonClick()
    {
        if (GM_Global.CurrentPlayerState == PlayerState.ChoosingEnemy || GM_Global.CurrentPlayerState == PlayerState.ChoosingCardOptional)
        {
            // 在这里填写你想执行的逻辑
            Debug.Log("This Selection Skiped");
            GM_Global.Target = this.gameObject;
        }
        else
        {
            Debug.Log("Wrong Player State");
        }


        // {留白}：在这里添加你需要执行的具体操作
    }


}
