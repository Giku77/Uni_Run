using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    public AudioSource audioSource;
    public AudioClip itemPickupClip;
    private UiManager uiManager;
    private float destroyTime = 5f;
    public enum ItemType
    {
        HealthPack,
        Ammo,
        Coin
    }

    public ItemType itemType;
    public int value = 10;


    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("Ui").GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("UiManager not found in the scene.");
        }
        StartCoroutine(destroyItem());
    }
    public void Use(GameObject target)
    {
        switch (itemType)
        {
            case ItemType.HealthPack:
                var playerHealth = target.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(value * 1.8f);
                }
                break;
            case ItemType.Ammo:
                var playerShooter = target.GetComponent<PlayerShooter>();
                if (playerShooter != null && playerShooter.gun != null)
                {
                    playerShooter.gun.AddAmmo(value);
                }
                break;
            case ItemType.Coin:
                uiManager.score += value;
                //uiManager.SetUpdateScore(uiManager.score + value);
                Debug.Log("Coin Collected!");
                break;
        }
        if (audioSource != null && itemPickupClip != null)
        {
            audioSource.PlayOneShot(itemPickupClip);
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.7f + Mathf.PingPong(Time.time, 0.8f), transform.position.z);
        transform.Rotate(Vector3.up, 60f * Time.deltaTime, Space.World);
    }

    public void setDestroyTime(float time)
    {
        destroyTime = time;
    }

    private IEnumerator destroyItem()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.Player))
        {
            Use(other.gameObject);
            //gameObject.SetActive(false); 
            Destroy(gameObject);
        }
    }
}
