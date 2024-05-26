using UnityEngine;

public class M_AttackController : MonoBehaviour
{
    [SerializeField] private float raycastRadius = 0.5f;
    [SerializeField] private float raycastRange = 50f;
    [SerializeField] private GameObject raycastRoot;

    [SerializeField] private LayerMask targetMask;

    [SerializeField] public FactionId factionId = FactionId.NEUTRAL;
    [SerializeField] public M_BaseProjectile projectilePrefab;
    [SerializeField] public Transform projectileSpawnRoot;

    public void SpawnProjectile() {
        if (projectilePrefab == null) { return; }

        Instantiate<M_BaseProjectile>(projectilePrefab, projectileSpawnRoot.transform.position,Quaternion.identity).Initialize(transform.forward, factionId); 
    }

    public void Initialize(FactionId factionId) {
        this.factionId = factionId;
    }

    public M_IDamagable CheckTarget(FactionId selfFactionId){
        RaycastHit[] hits = Physics.SphereCastAll(raycastRoot.transform.position, raycastRadius, transform.forward, raycastRange, targetMask) ;
        
        if (hits.Length > 0 ) {
            
            // looping all collider
            foreach (RaycastHit hitTarget in hits) {
                M_IDamagable target = hitTarget.collider.GetComponentInParent<M_IDamagable>();
                
                if (target != null && target.GetFactionId() != factionId)
                {
                    return target;
                }
            }
            
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(raycastRoot.transform.position, transform.forward * raycastRange, Color.red,2f,true);
    }
}
