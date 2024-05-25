using System.Collections;
using UnityEngine;

public class M_BaseMonster : MonoBehaviour
{
    [SerializeField] public M_MonsterConfiguration configuration;
    [SerializeField] public FactionId factionId = FactionId.NEUTRAL;

    [Header("State Manager")]
    [SerializeField] public CharacterStateId currentStateId = CharacterStateId.NONE;
    [SerializeField] public CharacterStateId initialStateId = CharacterStateId.NONE;

    [Header("Animator")]
    [SerializeField] public Animator modelAnimator;

    [Header("Combat")]
    [SerializeField] public M_AttackController attackController;
    [SerializeField] public M_BaseHealth healthController;

    private bool isUpdateActive = false;
    private bool isFixedUpdateActive = false;
        
    private void Start() {
        StartCoroutine(SwitchControlState(initialStateId));

        // reset all state in this object
        SetUpdateProcess(false);

        // initialize all
        healthController.Initialize();
    }

    public IEnumerator SwitchControlState(CharacterStateId nextStateId, bool isOverride = false) 
    {
        yield return null;

        if (nextStateId == currentStateId && !isOverride) { yield break; }

        onExitState();

        currentStateId = nextStateId;

        onEnterState();
    }

    private void Update()
    {
        if (!isUpdateActive) return;

        switch (currentStateId)
        {
            case CharacterStateId.IDLE:       
                attackController.CheckEnemy(factionId);
                break;
            case CharacterStateId.ATTACK:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if (!isFixedUpdateActive) return;

        switch (currentStateId)
        {
            case CharacterStateId.IDLE:
                break;
            case CharacterStateId.ATTACK:
                break;
            default:
                break;
        }
    }

    private void onEnterState()
    {
        switch (currentStateId)
        {
            case CharacterStateId.IDLE:
                PlayAnimation("RunFWD");

                // turn on/off update
                isUpdateActive = true;

                break;
            case CharacterStateId.ATTACK:
                break;
            default:
                break;
        }
    }

    private void onExitState() {
        switch (currentStateId) {
            case CharacterStateId.IDLE:

                // turn on/off update
                isUpdateActive = false;

                break;
            case CharacterStateId.ATTACK:
                break;
            default:
                break;
        }
    }

    private void SetUpdateProcess(bool isActivated) {
        isUpdateActive = isActivated;
        isFixedUpdateActive = isActivated;
    }

    public void PlayAnimation(string animationName) {
        modelAnimator.Play(animationName);
    }


    public MonsterId GetMonsterId() 
    {
        MonsterId monsterId = 0;

        if (configuration != null) monsterId = configuration.id;

        return monsterId;
    }
}
