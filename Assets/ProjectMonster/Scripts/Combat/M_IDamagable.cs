
using System;
using UnityEngine;

public interface M_IDamagable
{
    public event Action<int> damaged;
    public event Action died;
    public void GetDamaged(int damage, Vector3 direction);
    public FactionId GetFactionId();

}
