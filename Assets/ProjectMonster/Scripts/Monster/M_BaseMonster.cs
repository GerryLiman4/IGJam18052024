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

        // connect all signal
        healthController.died += onDied;
        attackController.Hit += OnHit;
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

        M_IDamagable target = null;

        switch (currentStateId)
        {
            
            case CharacterStateId.IDLE:       
                target = attackController.CheckTarget(factionId);
                // if there is target then switch to attack
                if (target != null)
                {
                    StartCoroutine(SwitchControlState(CharacterStateId.ATTACK));
                }
                else
                {
                    // if there isn't target then switch to moving
                    StartCoroutine(SwitchControlState(CharacterStateId.MOVE));
                }
                break;
            case CharacterStateId.ATTACK:
                break;
            case CharacterStateId.MOVE:
                target = attackController.CheckTarget(factionId);

                if (target != null) StartCoroutine(SwitchControlState(CharacterStateId.ATTACK));
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
		//switch characterMoveStateId
		//if (movementState == M_CharacterMoveStateId.Moving)
		//{
		//	transform.position += transform.forward * configuration.information.moveSpeed * Time.fixedDeltaTime;
		//}

		if (!isFixedUpdateActive) return;

        switch (currentStateId)
        {
            case CharacterStateId.IDLE:
                break;
            case CharacterStateId.ATTACK:
                break;
            case CharacterStateId.MOVE:
                transform.position += transform.forward * configuration.information.moveSpeed * Time.fixedDeltaTime;
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
				break;
            case CharacterStateId.ATTACK:
                animationFinished += onAttackAnimationFinished;
                PlayAnimation(AnimationId.Attack01.ToString());
                break;
            case CharacterStateId.MOVE:
                PlayAnimation(AnimationId.Idle.ToString());

                isFixedUpdateActive = true;
                isUpdateActive = true;
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

                attackController.DisableHitbox();
                break;
            case CharacterStateId.MOVE:

                isFixedUpdateActive = false;
                isUpdateActive = false;

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

    public void ExecuteAttack()
    {
        print("execute 1 , ");
        if (!configuration.information.isMelee) { 
            attackController.SpawnProjectile(); 
            return;
        }
        else
        {
            print("execute 2 , ");
            attackController.ActivateHitbox();
        }
    }
    private void OnHit(M_IDamagable target)
    {
        print("Masuk 3 , " + target.GetFactionId());
        target.GetDamaged(configuration.information.damage, transform.position);
    }

    public void CancelAttack()
    {
        attackController.DisableHitbox();
    }

    private void onDied()
    {
        Destroy(gameObject);
    }
}
