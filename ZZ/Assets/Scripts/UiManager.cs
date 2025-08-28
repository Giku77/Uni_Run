using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private int waveNumber;

    public GameObject target;

    public void SetAmmoText(int currentAmmo, int currentMagazine)
    {
        if (AmmoText != null)
        {
            AmmoText.text = currentAmmo + "/" + currentMagazine;
        }
    }

    public void SetUpdateScore(int score)
    {
        if (ScoreText != null)
        {
            ScoreText.text = "SCORE : " + score;
        }
    }

    public void SetWaveInfo(int waveNumber, int leftEnemy)
    {
        if (WaveText != null)
        {
            WaveText.text = "Wave : " + waveNumber + "\r\nEnemy Left : " + leftEnemy;
        }
    }

    public void SetActiveGameOverUi(bool isActive)
    {
        if (GameOverUi != null)
        {
            GameOverUi.SetActive(isActive);
        }
    }

    //private void SpawnRandomZombie(int num = 1)
    //{
    //    for(int i = 0; i < num; i++)
    //    {
    //        //Vector3 scale = target.transform.localScale;

    //        //float randX = Random.Range(-scale.x / 2f, scale.x / 2f);
    //        //float randY = Random.Range(-scale.y / 2f, scale.y / 2f);
    //        //float randZ = Random.Range(-scale.z / 2f, scale.z / 2f);

    //        //Vector3 randomPos = target.transform.position + new Vector3(randX, randY, randZ);

    //        //zombie.SetZombieData(zombie.GetRandZombieData());
    //        float randX = Random.Range(-8f, 8f);
    //        //float randY = Random.Range(-scale.y / 2f, scale.y / 2f);
    //        float randZ = Random.Range(-8f, 8f);
    //        Vector3 randomPos = new Vector3(randX, target.transform.position.y, randZ);
    //        Instantiate(zombie, randomPos, Quaternion.identity);

    //        //Debug.Log("·£´ý À§Ä¡: " + randomPos);
    //    }
    //}

    private void OnEnable()
    {
       //waveNumber = zombieSpawner.GetWaveNumber();
       SetAmmoText(gun.currentAmmo, gun.currentMagazine);
       SetWaveInfo(waveNumber, leftEnemy);
       SetUpdateScore(score);
       SetActiveGameOverUi(false);
       //SpawnRandomZombie(3);
    }


    //private void Start()
    //{
    //    if (AmmoText != null)
    //    {
    //        AmmoText.text = gun.currentAmmo + "/" + gun.currentMagazine;
    //    }
    //    if (GameOverUi != null)
    //    {
    //        GameOverUi.SetActive(false);
    //    }
    //    if (WaveText != null)
    //    {
    //        WaveText.text = "Wave : "+ waveNumber + "\r\nEnemy Left : " + leftEnemy;
    //    }
    //    if (ScoreText != null)
    //    {
    //        ScoreText.text = "SCORE : " + score;
    //    }
    //    SpawnRandomZombie(3);
    //}
    private void Update()
    {
        //if (AmmoText != null) AmmoText.text = gun.currentAmmo + "/" + gun.currentMagazine;
        //if (playerHealth != null && playerHealth.Isdead && GameOverUi != null) GameOverUi.SetActive(true);
        //if (WaveText != null) WaveText.text = "Wave : " + waveNumber + "\r\nEnemy Left : " + leftEnemy;
        if (ScoreText != null) ScoreText.text = "SCORE : " + score;

        //if (leftEnemy <= 0)
        //{
        //    waveNumber++;
        //    leftEnemy = waveNumber * 3;
        //    SpawnRandomZombie(leftEnemy);
        //    //for (int i = 0; i < leftEnemy; i++)
        //    //{
        //    //    SpawnRandomZombie();
        //    //}
        //}
    }
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
