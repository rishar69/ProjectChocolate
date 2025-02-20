using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [SerializeField]
    private SFXLibrary sfxLibrary;
    [SerializeField]
    private AudioSource sfx2DSource;
  
    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
    }
}