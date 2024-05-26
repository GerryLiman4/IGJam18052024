using UnityEngine;

public class M_BaseProjectile : MonoBehaviour
{
    [SerializeField] public Vector3 launchDirection = Vector3.zero;
    [SerializeField] public float speed = 0.15f;
    [SerializeField] public FactionId factionId;
    [SerializeField] public Collider collider;
    [SerializeField] public int damage = 15;

    // TODO nanti ini tambahin when off camera destroy
    public void Initialize(Vector3 launchDirection,FactionId factionId) 
    {
        this.launchDirection = launchDirection;
        this.factionId = factionId;
    }

    private void FixedUpdate()
    {
        Launch();
    }

    private void Launch() {
        transform.Translate(launchDirection * speed,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        M_IDamagable target = other.GetComponentInParent<M_IDamagable>();

        if (target != null && target.GetFactionId() != factionId)
        {
            target.GetDamaged(damage, transform.position);
            Destroy(gameObject);
        }
    }
}
