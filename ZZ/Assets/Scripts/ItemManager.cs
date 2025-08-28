using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class ItemManager : MonoBehaviour
{
    public GameObject Coin;
    public GameObject HealthPack;
    public GameObject AmmoPack;


    private bool isSpawning = true;


    //private GameObject Player;

    //public float rotationSpeed = 60f;
    public float spawnInterval = 3f;
    //public float destroyTime = 3f;
    //public NavMeshAgent navMeshAgent;

    private void Start()
    {
        //Coin = GameObject.FindGameObjectWithTag("Coin");
        //HealthPack = GameObject.FindGameObjectWithTag("HealthPack");
        //AmmoPack = GameObject.FindGameObjectWithTag("Ammo");
        StartCoroutine(SpawnItem());
        //Player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnDisable()
    {
        isSpawning = false;
        //StopCoroutine(SpawnItem());
    }

    private IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (!isSpawning) yield break;
            var random = Random.Range(0, 3);
            switch (random)
            {
                case 0: SpawnItem("Coin", transform.position); break;
                case 1: SpawnItem("HealthPack", transform.position); break;
                case 2: SpawnItem("Ammo", transform.position); break;
            }
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    private bool RandomSpawn(Vector3 pos, float range, out Vector3 vpos)
    {
        Vector3 randomPos = pos + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            vpos = hit.position;
            return true;
        }
        vpos = Vector3.zero;
        return false;
    }

    public void SpawnItem(string itemTag, Vector3 pos, float range = 8f)
    {
        bool spawned = false;
        int attempts = 0;
        var vpos = Vector3.zero;
        while (!spawned && attempts < 10)
        {
            spawned = RandomSpawn(pos, range, out vpos);
            attempts++;
        }
        if (spawned)
        {
            if (itemTag == "Coin")
            {
                Instantiate(Coin, vpos, Quaternion.identity);
            }
            else if (itemTag == "HealthPack")
            {
                Instantiate(HealthPack, vpos, Quaternion.identity);
            }
            else if (itemTag == "Ammo")
            {
                Instantiate(AmmoPack, vpos, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("Failed to spawn item after multiple attempts.");
        }
    }

}
