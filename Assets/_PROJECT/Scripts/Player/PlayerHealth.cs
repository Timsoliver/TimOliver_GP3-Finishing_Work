using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;

    public void Eliminate()
    {
        if (!IsAlive) return;
        
        IsAlive = false;
        gameObject.SetActive(false);
    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        IsAlive = true;
        transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
    }
}
