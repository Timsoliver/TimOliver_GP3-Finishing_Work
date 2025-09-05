using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInput;
    
    private Vector2 movementInput;
    private Vector2 aimInput;
    
    [SerializeField] private float controllerDeadZone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    private bool isGamepad;

    [Header("Movement Settings")] 
    public float speed = 5;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        
        isGamepad = playerInput.currentControlScheme.Equals("Gamepad");
    }

    private void Update()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        move = Vector3.ClampMagnitude(move, 1f);

        controller.Move(move * speed * Time.deltaTime);
        
        HandleRotation(move);
    }
    
    #region Input Callbacks
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }
    #endregion

    void HandleRotation(Vector3 move)
    {
        if (isGamepad)
        {
            if (Math.Abs(aimInput.x) > controllerDeadZone || Mathf.Abs(aimInput.y) > controllerDeadZone)
            {
                Vector3 playerDirection = new Vector3(aimInput.x, 0, aimInput.y);
                
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
            else if (move.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(move, Vector3.up);
            }
        }
        else
        {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 lookDirection = hit.point - transform.position;
                    lookDirection.y = 0;
                    if(lookDirection.sqrMagnitude > 0.01f)
                        transform.rotation = Quaternion.LookRotation(lookDirection);
                } 
        }
        
    }

    
}    

   

  