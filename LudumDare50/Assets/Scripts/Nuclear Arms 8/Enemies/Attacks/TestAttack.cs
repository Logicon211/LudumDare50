using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour, IAttack<Animator, GameObject>
{

    public float cooldown = 5f;
    public float damage = 5f;
    public float attackRange = 5f;
    public void Attack(Animator animator, GameObject target)
    {
        Debug.Log("Testing Attack");
        if (animator != null)
        {
            animator.SetTrigger("attacking");
        }
        IDamageable<float> targetDamageable = target.GetComponent<IDamageable<float>>();
        if (targetDamageable != null) targetDamageable.Damage(damage);
    }

    public float GetCooldown()
    {
        return cooldown;
    }


    public float GetAttackRange()
    {
        return attackRange;
    }
}
