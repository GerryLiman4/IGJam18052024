using System;
using System.Collections.Generic;
using UnityEngine;

public class M_EnemyMonsterSpawner : MonoBehaviour
{
	[SerializeField]
	public M_EnemySpawnerConfiguration defaultConfig;

	private M_EnemySpawnerConfiguration currentConfig;

	[SerializeField]
	List<M_MonsterIdWithMonsterObject> monsterIdReference;

	private M_EnemyWave currentWave;

	private int currentWaveIndex = 0;

	private float timer = 0f;

	private float waveTargetEndTime = 0f;

	private float currentWaveProgression = 0f;

	private bool currentWaveMaxProgression = false;

	private int currentMonster = 0;

	private int currentWaveDiedMonster = 0;

	public event Action WaveEnded;

	public static M_EnemyMonsterSpawner Instance;

	private void Awake()
	{
		Instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		M_GameManager.Instance.GameStateChanged += OnGameStateChanged;
		currentConfig = defaultConfig;
	}

	// Update is called once per frame
	void Update()
	{
		if (M_GameManager.Instance.GameState != M_GameStateId.WAVEPROGRESS && M_GameManager.Instance.GameState != M_GameStateId.WAVEMAXPROGRESS) return;
		if (currentWaveMaxProgression) return;

		timer += Time.deltaTime;
		UpdateWave();
	}

	private void OnGameStateChanged(M_GameStateId id)
	{
		if (id == M_GameStateId.WAVEPROGRESS)
		{
			StartWave();
		}
	}

	private void StartWave()
	{
		currentWave = currentConfig.enemyWaves[currentWaveIndex];
		waveTargetEndTime = timer + currentWave.TotalWaveTime;
	}

	private void UpdateWave()
	{
		currentWaveProgression = (timer - (currentWave.TotalWaveTime - waveTargetEndTime)) / currentWave.TotalWaveTime * 100;
		//Debug.Log(currentWaveProgression);

		if (currentWaveProgression >= 100)
		{
			currentWaveMaxProgression = true;
		}

		SpawnMonster();
		CheckCurrentWaveMonsterDead();
	}

	private void CheckCurrentWaveMonsterDead()
	{
		if (currentWaveDiedMonster >= currentWave.waveProgressionMonsterList.Count)
		{
			currentWaveIndex += 1;
			currentWave = null;

			timer = 0f;
			waveTargetEndTime = 0f;
			currentWaveProgression = 0f;
			currentWaveMaxProgression = false;
			currentMonster = 0;
			currentWaveDiedMonster = 0;
		}

		
	}

	private void SpawnMonster()
	{
		if (currentMonster > currentWave.waveProgressionMonsterList.Count - 1) return;

		MonsterId t = currentWave.GetMonster(currentWaveProgression, currentMonster);
		if (t != MonsterId.NONE)
		{
			GameObject spawnedMonster = null;
			for (int i = 0; i < monsterIdReference.Count; i++)
			{
				if (monsterIdReference[i].monsterId == t)
				{
					spawnedMonster = monsterIdReference[i].monsterObj;
					break;
				}
			}

			M_BaseMonster monster = spawnedMonster.GetComponent<M_BaseMonster>();

			monster.healthController.died += OnMonsterDied;

			int x = M_GridMap.Instance.GridSize.x - 1;
			int y = UnityEngine.Random.Range(0, M_GridMap.Instance.GridSize.y);

			M_BaseMonster spawnEnemy = M_GridMap.Instance.SpawnEnemy(spawnedMonster, new Vector2Int(x, y)).GetComponent<M_BaseMonster>();
			spawnEnemy.Initialize(FactionId.ENEMY);
			currentMonster += 1;
		}

	}

	private void OnMonsterDied()
	{
		currentWaveDiedMonster += 1;
	}
}
