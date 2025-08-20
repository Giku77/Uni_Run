using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class GameManager : Singleton<GameManager>
public class GameManager : MonoBehaviour
{
    public bool IsGameOver { get; private set; } 
    private int score;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUi;

    public void Awake()
    {
        //base.Awake();
        //DontDestroyOnLoad(gameObject);
        //FindUIObjects();
        scoreText.text = "Score: " + score;
        gameOverUi.SetActive(false);
        IsGameOver = false;
        score = 0;
    }

    //private void Start()
    //{
    //    //if(scoreText == null || gameOverUi == null)
    //    //{
    //    //    FindUIObjects();
    //    //}
    //    //FindUIObjects();
    //    //scoreText.text = "Score: " + score;
    //    //gameOverUi.SetActive(false);
    //}

    //private void FindUIObjects()
    //{
    //    int findCount = 0;

    //    Scene scene = SceneManager.GetActiveScene();

    //    foreach (GameObject rootObj in scene.GetRootGameObjects())
    //    {
    //        foreach (Transform child in rootObj.GetComponentsInChildren<Transform>(true))
    //        {
    //            GameObject go = child.gameObject;

    //            if (go.CompareTag("Score"))
    //            {
    //                scoreText = go.GetComponent<TextMeshProUGUI>();
    //                findCount++;
    //            }

    //            if (go.CompareTag("GameOver"))
    //            {
    //                gameOverUi = go;
    //                findCount++;
    //            }

    //            if (findCount >= 2)
    //                break;
    //        }
    //        if (findCount >= 2)
    //            break;
    //    }
    //}

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    FindUIObjects();
    //    scoreText.text = "Score: " + score;
    //    gameOverUi.SetActive(false);
    //}

    private void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Game Over! Press Escape to restart.");
            IsGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }
    public void AddScore(int add)
    {         
        if (IsGameOver) return; 
        score += add;
        scoreText.text = "Score: " + score;
    }
    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUi.SetActive(true);
        Debug.Log("Player is dead. Game Over!");
    }
}   

