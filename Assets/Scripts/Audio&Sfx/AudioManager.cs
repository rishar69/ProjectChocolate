using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    
    public static AudioManager Instance;
    public static SFXManager SFXManager { get; private set; }

    public MusicManager MusicManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        MusicManager = GetComponentInChildren<MusicManager>();

        if (MusicManager == null)
        {
            Debug.LogError("MusicManager not found as a child of AudioManager!");
        }

        SFXManager = GetComponentInChildren<SFXManager>();

        if (SFXManager == null)
        {
            Debug.LogError("SFXManager not found as a child of AudioManager!");
        }
    }
    private void OnEnable()
    {
        LoadVolume();
    }
    public void UpdateMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SaveVolume()
    {
        audioMixer.GetFloat("MasterVolume", out float masterVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);

        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", defaultValue: 0f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", defaultValue: 0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", defaultValue: 0f);

    }
}
