using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack<A, T>
{
    void Attack(A animator, T targetObject);

    float GetCooldown();

    float GetAttackRange();
}