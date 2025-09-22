using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SnowballShooter))]
public class PlayerInteraction : MonoBehaviour
{
    private SnowballPickup currentPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SnowballPickup pickup))
            currentPickup = pickup;
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentPickup != null && other.gameObject == currentPickup.gameObject)
            currentPickup = null;
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!context.performed || currentPickup == null) return;
        
        var shooter = GetComponent<SnowballShooter>();
        if (currentPickup.TryPickup(shooter))
            currentPickup = null;
    }
}