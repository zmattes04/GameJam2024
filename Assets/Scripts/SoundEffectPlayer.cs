using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource soundEffectSource;
    public SoundEffectType[] soundEffectTypes;
    public AudioClip[] audioClips;
    public float[] volumes;
    private SoundEffectType soundEffectPrevious;
    public bool loopInitial;
    public bool activatedOnce;

    private Dictionary<SoundEffectType, SoundEffectStruct> soundEffectDictionary = new Dictionary<SoundEffectType, SoundEffectStruct>();

    void Start()
    {
        soundEffectSource.loop = loopInitial;
        soundEffectDictionary = new Dictionary<SoundEffectType, SoundEffectStruct>(soundEffectTypes.Length);
        for (int i = 0; i < soundEffectTypes.Length; i++)
        {
            soundEffectDictionary.Add(soundEffectTypes[i], new SoundEffectStruct(audioClips[i], volumes[i]));
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
                soundEffectSource.volume = GetAudioClip(soundEffect).Volume;
                Debug.Log(soundEffectSource.volume);
                soundEffectSource.clip = GetAudioClip(soundEffect).Audio;
                soundEffectSource.Play();
            }
        }
        else
        {
            soundEffectSource.clip = GetAudioClip(soundEffect).Audio;
            soundEffectSource.Play();
        }
        soundEffectPrevious = soundEffect;
    }

    private SoundEffectStruct GetAudioClip(SoundEffectType soundEffect)
    {
        return soundEffectDictionary.TryGetValue(soundEffect, out SoundEffectStruct value)
            ? value
            : new SoundEffectStruct(audioClips[0], volumes[0]);
    }
}
