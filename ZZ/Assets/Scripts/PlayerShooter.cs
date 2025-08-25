using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static readonly int idReload = Animator.StringToHash("Reload");

    public Gun gun;
    private PlayerInput playerInput;
    private Animator animator;

    public Transform gunPivot;
    public Transform leftHandMount;
    public Transform rightHandMount;


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

    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);

    }
}
