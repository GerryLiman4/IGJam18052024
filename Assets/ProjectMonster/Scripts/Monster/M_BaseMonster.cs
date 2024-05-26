using System;
using System.Collections;
using UnityEngine;

public class M_BaseMonster : MonoBehaviour
{
    [SerializeField] public M_MonsterConfiguration configuration;
    [SerializeField] public FactionId factionId = FactionId.NEUTRAL;

    [Header("State Manager")]
    [SerializeField] public CharacterStateId currentStateId = CharacterStateId.NONE;
    [SerializeField] public CharacterStateId initialStateId = CharacterStateId.NONE;

    public M_CharacterMoveStateId movementState = M_CharacterMoveStateId.Idle;

    [Header("Animator")]
    [SerializeField] public Animator modelAnimator;

    [Header("Combat")]
    [SerializeField] public M_AttackController attackController;
    [SerializeField] public M_BaseHealth healthController;

    private bool isUpdateActive = false;
    private bool isFixedUpdateActive = false;

    private event Action<string> animationFinished;
    private void Start() {
        StartCoroutine(SwitchControlState(initialStateId));

        // reset all state in this object
        SetUpdateProcess(false);

        // initialize all
        healthController.Initialize(factionId);
        attackController.Initialize(factionId);
    }

    public void AsyncSwitchControlState(CharacterStateId nextStateId, bool isOverride = false) {
        StartCoroutine(SwitchControlState(nextStateId, isOverride));
    }

    public MonsterId GetMonsterId()
    {
        MonsterId monsterId = 0;

        if (configuration != null) monsterId = configuration.id;

        return monsterId;
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
                M_IDamagable target = attackController.CheckTarget(factionId);
                // if there is target then switch to attack
                if (target != null) StartCoroutine(SwitchControlState(CharacterStateId.ATTACK));
                break;
            case CharacterStateId.ATTACK:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
		//switch characterMoveStateId
		if (movementState == M_CharacterMoveStateId.Moving)
		{
			transform.position += transform.forward * configuration.information.moveSpeed * Time.fixedDeltaTime;
		}

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
                PlayAnimation(AnimationId.Idle.ToString());

                // turn on/off update
                isUpdateActive = true;

				if (configuration.information.isMoveable)
				{
					ChangeMovementState(M_CharacterMoveStateId.Moving);
				}
				break;
            case CharacterStateId.ATTACK:
                animationFinished += onAttackAnimationFinished;
                PlayAnimation(AnimationId.Attack01.ToString());
                ChangeMovementState(M_CharacterMoveStateId.Idle);
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
                animationFinished -= onAttackAnimationFinished;
                break;
            default:
                break;
        }
    }

    private void SetUpdateProcess(bool isActivated) {
        isUpdateActive = isActivated;
        isFixedUpdateActive = isActivated;
    }

    // animation
    public void PlayAnimation(string animationName) {
        modelAnimator.Play(animationName);
    }

    public void onAnimationFinished(string animationName) {
        animationFinished?.Invoke(animationName);
    }

    private void onAttackAnimationFinished(string animationName)
    {
        StartCoroutine(SwitchControlState(CharacterStateId.IDLE));
    }

    public void ExecuteAttack() {
        if (!configuration.information.isMelee) { attackController.SpawnProjectile(); }
    private void ChangeMovementState(M_CharacterMoveStateId state)
    {
        movementState = state;
    }
}
