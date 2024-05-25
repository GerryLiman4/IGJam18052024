using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_GridTile : MonoBehaviour
{
    [SerializeField] private GameObject normalPrefab;
    [SerializeField] private GameObject hoveredPrefab;

    public void SetHovered()
    {
        normalPrefab.SetActive(false);
        hoveredPrefab.SetActive(true);
    }

    public void SetNormal()
    {
        normalPrefab.SetActive(true);
        hoveredPrefab.SetActive(false);
    }
}
