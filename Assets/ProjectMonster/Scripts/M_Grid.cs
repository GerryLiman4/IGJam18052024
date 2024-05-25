using UnityEngine;

public class M_Grid
{

	public Vector2Int position { get; set; }

	public GameObject gridObject { get; set; }

	public M_Grid(Vector2Int inPosition, GameObject inGridObject = null)
	{
		position = inPosition;
		gridObject = inGridObject;
	}
}
