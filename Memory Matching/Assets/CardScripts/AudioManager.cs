using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceBGM;
    public AudioSource audioSourceBGM
    {
        get { return _audioSourceBGM; }
        set { _audioSourceBGM = value; }
    }

    [SerializeField] private AudioSource _audioSourceFX;
    public AudioSource audioSourceFX
    {
        get { return _audioSourceFX; }
        set { _audioSourceFX = value; }
    }


    [SerializeField] private List<AudioConfig> all_audio_configs;

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("More than one AudioManager Instance");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadComponent();
        PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadComponent()
    {
        this.all_audio_configs = GameManager.Instance.GetAudioConfigs().all_audio_configs;

    }
    public void PlayBGM(int value)
    {
        AudioClip audioClip = null;
        if(all_audio_configs == null)
        {
            return;
        }
        foreach(AudioConfig config in all_audio_configs)
        {
            if (config.audio_id == value)
            {
                audioClip = config.audio_clip;
            }
        }
        audioSourceBGM.clip = audioClip;
        audioSourceBGM.Play();
    }

    public void PlayFX(int value)
    {
        AudioClip audioClip = null;
        if (all_audio_configs == null)
        {
            return;
        }
        foreach (AudioConfig config in all_audio_configs)
        {
            if (config.audio_id == value)
            {
                audioClip = config.audio_clip;
            }
        }
        audioSourceFX.clip = audioClip;
        audioSourceFX.Play();
    }
}
