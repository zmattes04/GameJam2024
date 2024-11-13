using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource soundEffectSource;
    public SoundEffectType[] soundEffectTypes;
    public AudioClip[] audioClips;
    private SoundEffectType soundEffectPrevious;
    public bool loopInitial;
    public bool activatedOnce;

    private Dictionary<SoundEffectType, AudioClip> soundEffectDictionary = new Dictionary<SoundEffectType, AudioClip>();

    void Start()
    {
        soundEffectSource.loop = loopInitial;
        soundEffectDictionary = new Dictionary<SoundEffectType, AudioClip>(soundEffectTypes.Length);
        for (int i = 0; i < soundEffectTypes.Length; i++)
        {
            soundEffectDictionary.Add(soundEffectTypes[i], audioClips[i]);
        }
        soundEffectPrevious = SoundEffectType.Score;
    }

    public void PlaySoundEffect(SoundEffectType soundEffect)
    {
        if (soundEffectSource.isPlaying)
        {
            if ((soundEffect != soundEffectPrevious) || activatedOnce)
            {
                soundEffectSource.Stop();
                soundEffectSource.clip = GetAudioClip(soundEffect);
                soundEffectSource.Play();
            }
        }
        else
        {
            soundEffectSource.clip = GetAudioClip(soundEffect);
            soundEffectSource.Play();
        }
        soundEffectPrevious = soundEffect;
    }

    private AudioClip GetAudioClip(SoundEffectType soundEffect)
    {
        return soundEffectDictionary.TryGetValue(soundEffect, out AudioClip value)
            ? value
            : audioClips[0];
    }
}
