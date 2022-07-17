using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    protected Transform playerLocation;
    private NavMeshAgent _agent;
    protected TraumaInducer _traumaInducer;
    [SerializeField] protected float health = 10f;
    [SerializeField] protected float damageDealt = 5f;
    [SerializeField] protected float score = 10f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _traumaInducer = GetComponent<TraumaInducer>();
        playerLocation = GameManager.instance.Player.transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _agent.SetDestination(playerLocation.transform.position);
        DealDamage();
    }

    protected virtual void DealDamage()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (!(health <= 0)) return;
        GameManager.instance.AddScore(score);
        Death();
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
