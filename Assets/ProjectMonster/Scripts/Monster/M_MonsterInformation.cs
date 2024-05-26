using System;
using UnityEngine;

[Serializable]
public class M_MonsterInformation
{
    [SerializeField] public MonsterId id = MonsterId.NONE;
    [SerializeField] public string name = "";
    [SerializeField] public string description = "";
    [SerializeField] public int deployCost = 0;

    [SerializeField] public bool isMoveable = false;
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public bool isMelee = false;

    public M_MonsterInformation() { }

}
