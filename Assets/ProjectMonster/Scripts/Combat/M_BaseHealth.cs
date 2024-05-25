using System;
using UnityEngine;

public class M_BaseHealth : MonoBehaviour, M_IDamagable
{
    [SerializeField] public int maxHp = 100;
    [SerializeField] public Collider collider;

    public int currentHp;

    public event Action<int> damaged;
    public event Action died;

    public void GetDamaged(int damage)
    {
        currentHp -= damage;
        damaged?.Invoke(damage);

        if (currentHp <= 0) 
        {
            collider.enabled = false;
            died?.Invoke();
        }
    }

    public void Initialize()
    {
        collider.enabled = true;
        currentHp = maxHp;
    }
}
