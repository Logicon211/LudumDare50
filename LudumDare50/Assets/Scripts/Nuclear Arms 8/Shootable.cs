using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public void Damage()
    {
        gameObject.GetComponent<IDamageable<float>>().Damage(1);
    }
}
