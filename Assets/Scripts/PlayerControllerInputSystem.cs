using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerInputSystem : MonoBehaviour
{
    [SerializeField] private float WalkSpeed = 5.0f;
    [SerializeField] private float LookSensitivity = 0.2f;
    [SerializeField] private float LookAngleLimit = 90f;
    
    private Camera mainCamera;
    private CharacterController characterController;

    private InputAction moveInput;

    private float CurrentMoveSpeed = 0.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float lookAngle = 0.0f;

    private void Start()
    {
        if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        if (AttitudeSensor.current != null)
        {
            InputSystem.EnableDevice(AttitudeSensor.current);
        }
        
        mainCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        
        moveInput = InputSystem.actions.FindAction("Move");
        
        //make Cursor invisible on Screen
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
        CurrentMoveSpeed = WalkSpeed;
    }

    private void Update()
    {
        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());
        HandleMovement(moveVector);
        HandleLooking(mouseDelta);
    }

    private void HandleMovement(Vector2 moveVector)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        float oldY = moveDirection.y;
        Vector2 newSpeed = new Vector2(moveVector.y * CurrentMoveSpeed, moveVector.x * CurrentMoveSpeed);
        
        moveDirection = (forward * newSpeed.x) + (right * newSpeed.y);
        moveDirection.y = oldY;
        
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleLooking(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * LookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -LookAngleLimit, LookAngleLimit);
        
        mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * LookSensitivity, 0);
    }
}
