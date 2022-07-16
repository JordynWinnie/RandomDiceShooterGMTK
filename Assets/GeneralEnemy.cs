using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemy : Enemy
{
    protected float attackRange = 2f;
    protected float attackCoolDown = 2f;
    private float _attackCoolDown;

    protected override void Start()
    {
        base.Start();
        _attackCoolDown = attackCoolDown;
    }

    protected override void DealDamage()
    {
        base.DealDamage();
        _attackCoolDown -= Time.deltaTime;
       
        if (_attackCoolDown > 0) return;

        if (Vector3.Distance(playerLocation.transform.position, transform.position) < attackRange)
        {
            playerLocation.GetComponent<IDamageable>().TakeDamage(damageDealt);
            _attackCoolDown = attackCoolDown;
        }
    }
}
