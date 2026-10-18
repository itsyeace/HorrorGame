using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float maxDistance = 10f;

    private float _pitch;
    private float _yaw;
    
    private PlayerControls _controls;
    private CharacterController _characterController;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        
        _controls = new PlayerControls();
        _controls.Player.Enable();
        _controls.Player.ToggleFlashlight.performed += OnToggleFlashlightInput;
        _controls.Player.Interact.performed += Interact;
    }

    void Start()
    {
        Debug.Log(flashlight == null ? "flashlight is null" : "flashlight exists");
    }

    void Update()
    {
        Move();
        Look();
    }

    void OnToggleFlashlightInput(InputAction.CallbackContext context)
    {
        flashlight.Toggle();
    }

    void Move()
    {
        Vector2 input = _controls.Player.Move.ReadValue<Vector2>();
        Vector3 movement = (transform.right * input.x) + (transform.forward * input.y);
        _characterController.Move(movement * (moveSpeed * Time.deltaTime));
    }

    void Look()
    {
        Vector2 lookInput =  _controls.Player.Look.ReadValue<Vector2>();
        _yaw += lookInput.x * mouseSensitivity;
        _pitch += lookInput.y * mouseSensitivity;
        
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
        cameraTransform.localRotation = Quaternion.Euler(-_pitch, 0f, 0f);
    }

    void Interact(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.OnInteract();
            }
        }
    }
}
