using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGMAudioSource;
    public AudioSource EventAudioSource;
    public AudioClip[] BGMClips;
    public AudioClip[] clips;

    private void Start()
    {
        BgmSoundPlay("Prologue");
    }

    public void BgmSoundPlay(string clipName)
    {
        foreach (AudioClip clip in BGMClips)
        { 
            if (clip.name != clipName) continue;
            BGMAudioSource.clip = clip;
            break;
        }
        BGMAudioSource.loop = true;
        BGMAudioSource.Play();
    }

    public void EventSoundPlay(string clipName)
    {
        foreach (AudioClip clip in clips)
        { 
            if (clip.name != clipName) continue;
            EventAudioSource.PlayOneShot(clip);
            break;
        }
    }
}
