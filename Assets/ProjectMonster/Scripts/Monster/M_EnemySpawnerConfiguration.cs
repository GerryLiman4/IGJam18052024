using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Configuration", menuName = "Wave Configuration/Wave", order = 1)]
public class M_EnemySpawnerConfiguration : ScriptableObject
{
    [SerializeField]
    public List<M_EnemyWave> enemyWaves = new List<M_EnemyWave>();

}
