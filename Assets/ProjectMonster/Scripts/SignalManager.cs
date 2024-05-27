using System;
using UnityEngine;

public static class SignalManager
{
    public static event Action<GameObject> MouseClickOnGrid;
    public static event Action<FactionId> BaseDestroyed;

    public static void OnMouseClickOnGrid(GameObject gridTile)
    {
        MouseClickOnGrid?.Invoke(gridTile);
    }

    public static void OnBaseDestroyed(FactionId factionId)
    {
        BaseDestroyed?.Invoke(factionId);
    }
}
