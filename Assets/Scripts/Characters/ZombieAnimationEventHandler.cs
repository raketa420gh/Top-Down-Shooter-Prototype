using System;
using UnityEngine;

public class ZombieAnimationEventHandler : MonoBehaviour
{
    public static event Action OnAttacked;
        
    public void Attack()
    {
        OnAttacked?.Invoke();
    }
}
