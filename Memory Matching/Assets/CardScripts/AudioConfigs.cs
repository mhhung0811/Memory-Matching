using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="AudioConfigs", menuName ="Audio/AudioConfigs")]
public class AudioConfigs : ScriptableObject
{
    public List<AudioConfig> all_audio_configs;
}

[System.Serializable]
public class AudioConfig
{
    public int audio_id;
    public AudioClip audio_clip;
}