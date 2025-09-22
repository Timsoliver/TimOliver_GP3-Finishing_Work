using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    
    public Transform[] spawnPoints;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public Transform GetSpawnForPlayer(PlayerHealth player)
    {
        int index = Mathf.Abs(player.GetInstanceID() % spawnPoints.Length);
        return spawnPoints[index];
    }
}


