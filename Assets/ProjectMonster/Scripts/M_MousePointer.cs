using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_MousePointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        M_InputManager.Instance.MouseHitGrid += OnMouseHitGrid;
    }

	private void OnMouseHitGrid(Vector3 vector)
	{
		transform.position = vector;
	}
}
