
using System;

public interface M_IDamagable
{
    public event Action<int> damaged;
    public event Action died;
    public void GetDamaged(int damage);
    public FactionId GetFactionId();

}
