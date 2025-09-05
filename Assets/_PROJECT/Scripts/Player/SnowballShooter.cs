using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnowballShooter : MonoBehaviour
{
    [Header("Shooter Settings")]
    public GameObject snowballPrefab;
    public Transform firePoint;
    public float throwForce = 20f;
    
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ThrowSnowball();
        }
    }

    private void ThrowSnowball()
    {
        GameObject snowball = Instantiate(snowballPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody rb = snowball.GetComponent<Rigidbody>();
        
        Vector3 throwDirection = firePoint.forward;
        throwDirection.y = 0f;
        throwDirection.Normalize();
        
        rb.linearVelocity = throwDirection * throwForce;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }



}
