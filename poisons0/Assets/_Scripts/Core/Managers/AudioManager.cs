using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Clips")]
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip craftingSFX;
    [SerializeField] private AudioClip explosionSFX;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        PlayMusic(mainTheme);
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayExplosion() => PlaySFX(explosionSFX);
}