using UnityEngine;

public class M_BaseMonster : MonoBehaviour
{
    [SerializeField] public M_MonsterConfiguration configuration;
    [SerializeField] public FactionId factionId = FactionId.NEUTRAL;

    [Header("State Manager")]
    [SerializeField] public CharacterStateId currentStateId = CharacterStateId.NONE;

    [Header("Animator")]
    [SerializeField] public Animator modelAnimator;

    public void SwitchControlState(CharacterStateId nextStateId, bool isOverride = false) 
    {
        if (nextStateId == currentStateId && !isOverride) { return; }

        onExitState();

        currentStateId = nextStateId;

        onEnterState();
    }

    private void onEnterState()
    {
        switch (currentStateId)
        {
            default:
                break;
        }
    }

    private void onExitState() {
        switch (currentStateId) {
            default:
                break;
        }
    }

    public MonsterId GetMonsterId() 
    {
        MonsterId monsterId = 0;

        if (configuration != null) monsterId = configuration.id;

        return monsterId;
    }
}
