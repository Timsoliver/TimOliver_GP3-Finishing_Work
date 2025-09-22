using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour 
{
    public static RoundManager Instance { get; private set; }
    
    [Header("UI")]
    public Button startRoundButton;
    public GameObject player1WinPanel;
    public GameObject player2WinPanel;

    [Header("Settings")] 
    public string playerTag = "Player";
    public string spawnWallsTag = "SpawnWalls";
    public float endPanelDelay = 3f;
    
    private List<GameObject> players = new List<GameObject>();
    private bool roundActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (startRoundButton != null)
            startRoundButton.onClick.AddListener(OnStartRoundClicked);
        
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag(playerTag));

        HideEndPanels();

        LockSpawns(true);
    }

    private void Update()
    {
        if (!roundActive) return;

        CheckForRoundEnd();
    }

    public void RegisterPlayer(GameObject player)
    {
        if (player == null) return;
        if (!players.Contains(player)) players.Add(player);
    }

    private void OnStartRoundClicked()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag(playerTag));

        if (players.Count < 2)
        {
            Debug.LogWarning("[RoundManager] There are no players in your scene.");
            return;
        }
        LockSpawns(false);
        
        if(startRoundButton != null) startRoundButton.gameObject.SetActive(false);
        
        roundActive = true;
    }

    private void CheckForRoundEnd()
    {
        int aliveCount = 0;
        GameObject lastAlive = null;

        for (int i = 0; i < players.Count; i++)
        {
            GameObject p = players[i];

            if (p != null && p.activeInHierarchy)
            {
                aliveCount++;
                lastAlive = p;
            }
        }

        if (aliveCount <= 1)
        {
            roundActive = false;

            if (lastAlive == null)
            {
                StartCoroutine(ShowPanelAndRestart(null));
            }
            else
            {
                StartCoroutine(ShowPanelAndRestart(lastAlive));
            }
        }
    }

    private IEnumerator ShowPanelAndRestart(GameObject winner)
    {
        HideEndPanels();

        if (winner != null)
        {
            int idx = players.IndexOf(winner);

            if (idx == 0 && player1WinPanel != null)
                player1WinPanel.SetActive(true);
            else if (idx == 1 && player2WinPanel != null)
                player2WinPanel.SetActive(true);
            else
            {
                if (winner.name.Contains("1") && player1WinPanel != null)
                    player1WinPanel.SetActive(true);
                else if (winner.name.Contains("2") && player2WinPanel != null)
                    player2WinPanel.SetActive(true);
                else if (player1WinPanel != null)
                    player1WinPanel.SetActive(true);
                
            }
        }
        else
        {
            if (player1WinPanel != null) player1WinPanel.SetActive(true);
        }
        
        yield return new WaitForSeconds(endPanelDelay);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LockSpawns(bool locked)
    {
        var walls = GameObject.FindGameObjectsWithTag(spawnWallsTag);
        for (int i = 0; i < walls.Length; i++)
            walls[i].SetActive(locked);
    }

    private void HideEndPanels()
    {
        if (player1WinPanel != null) player1WinPanel.SetActive(false);
        if (player2WinPanel != null) player2WinPanel.SetActive(false);
    }
    

}