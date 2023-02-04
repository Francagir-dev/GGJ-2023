using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    public float rateMult = 1f;
    public GameObject parentSpawn;
    public Transform corner1;
    public Transform corner2;

    public List<GameObject> roots;

    float spawnRate = 1;
    public bool stopSpawn = false;

    private void Update()
    {
        UpdateSpawnRate(Time.time);
        if (!stopSpawn)
        {
            SpawnRoot();
        }
    }

    private void SpawnRoot()
    {
        Vector3 spawnPoint = GetRandomPosition();

        if (spawnRate > Random.Range(0, 100))
        {
            Instantiate(roots[Random.Range(0, roots.Count)], spawnPoint, Quaternion.identity, parentSpawn.transform);
        }
    }

    public void UpdateSpawnRate(float timePassed)
    {
        spawnRate = timePassed * rateMult;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(corner1.position.x, corner2.position.x), 
            Random.Range(corner1.position.y, corner2.position.y), 
            Random.Range(corner1.position.z, corner2.position.z)
            );
    }
}
