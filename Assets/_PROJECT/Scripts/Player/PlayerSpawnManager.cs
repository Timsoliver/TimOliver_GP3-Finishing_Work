using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    [Header("Player Prefabs")] 
    public GameObject[] playerPrefabs;

    [Header("Spawn Points")] 
    public Transform[] spawnPoints;

    private int playerCount = 0;

    private void OnEnable()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void OnDisable()
    {
        PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        int index = playerCount;

        if (index < playerPrefabs.Length && index < spawnPoints.Length)
        {
            GameObject newPlayer = Instantiate(playerPrefabs[index], spawnPoints[index].position,
                spawnPoints[index].rotation);
            
            PlayerInput newInput = newPlayer.GetComponent<PlayerInput>();
            
            newInput.SwitchCurrentControlScheme(playerInput.devices.ToArray());
            
            Destroy(playerInput.gameObject);
        }
        
        playerCount++;
    }
}
