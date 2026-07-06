using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterControllerBase characterController;

    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    private static readonly int JumpStartState = Animator.StringToHash("Jump_Start");

    void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (characterController == null)
        {
            characterController = GetComponent<CharacterControllerBase>();
        }
    }

    void OnEnable()
    {
        if (characterController != null)
        {
            characterController.OnJumped += PlayJumpAnim;
        }
    }

    void OnDisable()
    {
        if (characterController != null)
        {
            characterController.OnJumped -= PlayJumpAnim;
        }
    }

    void Update()
    {
        if (animator == null || characterController == null)
            return;

        bool grounded = characterController.IsGrounded();
        bool moving = characterController.HasMoveInput();
        bool sprinting = characterController.IsSprinting();

        animator.SetBool(IsWalking, grounded && moving && !sprinting);
        animator.SetBool(IsRunning, grounded && moving && sprinting);
        animator.SetBool(IsGrounded, grounded);
    }

    void PlayJumpAnim()
    {
        if (animator == null)
            return;

        animator.Play(JumpStartState, 0, 0f);
    }
}