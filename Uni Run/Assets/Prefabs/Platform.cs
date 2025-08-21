using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;

    private bool steppedOn = false;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        steppedOn = false;

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(Random.value < 0.3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !steppedOn)
        {
            Debug.Log("Player stepped on platform: " + gameObject.name);
            steppedOn = true;
            gameManager.AddScore(1);
            //foreach (GameObject obstacle in obstacles)
            //{
            //    obstacle.SetActive(true);
            //}
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Player left platform: " + gameObject.name);
    //        gameObject.SetActive(false);
    //    }
    //}

    private void Update()
    {
        if (transform.position.x < -20f)
        {
            gameObject.SetActive(false);
            PlatformSpawner spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PlatformSpawner>();
            spawner.t.Add(gameObject);
        }
    }
}
