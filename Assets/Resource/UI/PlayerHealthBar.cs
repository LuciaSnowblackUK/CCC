using TMPro;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public Creature Player;
    public TMP_Text Text;

    private void Start()
    {
        Player = GameObject.Find(nameof(Player)).GetComponent<Creature>();
        Text = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        Text.text = $"{Player.HP}";
    }
}
