using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_WaveProgressionMonster
{
    [SerializeField]
    public int waveTargetProgression = 0;

    [SerializeField]
    public List<MonsterId> monsterIds = new List<MonsterId>();
}
