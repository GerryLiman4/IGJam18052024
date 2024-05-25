using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_GridMap : MonoBehaviour
{

	[SerializeField]
	private Vector2Int gridSize;

	[SerializeField]
	private Vector2 cellSize;

	public Dictionary<Vector2Int, M_Grid> gridMap;

	private void Start()
	{
		gridMap = new Dictionary<Vector2Int, M_Grid>();
		for (int i = 0; i < gridSize.x; i++)
		{
			for (int j = 0; j < gridSize.y; j++)
			{
				gridMap.Add(new Vector2Int(i, j), new M_Grid(new Vector2Int(i, j)));
			}
		}
	}

	public Vector3 GetGridWorldPosition(Vector2Int gridPos)
	{
		Vector3 startPos = transform.position;
		return startPos + new Vector3(gridPos.x * cellSize.x, 0, gridPos.x * cellSize.y);
	}

	public M_Grid GetGrid(Vector2Int gridPos)
	{
		return gridMap[gridPos];
	}

	public void SetGridObject(Vector2Int gridPos, GameObject gridObject)
	{
		GetGrid(gridPos).gridObject = gridObject;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;

		Vector3 startPos = transform.position;
		for (int i = 0; i < gridSize.x; i++)
		{
			for (int j = 0; j < gridSize.y; j++)
			{
				Vector3 gridPos = startPos + new Vector3(i * cellSize.x, 0, j * cellSize.y);
				Gizmos.DrawLine(new Vector3(gridPos.x - cellSize.x / 2, 0, gridPos.z - cellSize.y / 2), new Vector3(gridPos.x + cellSize.x / 2, 0, gridPos.z - cellSize.y / 2));
				Gizmos.DrawLine(new Vector3(gridPos.x - cellSize.x / 2, 0, gridPos.z - cellSize.y / 2), new Vector3(gridPos.x - cellSize.x / 2, 0, gridPos.z + cellSize.y / 2));
				Gizmos.DrawLine(new Vector3(gridPos.x + cellSize.x / 2, 0, gridPos.z + cellSize.y / 2), new Vector3(gridPos.x - cellSize.x / 2, 0, gridPos.z + cellSize.y / 2));
				Gizmos.DrawLine(new Vector3(gridPos.x + cellSize.x / 2, 0, gridPos.z + cellSize.y / 2), new Vector3(gridPos.x + cellSize.x / 2, 0, gridPos.z - cellSize.y / 2));
			}
		}
	}
}
