using Assets.Scripts.DamageFlash;
using Core;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float health = 10f;
    [SerializeField] protected float damageDealt = 5f;
    [SerializeField] protected int score = 10;
    private NavMeshAgent _agent;
    protected TraumaInducer _traumaInducer;
    protected Transform playerLocation;
    protected DamageFlash damageFlash;
    protected float currHealth;

    // Start is called before the first frame update
    protected virtual void OnEnable()
    {
        currHealth = health;
        damageFlash = GetComponent<DamageFlash>();
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

    public virtual void TakeDamage(float damage)
    {
        currHealth -= damage;
        damageFlash.Flash();

        if (!(currHealth <= 0)) return;

        GameManager.instance.AddScore(score);
        Death();
    }

    protected virtual void DealDamage()
    {
    }

    public virtual void Death()
    {
        PoolManager.instance.PushToPool(gameObject);
    }
}