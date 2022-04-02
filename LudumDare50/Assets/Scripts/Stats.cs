using System;
using UnityEngine;

  [Serializable]
  public struct Stats {
    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float cooldown;
    [SerializeField] public int projectiles;
    [SerializeField] public float projectileSpeed;
  }
