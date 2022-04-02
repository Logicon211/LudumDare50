using UnityEngine;
using Enemy.Interface;
using System;

public abstract class Powerup : MonoBehaviour {

    [Serializable]
    protected struct stats {
        [SerializeField] public float damage;
        [SerializeField] public float speed;
        [SerializeField] public float cooldown;
        [SerializeField] public int projectiles;
    }
    [SerializeField] protected string[] levelDescriptions;
    [SerializeField] protected stats[] levelStats;
    private int Level;
    [SerializeField] protected int level {
        get {
            return Level;
        }
        set {
            Level = value;
            SetStats(Level);
        }
    }
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float cooldown;
    [SerializeField] protected int projectiles;
    [SerializeField] protected stats currentStats;

    float currentCooldown = 0f;

    void Start() {
        level = 0;
    }

    void FixedUpdate() {
        if (CanPerformAction()) {
            UsePowerup();
        }
    }

    // Should only need to override this function
    public virtual void UsePowerup() {}

    public int GetLevel() {
        return level;
    }

    public void SetLevel(int newLevel) {
        level = newLevel;
    }

    public void LevelUp(int numberOfLevels) {
        level += numberOfLevels;
    }

    public void SetStats(int newLevel) {
        if (levelStats.Length < newLevel) {
            Debug.Log("No stats for that level");
            return;
        }
        currentStats.damage = levelStats[newLevel].damage;
        currentStats.speed = levelStats[newLevel].speed;
        currentStats.cooldown = levelStats[newLevel].cooldown;
        currentStats.projectiles = levelStats[newLevel].projectiles;
    }

    public string GetPowerupLevelDescription(int level) {
        if (levelDescriptions.Length == 0) {
            return "No description";
        }
        if (levelDescriptions.Length > level) {
            return levelDescriptions[levelDescriptions.GetUpperBound(0)];
        }
        return levelDescriptions[level - 1];
    }

    bool CanPerformAction() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= cooldown) {
            currentCooldown = 0f;
            return true;
        }
        return false;
    }

    // Probably will work, just a generic entry point that we might be able to hit to calculate damage
    protected void DoDamage(GameObject target) {
        IDamageable<float> targetDamage = target.GetComponent<IDamageable<float>>();

        // Get the players total bonus

        // Add total bonus to damage
        targetDamage.Damage(damage);
    }
}
