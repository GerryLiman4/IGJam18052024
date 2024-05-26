using System;
using UnityEngine;

public class M_BaseHealth : MonoBehaviour, M_IDamagable
{
    [SerializeField] public int maxHp = 100;
    [SerializeField] public Collider collider;
    public FactionId factionId = FactionId.NEUTRAL;

    public int currentHp;

    public event Action<int> damaged;
    public event Action died;

    public void GetDamaged(int damage, Vector3 direction)
    {
        currentHp -= damage;
        damaged?.Invoke(damage);

        if (currentHp <= 0) 
        {
            collider.enabled = false;
            died?.Invoke();
        }
    }

    public FactionId GetFactionId()
    {
        return factionId;
    }

    public void Initialize(FactionId factionId)
    {
        this.factionId = factionId;
        collider.enabled = true;
        currentHp = maxHp;
    }
}
