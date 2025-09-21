using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerPrefabCycler : MonoBehaviour
{
    [Header("Prefabs for Players (Max 4)")]
    public GameObject[] playerPrefabs;
    [Header("Spawn Points (Optional)")]
    public Transform[] spawnPoints;

    private PlayerInputManager manager;
    private int playerIndex = 0;

    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();

        if (playerPrefabs.Length > 0)
            manager.playerPrefab = playerPrefabs[0];
    }

    private void OnEnable()
    {
        manager.onPlayerJoined += OnPlayerJoined;
    }

    private void OnDisable()
    {
        manager.onPlayerJoined -= OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput player)
    {
        // Place player at its spawn point
        if (playerIndex < spawnPoints.Length)
        {
            player.transform.SetPositionAndRotation(
                spawnPoints[playerIndex].position,
                spawnPoints[playerIndex].rotation
            );
        }

        Debug.Log($"Player {playerIndex + 1} joined with {manager.playerPrefab.name}");

        playerIndex++;

        if (playerIndex < playerPrefabs.Length)
        {
            // Assign next prefab for the next player
            manager.playerPrefab = playerPrefabs[playerIndex];
        }
        else
        {
            Debug.Log("Max players reached, disabling manager.");
            manager.enabled = false;
        }
    }
}