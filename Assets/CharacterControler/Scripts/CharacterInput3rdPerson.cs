using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class CharacterInput3rdPerson : MonoBehaviour
{
    public CharacterControllerBase characterController;
    public CameraFollow cameraFollow;
    
    //public Transform characterTransform;
    //public Transform cameraTransform;
    
    private InputAction moveControl;
    private InputAction lookControl;
    private InputAction jumpAction;
    private InputCharacter inputCharacter;
    private InputAction mouseDelta;
    private InputAction rewindAction;
    private InputAction sprintAction;

    public AbilityRewind rewind;
    private AbilityManager abilityManager;

    void Awake()
    {
        inputCharacter = new InputCharacter();
        abilityManager = GetComponent<AbilityManager>();
    }

    void OnEnable() {
        inputCharacter.Enable();

        moveControl = inputCharacter.Character.Move;
        moveControl.Enable();
        
        lookControl = inputCharacter.Character.Look;
        lookControl.Enable();
        
        jumpAction = inputCharacter.Character.Jump;
        jumpAction.Enable();
        
        mouseDelta = inputCharacter.Character.MouseDelta;
        mouseDelta.Enable();

        sprintAction = inputCharacter.Character.Sprint;
        sprintAction.Enable();

        rewindAction = inputCharacter.Character.Rewind;
        rewindAction.Enable();
    }
    
    void OnDisable() {
        moveControl.Disable();
        lookControl.Disable();
        
        jumpAction.Disable();
        
        mouseDelta.Disable();

        inputCharacter.Disable();
        rewindAction.Disable();

        sprintAction.Disable();

    }

    void OnDestroy() {
        moveControl.Dispose();
        lookControl.Dispose();
        
        jumpAction.Dispose();
        
        mouseDelta.Dispose();
        
        inputCharacter.Dispose();
        rewindAction.Dispose();

        sprintAction.Dispose();
    }

    void Update() {
        characterController.InputJump(jumpAction.WasPressedThisFrame(), jumpAction.IsPressed());
        characterController.InputMoveVector(GetWorldMoveVector());
        characterController.InputSprint(sprintAction.IsPressed());

        Vector2 lookVector = lookControl.ReadValue<Vector2>();
        lookVector += mouseDelta.ReadValue<Vector2>() * new Vector2(0.2f, 0.5f);
        
        cameraFollow.InputLookVector(lookVector);

        if (rewindAction.WasPressedThisFrame())
        {
            abilityManager.TryUseRewind();
        }
    }
    
    private Vector2 GetMoveVector() {
        return moveControl.ReadValue<Vector2>();
    }

    private Vector3 GetWorldMoveVector() {

        Vector2 localInput = GetMoveVector();
        Vector3 worldInput = Vector3.zero;

        Vector3 camRight = cameraFollow.transform.right;
        camRight.y = 0f;
        camRight.Normalize();
        
        Vector3 camFarward = cameraFollow.transform.forward;
        camFarward.y = 0f;
        camFarward.Normalize();
        
        worldInput += camRight * localInput.x;
        worldInput += camFarward * localInput.y;
        
        return worldInput;
    }
}
