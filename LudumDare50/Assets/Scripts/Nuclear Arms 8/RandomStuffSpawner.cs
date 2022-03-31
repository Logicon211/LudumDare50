using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStuffSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnTimer;
    public int maxAmountOfObjects;

    float currentTimer = 0f;
    List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        currentTimer = spawnTimer;
    }

    void FixedUpdate()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0f)
        {
            if (objects.Count <= maxAmountOfObjects) SpawnObject();
            currentTimer = spawnTimer;
        }
    }

    void SpawnObject()
    {
        GameObject newObject = Instantiate(objectToSpawn, transform.position, Quaternion.Euler(new Vector3(45, 0, 0)));
        objects.Add(newObject);
        DestroyableItemBase destroyableItemBase = newObject.GetComponent<DestroyableItemBase>();
        destroyableItemBase.SetSpawner(gameObject.GetComponent<RandomStuffSpawner>());
    }

    public void UpdateList(GameObject obj)
    {
        objects.Remove(obj);
    }
}
