using System;
using System.Collections.Generic;
using UnityEngine;

public class M_GameManager : MonoBehaviour
{

    [SerializeField]
    public M_GridMap gridMap;

	[SerializeField]
	public float preparationTime = 5f;

	[Header("Managers")]
	[SerializeField] private M_SlotControllerUI slotManager;
	[SerializeField] private List<M_MonsterConfiguration> availableMonsters = new List<M_MonsterConfiguration>();
	[SerializeField] private M_BaseHQController playerHQ;

	[Header("UI")]
	[SerializeField] private M_NotificationUI notificationUI;
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
		// initialize every manager
		slotManager.Initialize(availableMonsters);
		slotManager.selected += onSelectedCard;

		playerHQ.Initialize();

		SignalManager.MouseClickOnGrid += OnSelectGridTile;
		SignalManager.BaseDestroyed += OnBaseDestroyed;
		M_EnemyMonsterSpawner.Instance.WaveEnded += OnWaveEnded;
		playerHQ.UpdateFoodResourceAmount += OnUpdateFoodResource;
	}

    private void OnUpdateFoodResource(float total)
    {
        // should update ui
    }

    private void OnBaseDestroyed(FactionId factionId)
    {
		ChangeGameState(M_GameStateId.GAMEOVER);
		SignalManager.OnGameOver(factionId == FactionId.PLAYER);
	}

    private void OnSelectGridTile(GameObject gridTile)
    {
		if (slotManager.currentSelectedCard == null) return;

		M_GridTile selectedGridTile = gridTile.GetComponentInParent<M_GridTile>();

		if (selectedGridTile == null) return;

		foreach (KeyValuePair<Vector2Int, M_GridTile> loopedGridTile in gridMap.gridTileMap)
        {
			if (loopedGridTile.Value == selectedGridTile && loopedGridTile.Value.occupyingObject == null)
            {
				M_BaseMonster instantiatedAlly = gridMap.SpawnAlly(slotManager.currentSelectedCard.configuration.monsterModel.gameObject, loopedGridTile.Key).GetComponent<M_BaseMonster>();
				instantiatedAlly.isPlanted = true;
				instantiatedAlly.Initialize(FactionId.PLAYER);
				
				// instantiate monster model
				loopedGridTile.Value.OccupyTile(instantiatedAlly.gameObject);
				break;
            }

		}
	}

    //private M_BaseMonster SpawnMonsterInTile(M_MonsterConfiguration config, GameObject root)
    //   {
    //	M_BaseMonster instantiatedObject = Instantiate<M_BaseMonster>(config.monsterModel, root.transform.position,Quaternion.identity,root.transform);

    //	return instantiatedObject;
    //}


    private void onSelectedCard(M_CardSlotUI selectedCard)
    {
		print("Here");
		if (playerHQ == null || playerHQ.currentFoodResource < (float)selectedCard.configuration.information.deployCost)
        {
			StartCoroutine(notificationUI.Initialize("NOT ENOUGH RESOURCE", 1.5f));

			return;
		}
        print("Selected " + selectedCard.configuration.information.name);

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
