using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; } 

    public AudioClip[] musicTracks;
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
}
