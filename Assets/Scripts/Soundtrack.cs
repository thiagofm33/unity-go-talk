using System.Collections;
using UnityEngine;

public class Soundtrack : MonoBehaviour {

    public AudioClip enemySoundtrack;
    public AudioClip bossSoundtrack;

    private AudioSource audioSource;

    private float defaultVolume;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
    }

    public void PlayEnemySoundtrack() {
        audioSource.volume = defaultVolume;
        audioSource.clip = enemySoundtrack;
        audioSource.Play();
    }

    public void PlayBossSoundtrack() {
        audioSource.volume = defaultVolume;
        audioSource.clip = bossSoundtrack;
        audioSource.Play();
    }

    public void FadeOut(float target = 0) {
        StartCoroutine(DoFadeOut(target));
    }

    private IEnumerator DoFadeOut(float target = 0) {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while(audioSource.volume > target) {
            audioSource.volume = audioSource.volume - (0.018f * Time.deltaTime);
            yield return wait;
        }

        if(target == 0)
            audioSource.Stop();
    }

}
