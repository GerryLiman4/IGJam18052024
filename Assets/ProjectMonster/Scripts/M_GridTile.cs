using UnityEngine;

public class M_GridTile : MonoBehaviour
{
    [SerializeField] private GameObject normalPrefab;
    [SerializeField] private GameObject hoveredPrefab;
    [SerializeField] private GameObject redHoveredPrefab;

    [SerializeField] public GameObject occupyingObject;
    [SerializeField] public GameObject unitRoot;
    public void SetHovered()
    {
        normalPrefab.SetActive(false);
        hoveredPrefab.SetActive(occupyingObject == null ? true : false);
        redHoveredPrefab.SetActive(occupyingObject == null ? false : true);
    }

    public void SetNormal()
    {
        normalPrefab.SetActive(true);
        hoveredPrefab.SetActive(false);
        redHoveredPrefab.SetActive(false);
    }

    public void OccupyTile(GameObject obj)
    {
        occupyingObject = obj;
    }

    public void RemoveOccupyingObject()
    {
        Destroy(occupyingObject);
        occupyingObject = null;
    }
}
