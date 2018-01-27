using System.Collections;
using UnityEngine;

public class FootstepSounds : MonoBehaviour {

    private bool shouldPlay;
    public bool ShouldPlay {
        set {
            var old = shouldPlay;
            shouldPlay = value;

            if (old != shouldPlay) {
                if (!shouldPlay) {
                    if (playSoundRoutine != null) {
                        StopCoroutine(playSoundRoutine);
                        playSoundRoutine = null;
                    }
                }
                else {
                    playSoundRoutine = StartCoroutine(PlayFootsteps());
                }
            }
        }
    }

    private IEnumerator PlayFootsteps() {
        yield return new WaitForSeconds(delayBeforeFirst);
        while (true) {
            var step = footsteps[Random.Range(0, footsteps.Length)];
            audioSource.PlayOneShot(step);
            yield return new WaitForSeconds(timeBetweenSteps);
        }
    }

    public AudioClip[] footsteps;
    [Range(0f, 1f)]
    public float delayBeforeFirst;
    [Range(0f, 1f)]
    public float timeBetweenSteps;
    public AudioSource audioSource;

    private Coroutine playSoundRoutine;

}