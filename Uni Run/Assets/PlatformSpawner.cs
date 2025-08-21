using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameManager gameManager;
    //private GameObject[] platforms = new GameObject[100];
    public List<GameObject> t = new List<GameObject>();
    public int platformCount = 10;
    public float spawnRange = 3f;
    public float spawnHeight = 1f;
    public float spawnInterval = 0.5f;
    private float nextSpawnTime = 0f;

    private void Start()
    {
        SpawnPlatform();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void SpawnPlatform()
    {
        //if (platforms.Length == 0 || platformCount > 100) return;
        for(int i = 0; i < platformCount; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(0.5f, spawnRange), Random.Range(-spawnHeight, spawnHeight));
            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            platform.SetActive(false);
            t.Add(platform);
            //platforms[i] = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            //platforms[i].SetActive(false);
            //t.Add(platforms[i]);
        }
    }
    private void Update()
    {
        if (gameManager.IsGameOver) return;
        if (Time.time >= nextSpawnTime)
        {
            int randomIndex = Random.Range(0, t.Count);
            t[randomIndex].SetActive(true);
            if (t[randomIndex].transform.position.x < -20f)
              t[randomIndex].transform.position = new Vector2(Random.Range(-1, spawnRange), Random.Range(-spawnHeight, spawnHeight));
            t.RemoveAt(randomIndex);
            //if (platforms[randomIndex].activeInHierarchy) return;
            //platforms[randomIndex].SetActive(true);
            nextSpawnTime = Time.time + spawnInterval;
        }
    }


}
