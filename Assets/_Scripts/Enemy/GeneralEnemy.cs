using UnityEngine;

public class GeneralEnemy : Enemy
{
    private float _attackCoolDown;
    protected float attackCoolDown = 2f;
    protected float attackRange = 2f;

    protected override void OnEnable()
    {
        base.OnEnable();
        _attackCoolDown = attackCoolDown;
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        _attackCoolDown -= Time.deltaTime;

        if (_attackCoolDown > 0) return;

        if (Vector3.Distance(playerLocation.transform.position, transform.position) < attackRange)
        {
            StartCoroutine(_traumaInducer.StartAnim());
            playerLocation.GetComponent<IDamageable>().TakeDamage(damageDealt);
            _attackCoolDown = attackCoolDown;
        }
    }
}