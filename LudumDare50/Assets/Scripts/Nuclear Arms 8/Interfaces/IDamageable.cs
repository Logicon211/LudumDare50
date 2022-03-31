using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable<T>
{
    void Damage(T damageTaken);
}
