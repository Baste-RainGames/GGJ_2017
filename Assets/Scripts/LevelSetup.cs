using UnityEngine;
using UnityEngine.Audio;

public class LevelSetup : MonoBehaviour {

    public bool gunActive = true;
    public bool teleportActive = true;
    public bool sightActive = true;

    public AudioClip music;
    public AudioMixerGroup musicMixer;

    private void Awake() {
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = music;
        source.loop = true;
        source.outputAudioMixerGroup = musicMixer;
        source.Play();
    }

}