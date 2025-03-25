using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // variables for audio source

    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    // variables for audio clips 

    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip healthClip;
    public AudioClip staminaClip;
    public AudioClip winClip;
    public AudioClip killClip;
    public AudioClip swordClip;
    public AudioClip bowClip;
    public AudioClip hitClip;


    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
            
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip != null && vfxAudioSource != null)
        {
            vfxAudioSource.PlayOneShot(sfxClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
