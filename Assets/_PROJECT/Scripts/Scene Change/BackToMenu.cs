using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
    
}
