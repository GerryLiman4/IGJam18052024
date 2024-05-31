
using System;
using System.Collections.Generic;
using UnityEngine;

public class M_BaseHQController : MonoBehaviour
{
    [SerializeField] public FactionId factionId;
    [SerializeField] public M_BaseHealth hqHealth;

    [Header("Base Info")]
    [SerializeField] public float currentFoodResource = 0;
    [SerializeField] public float foodProducedMultiplier = 1.5f;
    [SerializeField] public Dictionary<int, float> upgradeProduceMultiplier = new Dictionary<int,float>();

    public int currentUpgradeIndex = 0;

    public event Action<float> UpdateFoodResourceAmount;
    public void Initialize()
    {
        gameObject.SetActive(true);

        // initialize all
        hqHealth.Initialize(factionId);
        hqHealth.died += OnBaseDestroyed;

        SetFoodProducedMultiplier(currentUpgradeIndex);
    }

    private void FixedUpdate()
    {
        currentFoodResource += Time.deltaTime * foodProducedMultiplier;
        UpdateFoodResourceAmount.Invoke(currentFoodResource);
    }

    private void SetFoodProducedMultiplier(int index)
    {
        currentUpgradeIndex = index;
        if (upgradeProduceMultiplier.ContainsKey(currentUpgradeIndex))
        {
            foodProducedMultiplier = upgradeProduceMultiplier[currentUpgradeIndex];
        }
    }

    private void OnBaseDestroyed()
    {
        SignalManager.OnBaseDestroyed(factionId);
        hqHealth.died -= OnBaseDestroyed;
        gameObject.SetActive(false);
    }
}
