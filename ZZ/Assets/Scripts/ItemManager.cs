using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject Coin;
    private GameObject HealthPack;
    private GameObject AmmoPack;
    //private GameObject Player;

    public float rotationSpeed = 60f;

    private void Start()
    {
        Coin = GameObject.FindGameObjectWithTag("Coin");
        HealthPack = GameObject.FindGameObjectWithTag("HealthPack");
        AmmoPack = GameObject.FindGameObjectWithTag("Ammo");
        //Player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        if (Coin != null)
        {
            Coin.transform.position = new Vector3(Coin.transform.position.x, 0.7f + Mathf.PingPong(Time.time, 0.8f), Coin.transform.position.z);
            Coin.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
        if (HealthPack != null)
        {
            HealthPack.transform.position = new Vector3(HealthPack.transform.position.x, 0.7f + Mathf.PingPong(Time.time, 0.8f), HealthPack.transform.position.z);
            HealthPack.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
        if (AmmoPack != null)
        {
            AmmoPack.transform.position = new Vector3(AmmoPack.transform.position.x, 0.7f + Mathf.PingPong(Time.time, 0.8f), AmmoPack.transform.position.z);
            AmmoPack.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
