using UnityEngine;
using UnityEngine.EventSystems;
using static GM_Global;
using System.Threading.Tasks; // 引入Task命名空间

public class EndTurn : MonoBehaviour, IPointerDownHandler
{
    // Important References
    public GM_Card GM_Card;
    public GM_Creature GM_Creature;
    public GM_Global GM_Global;
    public GM_Level GM_Level;

    void Start()
    {
        GM_Card = GameObject.Find(nameof(GM_Card)).GetComponent<GM_Card>();
        GM_Creature = GameObject.Find(nameof(GM_Creature)).GetComponent<GM_Creature>();
        GM_Global = GameObject.Find(nameof(GM_Global)).GetComponent<GM_Global>();
        GM_Level = GameObject.Find(nameof(GM_Level)).GetComponent<GM_Level>();

        // Optional: Error checking
        if (GM_Card == null) Debug.LogError("GM_Card not found or missing component.");
        if (GM_Creature == null) Debug.LogError("GM_Creature not found or missing component.");
        if (GM_Global == null) Debug.LogError("GM_Global not found or missing component.");
        if (GM_Level == null) Debug.LogError("GM_Level not found or missing component.");
    }

    // When the button is pressed
    public async void OnPointerDown(PointerEventData eventData)
    {
        if (GM_Global.CurrentPlayerState == PlayerState.PlayingCard)
        {
            // Execute the logic when button is pressed
            Debug.Log("EndTurn");

            // Call the asynchronous EndTurn method
            await GM_Global.EndTurn(); // Ensure the EndTurn method is called and awaited
        }
        else
        {
            Debug.Log("Wrong Player State");
        }

        // You can add additional logic after EndTurn completes here if necessary
    }
}
