using UnityEngine;
using UnityEngine.InputSystem;

public class SnowballShooter : MonoBehaviour
{
    [Header("Shooter Settings")]
    public GameObject snowballPrefab;
    public Transform firePoint;
    public float throwForce = 20f;

    private bool hasSnowball = false;

    // Called from PlayerInput → Invoke Unity Events (bind Throw to this)
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed && hasSnowball)
        {
            ThrowSnowball();
            hasSnowball = false;
            Debug.Log("Snowball thrown. hasSnowball = false");
        }
    }

    private void ThrowSnowball()
    {
        if (snowballPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Snowball prefab or firePoint not set.");
            return;
        }

        GameObject snowball = Instantiate(snowballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = snowball.GetComponent<Rigidbody>();
        if (rb == null) rb = snowball.AddComponent<Rigidbody>();

        Vector3 throwDirection = firePoint.forward;
        throwDirection.y = 0f;
        throwDirection.Normalize();

        rb.velocity = throwDirection * throwForce;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Corrected TryPickup - returns true only if pickup succeeded
    public bool TryPickupSnowball()
    {
        if (hasSnowball)
            return false;

        hasSnowball = true;
        Debug.Log("Picked up snowball. hasSnowball = true");
        return true;
    }

    public bool HasSnowball => hasSnowball;
}