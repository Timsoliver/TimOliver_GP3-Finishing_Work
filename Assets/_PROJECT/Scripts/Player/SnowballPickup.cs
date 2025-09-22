using UnityEngine;
using System.Collections;

public class SnowballPickup : MonoBehaviour
{
    [Header("Settings")] public float respawnTime = 5f;
    public Color availableColor = Color.white;
    public Color unavailableColor = Color.gray;

    [Header("UI Prompt")] 
    public GameObject promptText;

    public float showRange = 3f;
    
    private bool isAvailable = true;
    private Renderer rend;
    private Collider col;
    private Transform player;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        
        if (promptText == null)
            promptText.SetActive(false);

        SetAvailable(true);
    }

    private void Update()
    {
        if (promptText == null || player == null) return;
        
        float dist = Vector3.Distance(transform.position, player.position);
        promptText.SetActive(isAvailable && dist < showRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            player = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform == player)
        {
            player = null;
            if (promptText != null)
                promptText.SetActive(false);
        }
    }

    public bool TryPickup(SnowballShooter shooter)
    {
        if (!isAvailable) return false;
        if (!shooter.TryPickupSnowball()) return false;
        
        SetAvailable(false);
        StartCoroutine(RespawnRoutine());
        
        SoundManager.Instance.PlayPickup();
        
        return true;
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnTime);
        SetAvailable(true);
    }

    private void SetAvailable(bool value)
    {
        isAvailable = value;
        
        if (col != null) 
            col.enabled = value;
        
        if(rend != null)
            rend.material.color = value ? availableColor : unavailableColor;
        
        if (!value && promptText != null)
            promptText.SetActive(false);
    }

    public void ResetPickup()
    {
        StopAllCoroutines();
        SetAvailable(true);
    }
}