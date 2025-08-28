using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
public class ZombieData : ScriptableObject
{
    public float health = 100f;
    public float speed = 2f;
    public float damage = 20f;

    public Color skin;
    //public float attackRate;
    //public int scoreValue;
    //public GameObject deathEffect;

}
