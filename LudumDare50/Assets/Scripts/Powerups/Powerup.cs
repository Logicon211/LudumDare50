using UnityEngine;
using Enemy.Interface;

public abstract class Powerup : MonoBehaviour {

    [SerializeField] protected string[] levelDescriptions;
    [SerializeField] protected Stats[] levelStats;
    public int Level;
    [SerializeField] protected int level {
        get {
            return Level;
        }
        set {
            Level = value;
            SetStats(Level);
        }
    }
    [SerializeField] public Stats currentStats;
    [SerializeField] protected Sprite powerupIcon;

    [SerializeField] protected string powerupName = "";

    public GameObject gameArea;

    public bool active = false;
    float currentCooldown = 0f;
    protected GameObject playerObject;
    protected Player player;

    protected GameManager gameManager;

    private int maxLevel;
    protected virtual void Start() {
        gameArea = GameObject.FindGameObjectWithTag("ProjectileArea");
        level = 0;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        currentCooldown = currentStats.cooldown;
        maxLevel = levelStats.GetUpperBound(0) + 1;
        // Get player object/manager script
    }

    void FixedUpdate() {
        if (active && CanPerformAction()) {
            UsePowerup();
        }
    }

    public void SetPowerupActive() {
        active = true;
    }

    public void SetPowerupInActive() {
        active = false;
    }

    // Should only need to override this function
    public virtual void UsePowerup() {}

    public int GetLevel() {
        return level;
    }

    public void SetLevel(int newLevel) {
        level = newLevel;
    }

    public virtual void LevelUp(int numberOfLevels) {
        // if (level >= levelStats.Length) {
        //     Debug.Log("Cannot level up anymore, already at max level");
        //     return;
        // }

        // We can go above max now
        level += numberOfLevels;
    }

    public virtual void SetStats(int newLevel) {
        if (levelStats.Length < newLevel) {
            Debug.Log("We're overleveled here");
            return;
        }
        if (levelStats.Length <= level) {
            newLevel = levelStats.GetUpperBound(0);
        }
        currentStats.damage = levelStats[newLevel].damage;
        currentStats.speed = levelStats[newLevel].speed;
        currentStats.cooldown = levelStats[newLevel].cooldown;
        currentStats.projectiles = levelStats[newLevel].projectiles;
        currentStats.projectileSpeed = levelStats[newLevel].projectileSpeed;
    }

    public string GetPowerupLevelDescription(int level) {
        if (levelDescriptions.Length == 0) {
            return "No description";
        }
        if (levelDescriptions.Length < level) {
            return levelDescriptions[levelDescriptions.GetUpperBound(0)];
        }
        return levelDescriptions[level - 1];
    }

    bool CanPerformAction() {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0) {
            currentCooldown = currentStats.cooldown/player.playerStats.cooldownPercentBonus;
            return true;
        }
        return false;
    }

    // Probably will work, just a generic entry point that we might be able to hit to calculate damage
    // If we can get a generic summary of the players current bonuses, it'll make calculating damage faster
    public void DoDamage(GameObject target, float damage) {
        IDamageable<float> targetDamage = target.GetComponent<IDamageable<float>>();

        // Get the players total bonus

        // Add total bonus to damage
        if (targetDamage != null) {
            Debug.Log(
                damage
            );
            targetDamage.Damage(damage);
        }
    }
    public void DoDamage(GameObject target) {
        DoDamage(target, currentStats.damage);
    }

    public string getPowerupName() {
        return powerupName;
    }

    public Sprite getPowerupIcon() {
        return powerupIcon;
    }

    public GameManager getGameManager() {
        return gameManager;
    }

    // GET DAMAGE BASED ON MAX LEVEL
    public float GetDamage() {
        if(level > maxLevel) {
            int overlevel = level - maxLevel;
            return (currentStats.damage + overlevel) * player.playerStats.damagePercentBonus;
        } else {
            return currentStats.damage * player.playerStats.damagePercentBonus;
        }
    }
}
