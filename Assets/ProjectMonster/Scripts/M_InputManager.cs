using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class M_InputManager : MonoBehaviour
{

	[SerializeField]
	private Camera mainCamera;

	[SerializeField]
	private LayerMask gridLayerMask;

	private bool HitGrid = false;

	public event Action<Vector3> MouseHitGrid;
	public event Action MouseDoesntHitGrid;

	public static M_InputManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, gridLayerMask))
		{
			MouseHitGrid?.Invoke(hitInfo.point);
			HitGrid = true;

			Mouse mouse = Mouse.current;
			if (mouse.leftButton.wasPressedThisFrame)
			{
				SignalManager.OnMouseClickOnGrid(hitInfo.collider.gameObject);
			}
		}
		else if (HitGrid)
		{
			MouseDoesntHitGrid?.Invoke();
			HitGrid = false;
		}

		
	}
}
