using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    public AudioSource audioSource;
    public AudioClip itemPickupClip;
    public UiManager uiManager;
    public enum ItemType
    {
        HealthPack,
        Ammo,
        Coin
    }

    public ItemType itemType;
    public int value = 10;

    public void Use(GameObject target)
    {
        switch (itemType)
        {
            case ItemType.HealthPack:
                var playerHealth = target.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(value);
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
                uiManager.score += 10;
                Debug.Log("Coin Collected!");
                break;
        }
        if (audioSource != null && itemPickupClip != null)
        {
            audioSource.PlayOneShot(itemPickupClip);
        }
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
