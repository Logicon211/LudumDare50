using UnityEngine;

public abstract class Powerup : MonoBehaviour {
    [SerializeField] protected string[] levelDescriptions;
    [SerializeField] protected int level;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float cooldown;

    float currentCooldown = 0f;

    void Start() {

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
}
