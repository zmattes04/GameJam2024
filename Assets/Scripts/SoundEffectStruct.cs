using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SoundEffectStruct
{
    public AudioClip Audio { get; set; }
    public float Volume { get; set; }

    public SoundEffectStruct(AudioClip audio, float volume)
    {
        Audio = audio;
        Volume = volume;
    }
}

public enum SoundEffectType
{
    Score
}

