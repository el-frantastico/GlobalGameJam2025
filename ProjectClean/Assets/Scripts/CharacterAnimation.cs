using KinematicCharacterController;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private KinematicCharacterMotor characterMotor;
    [SerializeField]
    private Animator animator;

    private bool isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterMotor = GetComponent<KinematicCharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterMotor.GroundingStatus.IsStableOnGround)
        {
            isFalling = false;
            animator.SetBool("IsGrounded", true);
        }
        else if (!isFalling)
        {
            isFalling = true;
            animator.SetBool("IsGrounded", false);
            animator.SetTrigger("Jump");
        }

        float lateralMovement = Mathf.Abs(characterMotor.Velocity.x);
        animator.SetFloat("WalkSpeed", lateralMovement);
    }
}
