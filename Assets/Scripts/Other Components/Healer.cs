using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int healAmount;

    public int HealAmount => healAmount;
}
