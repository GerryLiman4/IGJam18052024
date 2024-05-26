using UnityEngine;

public class M_EnemyMonsterSpawner : MonoBehaviour
{
	[SerializeField]
	public M_EnemySpawnerConfiguration defaultConfig;

	private M_EnemySpawnerConfiguration currentConfig;

	private M_EnemyWave currentWave;

	private int currentWaveIndex = 0;

	private float timer = 0f;

	private float waveTargetEndTime = 0f;

	private float currentWaveProgression = 0f;

	private bool currentWaveMaxProgression = false;

	private int currentMonster = 0;

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
	}

	private void SpawnMonster()
	{
		MonsterId t = currentWave.GetMonster(currentWaveProgression, currentMonster);
		if (t != MonsterId.NONE)
		{
			Debug.Log(t);
			currentMonster += 1;
		}

	}
}
