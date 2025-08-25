using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static readonly int idReload = Animator.StringToHash("Reload");

    public Gun gun;
    private PlayerInput playerInput;
    private Animator animator;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerInput.Fire)
        {
            gun.Fire();
        }
        else if (playerInput.Reload)
        {
            if(gun.Reload())
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }
                animator.SetTrigger(idReload);
            }
        }
    }
}
