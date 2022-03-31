using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable<float>
{
    private RoomManager roomManager;

    public float time;
    public float initialTime;

    public HealthBar healthbar;

    public AudioSource hurtAudioSource;
    public AudioClip hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        roomManager = GameObject.FindWithTag("RoomManager").GetComponent<RoomManager>();
        initialTime = roomManager.getInitialTime();
        time = roomManager.getInitialTime();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0) {
            // Trigger gameover
            Debug.Log("GAME OVER!");
            SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
        }

        healthbar.SetHealth(time/initialTime);
    }

    public float getTimeLeft() {
        return time;
    }

    public float getInitialTime() {
        return initialTime;
    }

    public void Damage(float damageTaken)
    {
        Debug.Log("PLAY HURT SOUND");
        hurtAudioSource.PlayOneShot(hurtSound);
        time -= damageTaken;
    }
}
