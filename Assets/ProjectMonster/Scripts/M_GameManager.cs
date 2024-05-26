using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_GameManager : MonoBehaviour
{

    [SerializeField]
    public M_GridMap gridMap;

	[SerializeField]
	public float preparationTime = 5f;

	public M_GameStateId GameState { get; private set; } = M_GameStateId.PREPARATION;

	private float time = 0f;

	public event Action<M_GameStateId> GameStateChanged;

	public static M_GameManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		M_EnemyMonsterSpawner.Instance.WaveEnded += OnWaveEnded;
	}

	private void Update()
	{
		time += Time.deltaTime;
		CheckPreparationTime();

		if (Input.GetKey(KeyCode.Z))
		{
			ChangeGameState(M_GameStateId.GAMEOVER);
		}

	}

	private void CheckPreparationTime()
	{
		if (time >= preparationTime && GameState == M_GameStateId.PREPARATION)
		{
			ChangeGameState(M_GameStateId.WAVEPROGRESS);
		}
	}

	private void ChangeGameState(M_GameStateId gameState)
	{
		GameState = gameState;
		GameStateChanged?.Invoke(gameState);
	}

	private void OnWaveEnded()
	{
		time = 0f;
		ChangeGameState(M_GameStateId.PREPARATION);
	}
}
