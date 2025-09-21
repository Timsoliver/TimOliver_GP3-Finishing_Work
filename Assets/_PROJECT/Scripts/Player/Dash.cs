using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]

public class Dash : MonoBehaviour
{
    private PlayerController moveScript;
    private Collider playerCollider;
    private Renderer playerRenderer;
    private string originalTag;
    private Material originalMaterial;

    [Header("Dash Settings")]
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.5f;
    
    [Header("Visual Dash")]
    public Material dashMaterial;

    public Image dashUI;
    
    private bool isDashing;
    private bool dashAvailable = true;

    void Awake()
    {
        moveScript = GetComponent<PlayerController>();
        playerCollider = GetComponent<Collider>();
        playerRenderer = GetComponent<Renderer>();

        if (playerRenderer != null)
            originalMaterial = playerRenderer.material;
        
        originalTag = gameObject.tag;

        if (dashUI != null)
        {
            dashUI.fillAmount = 1f;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && dashAvailable)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        dashAvailable = false;
        
        if (playerCollider != null) playerCollider.enabled = false;
        gameObject.tag = "Untagged";
        
        if (playerRenderer != null && dashMaterial != null)
            playerRenderer.material = dashMaterial;

        Vector3 dashDir = moveScript.FacingDirection;
        if (dashDir.sqrMagnitude < 0.01f)
            dashDir = transform.forward;

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            float step = (dashDistance / dashDuration) * Time.deltaTime;
            moveScript.Controller.Move(dashDir * step);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        if (playerCollider != null) playerCollider.enabled = true;
        gameObject.tag = originalTag;
        
        if (playerRenderer != null && originalMaterial !=null)
            playerRenderer.material = originalMaterial;
        
        isDashing = false;

        if (dashUI != null)
        {    dashUI.fillAmount = 0f;
            yield return StartCoroutine(CooldownRoutine());
        }
        else
        {
            yield return new WaitForSeconds(dashCooldown);
        }
        
        dashAvailable = true;
    }

    private IEnumerator CooldownRoutine()
    {
        float elapsed = 0f;

        while (elapsed < dashCooldown)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / dashCooldown);
            
            dashUI.fillAmount = progress;
            
            yield return null;
        }
        
        dashUI.fillAmount = 1f;
    }
}
