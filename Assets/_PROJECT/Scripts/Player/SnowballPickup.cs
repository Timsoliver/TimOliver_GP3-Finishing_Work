using UnityEngine;

public class SnowballPickup : MonoBehaviour
{
    // Called by the player to attempt pickup.
    // Returns true if consumed (picked up) and the pickup should be removed.
    public bool TryPickup(SnowballShooter shooter)
    {
        if (shooter == null) return false;

        if (!shooter.TryPickupSnowball())
            return false; // player already has one

        // Successful pickup - do optional VFX/sound here
        Destroy(gameObject);
        return true;
    }
}