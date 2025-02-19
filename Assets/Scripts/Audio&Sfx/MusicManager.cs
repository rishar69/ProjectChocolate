using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;


    public void PlayMusic(string trackName, float fadeDuration = 0.7f)
    {
        AudioClip nextTrack = musicLibrary.GetClipFromName(trackName);
        if (nextTrack == null)
        {
            Debug.LogWarning($"Track {trackName} not found!");
            return;
        }

        StartCoroutine(AnimateMusicCrossfade(nextTrack, fadeDuration));
    }

    private IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        float initialVolume = musicSource.volume;

        // Fade out
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(initialVolume, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;

        // Fade in
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, initialVolume, percent);
            yield return null;
        }
    }
}
