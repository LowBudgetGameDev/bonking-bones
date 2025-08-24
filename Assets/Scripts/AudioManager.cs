using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;

    private float volume; // Ranges from 0.0 - 1.0

    private void Awake()
    {
        Instance = this;

        volume = SavedData.GetFloat(SavedData.Data.Volume, 1.0f);
    }

    private void Start()
    {
        audioMixer.SetFloat("Volume", VolumeToGain(volume));
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;

        volume = Mathf.Clamp01(volume);
        SavedData.SetFloat(SavedData.Data.Volume, volume);

        audioMixer.SetFloat("Volume", VolumeToGain(volume));
    }

    public float GetVolume()
    {
        return volume;
    }

    private float VolumeToGain(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, 0.001f, 1f);

        return Mathf.Log10(clampedVolume) * 20f;
    }

}
