using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [Header("Looping sources")]
    [SerializeField] private AudioSource runSource;
    [SerializeField] private AudioSource slideSource;

    [Header("One-shot source")]
    [SerializeField] private AudioSource sfxSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        AudioSource[] sources = GetComponents<AudioSource>();

        runSource = sources[1];
        slideSource = sources[2];
        sfxSource = sources[3];
    }

    // ---------- ONE-SHOTS (jump, hit, etc.) ----------

    public void PlayOneShot(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, volume);   // cannot be stopped individually (short SFX)
    }

    // ---------- RUN LOOP ----------

    public void PlayRunLoop(AudioClip clip, float volume = 1f)
    {
        if (runSource == null || clip == null) return;

        if (runSource.clip != clip)
            runSource.clip = clip;

        runSource.volume = volume;
        runSource.loop = true;
        if (!runSource.isPlaying)
            runSource.Play();
    }

    public void StopRunLoop()
    {
        if (runSource == null) return;
        runSource.Stop();   // stops only the run loop
    }

    // ---------- SLIDE LOOP ----------

    public void PlaySlideLoop(AudioClip clip, float volume = 1f)
    {
        if (slideSource == null || clip == null) return;

        if (slideSource.clip != clip)
            slideSource.clip = clip;

        slideSource.volume = volume;
        slideSource.loop = true;
        if (!slideSource.isPlaying)
            slideSource.Play();
    }

    public void StopSlideLoop()
    {
        if (slideSource == null) return;
        slideSource.Stop(); // stops only the slide loop
    }
}
