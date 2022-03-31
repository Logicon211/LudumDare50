using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableItemBase : MonoBehaviour, IDamageable<float>, IKillable
{

    public float health;

    public AudioSource oneShotAudioSource;
    public AudioClip destroyAudio;
    public GameObject itemObject;

    float currentHealth;
    RandomStuffSpawner spawner;

    bool destroyed = false;

    public GameObject explosionEffect;

    void Start()
    {
        currentHealth = health;
    }

    void Update()
    {

    }

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0f) Kill();
    }

    public void Kill()
    {
        if (spawner != null)
        {
            spawner.UpdateList(gameObject);
        }
        oneShotAudioSource.PlayOneShot(destroyAudio);
        Transform explosionTransform = gameObject.transform;
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(itemObject);
        Destroy(gameObject, destroyAudio.length);

    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetSpawner(RandomStuffSpawner spawner)
    {
        this.spawner = spawner;
    }
}
