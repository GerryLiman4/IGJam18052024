using UnityEngine;

public class M_BaseHQController : MonoBehaviour
{
    [SerializeField] public FactionId factionId;
    [SerializeField] public M_BaseHealth hqHealth;

    public void Initialize()
    {
        gameObject.SetActive(true);

        // initialize all
        hqHealth.Initialize(factionId);
        hqHealth.died += OnBaseDestroyed;
    }

    private void OnBaseDestroyed()
    {
        SignalManager.OnBaseDestroyed(factionId);
        hqHealth.died -= OnBaseDestroyed;
        gameObject.SetActive(false);
    }
}
