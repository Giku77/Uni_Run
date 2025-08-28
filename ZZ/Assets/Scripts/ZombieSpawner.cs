using UnityEngine;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab;

    //public ItemManager itemManager;

    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints;

    private List<Zombie> activeZombies = new List<Zombie>();

    public UiManager uiManager;

    private int waveNumber;

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    private void SpawnWave()
    {
        waveNumber++;
        
        int count = Mathf.RoundToInt(waveNumber * 1.5f);
        for (int i = 0; i < count; i++)
        {
            CreateZombie();
        }
        uiManager.SetWaveInfo(waveNumber, activeZombies.Count);
        //waveNumber++;
    }

    public void CreateZombie()
    {
        var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var zombie = Instantiate(zombiePrefab, point.position, point.rotation);
        //var zombie = Instantiate(zombiePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        zombie.SetZombieData(zombieDatas[Random.Range(0, zombieDatas.Length)]);
        activeZombies.Add(zombie);
        zombie.OnDeath += () => activeZombies.Remove(zombie);
        zombie.OnDeath += () => uiManager.SetWaveInfo(waveNumber, activeZombies.Count);
        zombie.OnDeath += () => Destroy(zombie.gameObject, 5f);
    }

    private void Update()
    {
       if (activeZombies.Count == 0)
       {
            SpawnWave();
       }
    }
}
