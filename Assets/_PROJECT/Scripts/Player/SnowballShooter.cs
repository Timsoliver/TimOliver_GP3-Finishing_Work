using UnityEngine;
using UnityEngine.InputSystem;

public class SnowballShooter : MonoBehaviour
{
    [Header("Shooter Shooter")] 
    public GameObject snowballPrefab;
    public Transform firePoint;
    public float throwForce = 20f;

    private bool hasSnowball;

    private void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed && hasSnowball)
        {
            ThrowSnowball();
            hasSnowball = false;
        }
    }

    private void ThrowSnowball()
    {
        GameObject snowball = Instantiate(snowballPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody rb = snowball.GetComponent<Rigidbody>();
        if(rb == null) rb = snowball.AddComponent<Rigidbody>();

        Vector3 dir = firePoint.forward;
        dir.y = 0f;
        dir.Normalize();
        
        rb.linearVelocity = dir * throwForce;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        SoundManager.Instance.PlayThrow();
    }

    public bool TryPickupSnowball()
    {
        if (hasSnowball) return false;
        hasSnowball = true;
        return true;
    }
    
    public bool HasSnowball => hasSnowball;
}