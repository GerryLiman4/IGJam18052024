using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_EnemyWave
{

	[SerializeField]
	private float totalWaveTime = 0f;
	public float TotalWaveTime { get { return totalWaveTime; } }

	[SerializeField]
	public List<M_WaveProgressionMonster> waveProgressionMonsterList;

	public MonsterId GetMonster(float waveProgression, int currentMonsterIdx)
	{
		for (int i = currentMonsterIdx; i < waveProgressionMonsterList.Count; i++)
		{
			if (waveProgression >= waveProgressionMonsterList[i].waveTargetProgression)
			{
				int random = Random.Range(0, waveProgressionMonsterList[i].monsterIds.Count);
				return waveProgressionMonsterList[i].monsterIds[random];
			}
		}
		return MonsterId.NONE;
	}
}
