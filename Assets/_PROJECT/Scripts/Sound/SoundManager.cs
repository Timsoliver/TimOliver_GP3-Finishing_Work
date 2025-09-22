using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")] 
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Sound Effects")] 
    public AudioClip winClip;
    public AudioClip throwClip;
    public AudioClip pickupClip;
    public AudioClip destroyClip;

    [Header("Music")] 
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!musicSource.isPlaying)
            PlayMusic();
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayWin()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
        
        PlaySFX(winClip);
    }
    
    public void PlayThrow() => PlaySFX(throwClip);
    public void PlayPickup() => PlaySFX(pickupClip);
    public void PlayDestroy() => PlaySFX(destroyClip);

    public void PlayMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
