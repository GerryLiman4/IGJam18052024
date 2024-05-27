using System;
using UnityEngine;

public static class SignalManager
{
    public static event Action<GameObject> MouseClickOnGrid;

    public static void OnMouseClickOnGrid(GameObject gridTile)
    {
        MouseClickOnGrid?.Invoke(gridTile);
    }
}
