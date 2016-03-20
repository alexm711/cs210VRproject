using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance = null;

    public AudioSource effects;
    public AudioSource music;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void playSoundEffectOnce(AudioClip clip)
    {
        effects.PlayOneShot(clip, 1f);
    }

    public void playSoundEffect(AudioClip clip)
    {
        if (effects.clip != clip || !effects.isPlaying)
        {
            effects.clip = clip;
            effects.Play();
        }

    }

    public void stopSoundEffect()
    {
        effects.Stop();
    }

    public void setBackgroundMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }
}