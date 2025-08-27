using UnityEngine;

public static class TagManager
{
    public static readonly string Player = "Player";
    public static readonly string Enemy = "Enemy";
    public static readonly string Item = "Item";
    public static readonly string Obstacle = "Obstacle";
    public static readonly string Projectile = "Projectile";
}

public class PlayerMovement : MonoBehaviour
{
    private static readonly int MoveHash = Animator.StringToHash("Move");

    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    public AudioClip itemPickuoClip;
    private AudioSource audioSource;

    private Gun gun;
    private PlayerHealth playerHealth;

    private PlayerInput playerInput;
    private Rigidbody rb;
    private Animator animator;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        gun = GetComponentInChildren<Gun>();
        audioSource = GetComponent<AudioSource>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Coin"))
    //    {
    //        other.gameObject.SetActive(false);
    //        Debug.Log("Coin Collected!");
    //        if (audioSource != null && itemPickuoClip != null)
    //        {
    //            audioSource.PlayOneShot(itemPickuoClip);
    //        }
    //    }
    //    else if (other.CompareTag("Ammo"))
    //    {
    //        other.gameObject.SetActive(false);
    //        gun.currentMagazine += 10;
    //        Debug.Log("Ammo!");
    //        if (audioSource != null && itemPickuoClip != null)
    //        {
    //            audioSource.PlayOneShot(itemPickuoClip);
    //        }
    //    }
    //    else if (other.CompareTag("HealthPack"))
    //    {
    //        other.gameObject.SetActive(false);
    //        playerHealth.Heal(playerHealth.MaxHealth);
    //        Debug.Log("Health!");
    //    }
    //}
    private void FixedUpdate()
    {
        //회전
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y; 

            transform.LookAt(target);
        }
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, playerInput.Roatate * rotationSpeed * Time.fixedDeltaTime, 0f));
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, playerInput.Roatate * rotationSpeed * Time.deltaTime, 0f));

        //이동
        rb.MovePosition(rb.position + transform.forward * playerInput.Move * moveSpeed * Time.fixedDeltaTime);
        //rb.MovePosition(transform.position + transform.forward * playerInput.Move * moveSpeed * Time.deltaTime);

        //애니메이션 설정
        if (animator != null)
        {
            //animator.SetFloat("Move", playerInput.Move); // 오버로딩으로 int, float, double 등 다양한 타입을 지원합니다.
            //보통 id로 치환해서 쓰는 방식이 많음
            animator.SetFloat(MoveHash, playerInput.Move);
            //animator.SetFloat("RotateSpeed", playerInput.Roatate);
            //animator.SetBool("Fire", playerInput.Fire);
            //animator.SetBool("Reload", playerInput.Reload);
        }
    }
}
