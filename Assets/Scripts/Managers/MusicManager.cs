using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; } 

    public AudioClip[] musicTracks;
    public AudioClip[] soundEffectsTracks;
    private int currentTrackIndex = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.volume = 0.5f;

            StartCoroutine(PlayMusicSequentially());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayMusicSequentially()
    {
        while (true)
        {
            audioSource.clip = musicTracks[currentTrackIndex];

            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        }
    }

    public void PlaySoundEffect(string soundEffectName)
    {
        AudioClip soundToPlay = System.Array.Find(soundEffectsTracks, sound => sound.name == soundEffectName);

        if (soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Sound effect not found: " + soundEffectName);
        }
    }
}
