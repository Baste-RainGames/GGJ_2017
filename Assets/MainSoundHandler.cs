using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainSoundHandler : MonoBehaviour {

    private static MainSoundHandler instance;
    public AudioClip movieSceneClip;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    private AudioSource playingSource;
    private AudioSource otherSource;
    private AudioSource oneOff;

    public void Start() {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneLoaded;

        playingSource = gameObject.AddComponent<AudioSource>();
        playingSource.clip = movieSceneClip;
        playingSource.loop = true;
        playingSource.outputAudioMixerGroup = musicMixer;

        otherSource = gameObject.AddComponent<AudioSource>();
        otherSource.loop = true;
        otherSource.outputAudioMixerGroup = musicMixer;

        oneOff = gameObject.AddComponent<AudioSource>();
        oneOff.loop = false;
        oneOff.outputAudioMixerGroup = sfxMixer;

        playingSource.Play();
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1) {
        var levelSetup = FindObjectOfType<LevelSetup>();
        if (levelSetup == null)
            return;

        StartCoroutine(CrossFadeTo(levelSetup.music)); 
    }

    private IEnumerator CrossFadeTo(AudioClip levelSetupMusic) {
        otherSource.clip = levelSetupMusic;
        otherSource.volume = 0f;
        otherSource.Play();

        float time = 0f;
        while (time < 1f) {
            yield return null;
            time += Time.deltaTime;
            playingSource.volume = 1f - time;
            otherSource.volume = time;
        }

        playingSource.volume = 0f;
        otherSource.volume = 1f;

        playingSource.Stop();

        var temp = playingSource;
        playingSource = otherSource;
        otherSource = temp;
    }

    public static void PlayClip(AudioClip clip) {
        instance.oneOff.PlayOneShot(clip);
    }
}
