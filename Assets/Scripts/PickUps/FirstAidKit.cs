
using UnityEngine;

public class FirstAidKit : PickUpItem
{
    [SerializeField] [Min(1)] private int healAmount;

    protected override void ApplyEffect()
    {
        var player = FindObjectOfType<Player>();
        player.Heal(healAmount);
    }
}
