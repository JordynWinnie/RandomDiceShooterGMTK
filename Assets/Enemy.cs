using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public Transform playerLocation;
    private NavMeshAgent _agent;
    [SerializeField] protected float health = 10f;
    [SerializeField] protected float damageDealt = 5f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _agent.SetDestination(playerLocation.position);

        
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
