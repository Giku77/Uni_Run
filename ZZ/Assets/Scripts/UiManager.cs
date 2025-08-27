using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Zombie zombie;
    public Text AmmoText;
    public Text WaveText;
    public Text ScoreText;
    public Button RestartButton;
    public GameObject GameOverUi;
    public Gun gun;
    public PlayerHealth playerHealth;
    public int leftEnemy;
    public int score;
    private int waveNumber = 1;

    public GameObject target;

    void SpawnRandomZombie(int num = 1)
    {
        for(int i = 0; i < num; i++)
        {
            //Vector3 scale = target.transform.localScale;

            //float randX = Random.Range(-scale.x / 2f, scale.x / 2f);
            //float randY = Random.Range(-scale.y / 2f, scale.y / 2f);
            //float randZ = Random.Range(-scale.z / 2f, scale.z / 2f);

            //Vector3 randomPos = target.transform.position + new Vector3(randX, randY, randZ);

            float randX = Random.Range(-10f, 10f);
            //float randY = Random.Range(-scale.y / 2f, scale.y / 2f);
            float randZ = Random.Range(-10f, 10f);
            Vector3 randomPos = new Vector3(randX, target.transform.position.y, randZ);
            Instantiate(zombie, randomPos, Quaternion.identity);

            //Debug.Log("·£´ý À§Ä¡: " + randomPos);
        }
    }


    private void Start()
    {
        if (AmmoText != null)
        {
            AmmoText.text = gun.currentAmmo + "/" + gun.currentMagazine;
        }
        if (GameOverUi != null)
        {
            GameOverUi.SetActive(false);
        }
        if (WaveText != null)
        {
            WaveText.text = "Wave : "+ waveNumber + "\r\nEnemy Left : " + leftEnemy;
        }
        if (ScoreText != null)
        {
            ScoreText.text = "SCORE : " + score;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SpawnRandomZombie();
        }
        if (AmmoText != null) AmmoText.text = gun.currentAmmo + "/" + gun.currentMagazine;
        if (playerHealth != null && playerHealth.Isdead && GameOverUi != null) GameOverUi.SetActive(true);
        if (WaveText != null) WaveText.text = "Wave : " + waveNumber + "\r\nEnemy Left : " + leftEnemy;
        if (ScoreText != null) ScoreText.text = "SCORE : " + score;

        if (leftEnemy <= 0)
        {
            waveNumber++;
            leftEnemy = waveNumber * 3;
            SpawnRandomZombie(leftEnemy);
            //for (int i = 0; i < leftEnemy; i++)
            //{
            //    SpawnRandomZombie();
            //}
        }
    }
    public void OnRestartButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
